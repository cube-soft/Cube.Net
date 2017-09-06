﻿/* ------------------------------------------------------------------------- */
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
using System.Collections.Generic;

namespace Cube.Net.Rss
{
    /* --------------------------------------------------------------------- */
    ///
    /// RssFeed
    ///
    /// <summary>
    /// RSS 情報を保持するクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class RssFeed
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Id
        /// 
        /// <summary>
        /// Web サイトの ID を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Id { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        /// 
        /// <summary>
        /// Web サイトのタイトルを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Description
        /// 
        /// <summary>
        /// Web サイトの概要を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Description { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Link
        /// 
        /// <summary>
        /// Web サイトの URL を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Uri Link { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Icon
        /// 
        /// <summary>
        /// Web サイトのアイコンを示す URL を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Uri Icon { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Items
        /// 
        /// <summary>
        /// 新着記事一覧を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<RssItem> Items { get; set; }

        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// RssItem
    ///
    /// <summary>
    /// RSS の項目情報を保持するクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class RssItem
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Id
        /// 
        /// <summary>
        /// 記事 ID を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Id { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Title
        /// 
        /// <summary>
        /// 記事タイトルを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Title { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Summary
        /// 
        /// <summary>
        /// 記事の概要を取得または設定します。
        /// </summary>
        /// 
        /// <remarks>
        /// RSS では description タグ、Atom では summary タグに相当します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public string Summary { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Content
        /// 
        /// <summary>
        /// 記事の詳細内容を取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string Content { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Link
        /// 
        /// <summary>
        /// 記事の URL を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Uri Link { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// PublishTime
        /// 
        /// <summary>
        /// 記事の発行日時を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public DateTime PublishTime { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Categories
        /// 
        /// <summary>
        /// 記事の属するカテゴリ一覧を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<string> Categories { get; set; }

        #endregion
    }
}
