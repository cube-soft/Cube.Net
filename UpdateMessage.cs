﻿/* ------------------------------------------------------------------------- */
///
/// UpdateMessage.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.Net
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Net.UpdateMessage
    /// 
    /// <summary>
    /// アップデートメッセージを格納するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class UpdateMessage
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Notify
        ///
        /// <summary>
        /// メッセージを通知すべきかどうかを表す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool Notify { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Version
        ///
        /// <summary>
        /// メッセージの対象となるバージョンを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Version { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// アップデートメッセージを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Text { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Uri
        ///
        /// <summary>
        /// アップデートを行うための URL を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Uri Uri { get; set; }

        #endregion
    }
}