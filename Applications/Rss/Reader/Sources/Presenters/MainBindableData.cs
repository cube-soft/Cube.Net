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
using Cube.Xui;
using System.Collections.Generic;

namespace Cube.Net.Rss.Reader
{
    /* --------------------------------------------------------------------- */
    ///
    /// MainBindableData
    ///
    /// <summary>
    /// メイン画面にバインドされるデータ群を定義したクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class MainBindableData
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// RssBindableData
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="root">ルートオブジェクト</param>
        /// <param name="settings">設定用オブジェクト</param>
        /// <param name="dispatcher">同期用コンテキスト</param>
        ///
        /* ----------------------------------------------------------------- */
        public MainBindableData(IEnumerable<IRssEntry> root,
            SettingsFolder settings, IDispatcher dispatcher)
        {
            Root       = root;
            Lock       = new Bindable<LockSettings>(settings.Lock, dispatcher);
            Local      = new Bindable<LocalSettings>(settings.Value, dispatcher);
            Shared     = new Bindable<SharedSettings>(settings.Shared, dispatcher);
            Current    = new Bindable<IRssEntry>(dispatcher);
            LastEntry  = new Bindable<RssEntry>(dispatcher);
            Content    = new Bindable<object>(dispatcher);
            Message    = new Bindable<string>(dispatcher);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Root
        ///
        /// <summary>
        /// 登録されている RssEntry のルートにあたるオブジェクトを
        /// 取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<IRssEntry> Root { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Lock
        ///
        /// <summary>
        /// ロック情報を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable<LockSettings> Lock { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Local
        ///
        /// <summary>
        /// ローカル設定を保持するオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable<LocalSettings> Local { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Shared
        ///
        /// <summary>
        /// ユーザ設定を保持するオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable<SharedSettings> Shared { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Current
        ///
        /// <summary>
        /// 選択中のカテゴリまたは RSS エントリを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable<IRssEntry> Current { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// LastEntry
        ///
        /// <summary>
        /// 最後に選択した RSS エントリを取得します。
        /// </summary>
        ///
        /// <remarks>
        /// RSS エントリを選択中の場合、Current と LastEntry は同じ値に
        /// なります。カテゴリを選択中の場合、Current は該当カテゴリの
        /// 値を LastEntry は直前まで選択されていた RssEntry の値を保持
        /// します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable<RssEntry> LastEntry { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Content
        ///
        /// <summary>
        /// Web ブラウザに表示する内容を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable<object> Content { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Message
        ///
        /// <summary>
        /// メッセージを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable<string> Message { get; }

        #endregion
    }
}