﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using Cube.Xui.Converters;

namespace Cube.Net.App.Rss.Reader
{
    /* --------------------------------------------------------------------- */
    ///
    /// MinuteConverter
    ///
    /// <summary>
    /// 秒単位の値を分単位に変換するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MinuteConverter : DuplexConverter
    {
        /* ----------------------------------------------------------------- */
        ///
        /// MinuteConverter
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public MinuteConverter() : base(
            e => (int)(((TimeSpan?)e)?.TotalMinutes ?? 0.0),
            e => TimeSpan.FromMinutes(int.Parse(e as string))
        ) { }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// HourConverter
    ///
    /// <summary>
    /// 秒単位の値を時間単位に変換するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class HourConverter : DuplexConverter
    {
        /* ----------------------------------------------------------------- */
        ///
        /// HourConverter
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public HourConverter() : base(
            e => (int)(((TimeSpan?)e)?.TotalHours ?? 0.0),
            e => TimeSpan.FromHours(int.Parse(e as string))
        ) { }
    }
}
