﻿/*
 * Street Smart .NET integration
 * Copyright (c) 2016 - 2018, CycloMedia, All rights reserved.
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

using System.Threading.Tasks;
using CefSharp.WinForms;

using StreetSmart.WinForms.Interfaces;

namespace StreetSmart.WinForms.API
{
  internal class ObliqueViewer : Viewer, IObliqueViewer
  {
    #region Constructors

    public ObliqueViewer(ChromiumWebBrowser browser, ObliqueViewerList obliqueViewerList, string name)
      : base(browser, obliqueViewerList, name)
    {
    }

    #endregion

    #region Interface Functions

    public async Task<bool> GetButtonEnabled(ObliqueViewerButtons buttonId)
    {
      return await base.GetButtonEnabled(buttonId);
    }

    public void ToggleButtonEnabled(ObliqueViewerButtons buttonId, bool enabled)
    {
      base.ToggleButtonEnabled(buttonId, enabled);
    }

    #endregion
  }
}
