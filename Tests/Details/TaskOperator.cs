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
using System.Threading;
using System.Threading.Tasks;

namespace Cube.Net.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// TaskOperator
    ///
    /// <summary>
    /// Task の拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class TaskOperator
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Wait
        ///
        /// <summary>
        /// 一定時間待機します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static async Task<bool> Wait(this CancellationTokenSource src)
        {
            try { await Task.Delay(10000, src.Token).ConfigureAwait(false); }
            catch (TaskCanceledException) { return true; /* OK */ }
            return false; /* Timeout */
        }

        #endregion
    }
}
