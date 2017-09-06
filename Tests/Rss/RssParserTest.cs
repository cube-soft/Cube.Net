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
using System.IO;
using System.Linq;
using NUnit.Framework;
using Cube.Net.Rss;

namespace Cube.Net.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// RssParserTest
    ///
    /// <summary>
    /// RssParser のテスト用クラスです。
    /// </summary>
    /// 
    /// <remarks>
    /// サンプルファイルの一部は下記 Web ページ、およびリンク先から
    /// 取得しました。
    /// https://validator.w3.org/feed/docs/rss1.html
    /// https://validator.w3.org/feed/docs/rss2.html
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    [Parallelizable]
    [TestFixture]
    class RssParserTest : FileHandler
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// ParseFeed
        /// 
        /// <summary>
        /// フィードを解析するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCaseSource(nameof(TestCases))]
        public int ParseFeed(string src, RssFeed expected)
        {
            using (var stream = File.OpenRead(Example(src)))
            {
                var actual = RssParser.Create(stream);

                Assert.That(actual.Id,          Is.EqualTo(expected.Id));
                Assert.That(actual.Title,       Is.EqualTo(expected.Title));
                Assert.That(actual.Description, Is.EqualTo(expected.Description));
                Assert.That(actual.Icon,        Is.EqualTo(expected.Icon));
                Assert.That(actual.Link,        Is.EqualTo(expected.Link));

                return actual.Items.Count();
            }
        }

        #endregion

        #region TestCases

        /* ----------------------------------------------------------------- */
        ///
        /// TestCases
        /// 
        /// <summary>
        /// テスト用データを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static IEnumerable<TestCaseData> TestCases
        {
            get
            {
                yield return new TestCaseData("SampleRss20-01.xml", new RssFeed
                {
                    Id          = null,
                    Title       = "Scripting News",
                    Description = "A weblog about scripting and stuff like that.",
                    Icon        = null,
                    Link        = new Uri("http://www.scripting.com/"),
                }).Returns(9);

                yield return new TestCaseData("SampleAtom-01.xml", new RssFeed
                {
                    Id          = "urn:uuid:60a76c80-d399-11d9-b93C-0003939e0af6",
                    Title       = "Example Feed",
                    Description = null,
                    Icon        = null,
                    Link        = new Uri("http://example.org/"),
                }).Returns(1);
            }
        }

        #endregion
    }
}
