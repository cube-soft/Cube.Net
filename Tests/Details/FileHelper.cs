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
using Cube.FileSystem;
using System.Reflection;

namespace Cube.Net.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// FileHelper
    ///
    /// <summary>
    /// テストでファイルを使用するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class FileHelper
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// FileHelper
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected FileHelper() : this(new Operator()) { }

        /* ----------------------------------------------------------------- */
        ///
        /// FileHelper
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="io">ファイル操作用オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        protected FileHelper(Operator io)
        {
            IO = io;
            Root = IO.Get(Assembly.GetExecutingAssembly().Location).DirectoryName;
            _directory = GetType().FullName;

            if (!IO.Exists(Results)) IO.CreateDirectory(Results);
            Delete(Results);
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// IO
        ///
        /// <summary>
        /// ファイル操作用オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected Operator IO { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Root
        ///
        /// <summary>
        /// テスト用リソースの存在するルートディレクトリへのパスを
        /// 取得、または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Root { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Examples
        ///
        /// <summary>
        /// テスト用ファイルの存在するフォルダへのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Examples => IO.Combine(Root, "Examples");

        /* ----------------------------------------------------------------- */
        ///
        /// Results
        ///
        /// <summary>
        /// テスト結果を格納するためのフォルダへのパスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected string Results => IO.Combine(Root, $@"Results\{_directory}");

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Example
        ///
        /// <summary>
        /// ファイル名に対して Examples フォルダのパスを結合したパスを
        /// 取得します。
        /// </summary>
        ///
        /// <param name="filename">ファイル名</param>
        /// <returns>パス</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected string Example(string filename) => IO.Combine(Examples, filename);

        /* ----------------------------------------------------------------- */
        ///
        /// Example
        ///
        /// <summary>
        /// ファイル名に対して Results フォルダのパスを結合したパスを
        /// 取得します。
        /// </summary>
        ///
        /// <param name="filename">ファイル名</param>
        /// <returns>パス</returns>
        ///
        /* ----------------------------------------------------------------- */
        protected string Result(string filename) => IO.Combine(Results, filename);

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Delete
        ///
        /// <summary>
        /// 指定されたフォルダ内に存在する全てのファイルおよびフォルダを
        /// 削除します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Delete(string directory)
        {
            foreach (string f in IO.GetFiles(directory)) IO.Delete(f);
            foreach (string d in IO.GetDirectories(directory))
            {
                Delete(d);
                IO.Delete(d);
            }
        }

        #endregion

        #region Fields
        private string _directory = string.Empty;
        #endregion
    }
}
