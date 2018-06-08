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

using System;
using System.Drawing;

namespace StreetSmart.WinForms.Interfaces.GeoJson
{
  /// <summary>
  /// Result direction of the observation
  /// </summary>
  public interface IResultDirection
  {
    /// <summary>
    /// Direction
    /// </summary>
    IDirection Direction { get; }

    /// <summary>
    /// GroundLevel offset
    /// </summary>
    double GroundLevelOffset { get; }

    /// <summary>
    /// ImageId
    /// </summary>
    string Id { get; }

    /// <summary>
    /// match image
    /// </summary>
    Image MatchImage { get; }

    /// <summary>
    /// Orientation
    /// </summary>
    IProperty Orientation { get; }

    /// <summary>
    /// Position
    /// </summary>
    IPositionStdev Position { get; }

    /// <summary>
    /// RecordedAt
    /// </summary>
    DateTime? RecordedAt { get; }
  }
}
