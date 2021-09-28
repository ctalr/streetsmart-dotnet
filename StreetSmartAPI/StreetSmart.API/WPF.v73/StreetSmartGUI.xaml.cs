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

using System.Windows;

using StreetSmart.Common.API;
using StreetSmart.Common.Interfaces.API;

namespace StreetSmart.Wpf
{
  /// <summary>
  /// Interaction logic for StreetSmartGUI.xaml
  /// </summary>
  public partial class StreetSmartGUI
  {
    #region dependency property API

    /// <summary>
    /// Dependency property of the api
    /// </summary>
    public static readonly DependencyProperty ApiProperty =
      DependencyProperty.Register("Api", typeof(IStreetSmartAPI), typeof(StreetSmartGUI),
        new PropertyMetadata(null, OnApiChanged));

    /// <summary>
    /// Changed the api
    /// </summary>
    private static void OnApiChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      if (e.NewValue != null)
      {
        ((StreetSmartGUI) sender).InitApi((StreetSmartAPI) e.NewValue);
      }
    }

    #endregion

    #region Properties

    /// <summary>
    /// The StreetSmart API
    /// </summary>
    public IStreetSmartAPI Api
    {
      get => (IStreetSmartAPI) GetValue(ApiProperty);
      set => SetValue(ApiProperty, value);
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor of the StreetSmartGUI
    /// </summary>
    public StreetSmartGUI()
    {
      try
      {
        InitializeComponent();
      }
      catch (System.Exception) { }
    }

    #endregion

    #region private function - init the API

    private void InitApi(StreetSmartAPI api)
    {
      api.InitBrowser(Browser);
    }

    #endregion
  }
}
