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

namespace StreetSmart.WinForms.Interfaces
{
  /// <summary>
  /// TileSchema of the recording.
  /// </summary>
  public enum TileSchema
  {
    /// <summary>
    /// No tiling
    /// </summary>
    NoTiling = 1,

    /// <summary>
    /// DCR9 Tiling.
    /// </summary>
    Dcr9Tiling = 2,

    /// <summary>
    /// DCR10 Tiling
    /// </summary>
    Dcr10Tiling = 3
  }
}
