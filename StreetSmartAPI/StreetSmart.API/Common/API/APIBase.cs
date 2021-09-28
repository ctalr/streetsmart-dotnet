/*
 * Street Smart .NET integration
 * Copyright (c) 2016 - 2020, CycloMedia, All rights reserved.
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 */

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using StreetSmart.Common.Data;
using StreetSmart.Common.Exceptions;

using CefSharp;

#if WINFORMS
using CefSharp.WinForms;
#else
using CefSharp.Wpf;
#endif

namespace StreetSmart.Common.API
{
  // ReSharper disable once InconsistentNaming
  abstract class APIBase: DataConvert
  {
    #region Tasks

    private readonly Dictionary<string, TaskCompletionSource<object>> _resultTask;

    #endregion

    #region Members

    private int _processId;

    #endregion

    #region Properties

    protected int GetProcessId => _processId = (_processId + 1) % 10000;

    protected ChromiumWebBrowser Browser { get; set; }

    protected virtual string CallFunctionBase => string.Empty;

    public virtual string JsThis => $"{GetType().Name}Events";

    #endregion

    #region Callback definitions

    public virtual string JsResult => $"{nameof(OnResult).FirstCharacterToLower()}";

    public string JsThisResult => $"{nameof(OnThisResult).FirstCharacterToLower()}";

    protected string JsLoginSuccess => $"{nameof(OnLoginSuccess).FirstCharacterToLower()}";

    protected string JsLoginFailed => $"{nameof(OnLoginFailedException).FirstCharacterToLower()}";

    protected string JsMeasurementFailed => $"{nameof(OnMeasurementException).FirstCharacterToLower()}";

    public virtual string JsImNotFound => $"{nameof(OnImageNotFoundException).FirstCharacterToLower()}";

    protected string JsCloseViewerException => $"{nameof(OnViewerCloseException).FirstCharacterToLower()}";

    protected string JsStreetSmartException => $"{nameof(OnStreetSmartException).FirstCharacterToLower()}";

    #endregion

    #region Constructors

    protected APIBase()
    {
      _processId = 0;
      _resultTask = new Dictionary<string, TaskCompletionSource<object>>();
    }

    protected APIBase(ChromiumWebBrowser browser)
      : this()
    {
      Browser = browser;
    }

    #endregion

    #region Callbacks

    public void OnResult(object result, string funcName)
    {
      CheckResultTask(funcName);
      _resultTask[funcName].TrySetResult(result);
    }

    public void OnThisResult(bool result, string funcName)
    {
      CheckResultTask(funcName);
      _resultTask[funcName].TrySetResult(result);
    }

    public void OnLoginSuccess(string funcName)
    {
      CheckResultTask(funcName);
      _resultTask[funcName].TrySetResult(true);
    }

    public void OnMeasurementException(string message, string funcName)
    {
      CheckResultTask(funcName);
      _resultTask[funcName].TrySetResult(new StreetSmartMeasurementException(message));
    }

    public void OnLoginFailedException(string message, string funcName)
    {
      CheckResultTask(funcName);
      _resultTask[funcName].TrySetResult(new StreetSmartLoginFailedException(message));
    }

    public void OnImageNotFoundException(string message, string funcName)
    {
      CheckResultTask(funcName);
      _resultTask[funcName].TrySetResult(new StreetSmartImageNotFoundException(message));
    }

    public void OnViewerCloseException(string message, string funcName)
    {
      CheckResultTask(funcName);
      _resultTask[funcName].TrySetResult(new StreetSmartCloseViewerException(message));
    }

    public void OnStreetSmartException(string message, string funcName)
    {
      CheckResultTask(funcName);
      _resultTask[funcName].TrySetResult(new StreetSmartException(message));
    }

    #endregion

    #region Functions

    protected void RegisterThisJsObject()
    {
      Browser.JavascriptObjectRepository.Register(JsThis, this, false);
    }

    protected bool CheckResultTask(string funcName)
    {
      bool result = true;

      if (!_resultTask.ContainsKey(funcName))
      {
        _resultTask.Add(funcName, new TaskCompletionSource<object>());
        result = false;
      }

      return result;
    }

    protected string GetScript(string funcName, int processId = 0, [CallerMemberName] string memberName = "")
    {
      string memberId = $"{memberName}{processId}";
      return $"{JsThis}.{JsResult}({CallFunctionBase}.{funcName},{memberId.ToQuote()});";
    }

    protected async Task<object> CallJsGetScriptAsync(string script, [CallerMemberName] string memberName = "")
    {
      int processId = GetProcessId;
      object result = await CallJsAsync(AddTryCatch(GetScript(script, processId, memberName), $"{memberName}{processId}"), processId, memberName);

      if (result is Exception exception)
      {
        throw exception;
      }

      return result;
    }

    protected string AddTryCatch(string script, string funcName)
    {
      return $"try{{{script}}}catch(e){{{JsThis}.{JsStreetSmartException}(e.message,{funcName.ToQuote()});}}";
    }

    protected virtual async Task<object> CallJsAsync(string script, int processId, [CallerMemberName] string memberName = "")
    {
      object funcResult = null;
      IBrowser myBrowser = Browser?.GetBrowser();

      if (myBrowser != null)
      {
        string funcName = $"{memberName}{processId}";

        if (CheckResultTask(funcName))
        {
          _resultTask[funcName] = new TaskCompletionSource<object>();
        }
        
        // Manual bindings due to newer versions requiring these:
        Browser.ExecuteScriptAsyncWhenPageLoaded(@"(async function() {await CefSharp.BindObjectAsync('StreetSmartAPIEvents', 'bound');})();");
        Browser.ExecuteScriptAsyncWhenPageLoaded(@"(async function() {await CefSharp.BindObjectAsync('PanoramaViewerListEvents', 'bound');})();");
   
        Browser.ExecuteScriptAsync(script);
        await _resultTask[funcName].Task;
        TaskCompletionSource<object> result = _resultTask[funcName];
        _resultTask.Remove(funcName);
        funcResult = result.Task.Result;
      }

      return funcResult;
    }

    #endregion
  }
}
