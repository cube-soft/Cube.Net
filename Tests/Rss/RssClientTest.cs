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
using Cube.Net.Rss;
using NUnit.Framework;

namespace Cube.Net.Tests.Rss
{
    /* --------------------------------------------------------------------- */
    ///
    /// RssClientTest
    ///
    /// <summary>
    /// RssClient のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class RssClientTest
    {
        /* ----------------------------------------------------------------- */
        ///
        /// GetAsync
        /// 
        /// <summary>
        /// RSS フィードを取得するテストを実行します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetAsync()
        {
            var client = new RssClient();
            var rss = client.GetAsync(new Uri("http://blog.cube-soft.jp/?feed=rss2")).Result;
            Assert.That(rss, Is.Not.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetAsync_Redirect
        /// 
        /// <summary>
        /// HTML ページを示す URL を指定した時の挙動を確認します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetAsync_Redirect()
        {
            var redirect = default(Uri);
            var client = new RssClient();
            client.Redirected += (s, e) => redirect = e.NewValue;

            var rss = client.GetAsync(new Uri("http://blog.cube-soft.jp/")).Result;
            Assert.That(rss, Is.Not.Null);
            Assert.That(redirect, Is.EqualTo(new Uri("http://blog.cube-soft.jp/?feed=rss2")));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetAsync_NotFound
        /// 
        /// <summary>
        /// RSS フィードが見つからない時の挙動を確認します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetAsync_NotFound()
        {
            var client = new RssClient();
            var rss = client.GetAsync(new Uri("http://www.cube-soft.jp/")).Result;
            Assert.That(rss, Is.Null);
        }
    }
}