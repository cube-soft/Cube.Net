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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Cube.FileSystem;
using Cube.Settings;
using Cube.Log;

namespace Cube.Net.Rss
{
    /* --------------------------------------------------------------------- */
    ///
    /// RssCacheCollection
    ///
    /// <summary>
    /// RSS フィードを保持するためのコレクションクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class RssCacheCollection : IDictionary<Uri, RssFeed>
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// IO
        /// 
        /// <summary>
        /// 入出力用オブジェクトを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Operator IO { get; set; } = new Operator();

        /* ----------------------------------------------------------------- */
        ///
        /// Directory
        /// 
        /// <summary>
        /// キャッシュを保存するためのディレクトリのパスを取得または
        /// 設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Directory { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// Capacity
        /// 
        /// <summary>
        /// メモリ上に保持しておく要素数を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public uint Capacity
        {
            get => _capacity;
            set => _capacity = Math.Max(value, 1);
        }

        #region IDictionary<Uri, RssFeed>

        /* ----------------------------------------------------------------- */
        ///
        /// Item
        /// 
        /// <summary>
        /// 指定したキーを持つ要素を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RssFeed this[Uri key]
        {
            get
            {
                var dest = _src[key];
                Pop(key, dest);
                return dest;
            }
            set => _src[key] = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Keys
        /// 
        /// <summary>
        /// キー一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ICollection<Uri> Keys => _src.Keys;

        /* ----------------------------------------------------------------- */
        ///
        /// Values
        /// 
        /// <summary>
        /// 値一覧を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ICollection<RssFeed> Values => _src.Values;

        /* ----------------------------------------------------------------- */
        ///
        /// Count
        /// 
        /// <summary>
        /// 要素の数を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Count => _src.Count;

        /* ----------------------------------------------------------------- */
        ///
        /// IsReadOnly
        /// 
        /// <summary>
        /// 読み取り専用かどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsReadOnly => _src.IsReadOnly;

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        /// 
        /// <summary>
        /// メモリ上にある要素を全てキャッシュファイルに保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Save()
        {
            foreach (var uri in _otm)
            {
                try { Stash(uri); }
                catch (Exception err) { this.LogWarn(err.ToString(), err); }
            }
            _otm.Clear();
        }

        #region IDictionary<Uri, RssFeed>

        /* ----------------------------------------------------------------- */
        ///
        /// Contains
        /// 
        /// <summary>
        /// 指定された要素が含まれているかどうかを判別します。
        /// </summary>
        /// 
        /// <param name="item">要素</param>
        /// 
        /// <returns>含まれているかどうか</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool Contains(KeyValuePair<Uri, RssFeed> item) => _src.Contains(item);

        /* ----------------------------------------------------------------- */
        ///
        /// ContainsKey
        /// 
        /// <summary>
        /// 指定されたキーを持つ要素が含まれているかどうかを判別します。
        /// </summary>
        /// 
        /// <param name="key">要素</param>
        /// 
        /// <returns>含まれているかどうか</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool ContainsKey(Uri key) => _src.ContainsKey(key);

        /* ----------------------------------------------------------------- */
        ///
        /// Add
        /// 
        /// <summary>
        /// 指定したキーおよび値を持つ要素を追加します。
        /// </summary>
        /// 
        /// <param name="key">キー</param>
        /// <param name="value">値</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Add(Uri key, RssFeed value)
        {
            _src.Add(key, value);
            _otm.Add(key);
            Stash();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Add
        /// 
        /// <summary>
        /// 要素を追加します。
        /// </summary>
        /// 
        /// <param name="item">要素</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Add(KeyValuePair<Uri, RssFeed> item)
        {
            _src.Add(item);
            _otm.Add(item.Key);
            Stash();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Remove
        /// 
        /// <summary>
        /// 指定したキーを持つ要素を削除します。
        /// </summary>
        /// 
        /// <param name="key">キー</param>
        /// 
        /// <returns>削除が成功したかどうか</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool Remove(Uri key)
        {
            var result =  _src.Remove(key);
            if (result) _otm.Remove(key);
            return result;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Remove
        /// 
        /// <summary>
        /// 指定した要素を削除します。
        /// </summary>
        /// 
        /// <param name="item">要素</param>
        /// 
        /// <returns>削除が成功したかどうか</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool Remove(KeyValuePair<Uri, RssFeed> item)
        {
            var result = _src.Remove(item);
            if (result) _otm.Remove(item.Key);
            return result;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Clear
        /// 
        /// <summary>
        /// 全ての項目を削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Clear()
        {
            _src.Clear();
            _otm.Clear();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TryGetValue
        /// 
        /// <summary>
        /// 指定したキーに対応する値を取得します。
        /// </summary>
        /// 
        /// <param name="key">キー</param>
        /// <param name="value">値を格納するためのオブジェクト</param>
        /// 
        /// <returns>取得が成功したかどうか</returns>
        ///
        /* ----------------------------------------------------------------- */
        public bool TryGetValue(Uri key, out RssFeed value)
        {
            var result = _src.TryGetValue(key, out value);
            if (result) Pop(key, value);
            return result;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CopyTo
        /// 
        /// <summary>
        /// 全ての要素を配列にコピーします。
        /// </summary>
        ///
        /// <remarks>
        /// このメソッドはサポートされません。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public void CopyTo(KeyValuePair<Uri, RssFeed>[] array, int index)
            => throw new NotSupportedException();

        /* ----------------------------------------------------------------- */
        ///
        /// GetEnumerator
        /// 
        /// <summary>
        /// コレクションを反復処理する列挙子を取得します。
        /// </summary>
        /// 
        /// <returns>列挙子</returns>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerator<KeyValuePair<Uri, RssFeed>> GetEnumerator()
        {
            foreach (var kv in _src)
            {
                Pop(kv.Key, kv.Value);
                yield return kv;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetEnumerator
        /// 
        /// <summary>
        /// コレクションを反復処理する列挙子を取得します。
        /// </summary>
        /// 
        /// <returns>列挙子</returns>
        ///
        /* ----------------------------------------------------------------- */
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Stash
        ///
        /// <summary>
        /// 最も古い要素をファイルに保存します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Stash()
        {
            if (_otm.Count <= Capacity) return;
            var uri = _otm[0];
            Stash(uri);
            _otm.Remove(uri);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Stash
        ///
        /// <summary>
        /// RSS フィードをキャッシュファイルに保存します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void Stash(Uri uri)
        {
            if (string.IsNullOrEmpty(Directory) || !_src.ContainsKey(uri)) return;

            var feed = _src[uri];
            if (feed == null || feed.LastChecked == DateTime.MinValue) return;

            if (!IO.Exists(Directory)) IO.CreateDirectory(Directory);
            using (var s = IO.Create(CacheName(uri))) SettingsType.Json.Save(s, feed);

            feed.Items.Clear();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Pop
        ///
        /// <summary>
        /// RSS フィードをキャッシュファイルから復帰させます。
        /// </summary>
        /// 
        /// <remarks>
        /// キャッシュファイルから復帰させるタイミングで既読の記事を
        /// 完全に削除します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        private void Pop(Uri uri, RssFeed dest)
        {
            try
            {
                if (_otm.Contains(uri))
                {
                    _otm.Remove(uri);
                    _otm.Add(uri);
                    return;
                }

                var feed = Load(uri);
                if (feed != null && feed.LastChecked != DateTime.MinValue)
                {
                    dest.LastChecked = feed.LastChecked;
                    dest.Items.Clear();
                    foreach (var a in feed.Items.Where(e => !e.Read)) dest.Items.Add(a);
                    _otm.Add(uri);
                }
            }
            finally { Stash(); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Load
        ///
        /// <summary>
        /// RSS フィードをキャッシュファイルから読み込みます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private RssFeed Load(Uri uri)
        {
            try
            {
                if (string.IsNullOrEmpty(Directory)) return default(RssFeed);
                var cache = CacheName(uri);
                if (!IO.Exists(cache)) return default(RssFeed);
                using (var s = IO.OpenRead(cache)) return SettingsType.Json.Load<RssFeed>(s);
            }
            catch (Exception err) { this.LogWarn(err.ToString(), err); }
            return default(RssFeed);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CacheName
        ///
        /// <summary>
        /// キャッシュが保存されるファイルのパスを取得します。
        /// </summary>
        /// 
        /// <param name="uri">URL</param>
        /// 
        /// <returns>キャッシュファイルのパス</returns>
        ///
        /* ----------------------------------------------------------------- */
        private string CacheName(Uri uri)
        {
            var md5  = new MD5CryptoServiceProvider();
            var data = System.Text.Encoding.UTF8.GetBytes(uri.ToString());
            var hash = md5.ComputeHash(data);
            var name = BitConverter.ToString(hash).ToLower().Replace("-", "");
            return IO.Combine(Directory, name);
        }

        #region Fields
        private IDictionary<Uri, RssFeed> _src = new Dictionary<Uri, RssFeed>();
        private IList<Uri> _otm = new List<Uri>();
        private uint _capacity = 10;
        #endregion

        #endregion
    }
}
