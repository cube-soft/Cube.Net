﻿/* ------------------------------------------------------------------------- */
///
/// ClientOperations.cs
///
/// Copyright (c) 2010 CubeSoft, Inc.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Affero General Public License as published
/// by the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Affero General Public License for more details.
///
/// You should have received a copy of the GNU Affero General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
///
/* ------------------------------------------------------------------------- */
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace Cube.Net.Http
{
    /* --------------------------------------------------------------------- */
    ///
    /// ClientOperations
    ///
    /// <summary>
    /// System.Net.Http.HttpClient の拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ClientOperations
    {
        /* --------------------------------------------------------------------- */
        ///
        /// GetJsonAsync
        /// 
        /// <summary>
        /// JSON 形式のデータを非同期で取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static async Task<T> GetJsonAsync<T>(this HttpClient client, Uri uri) where T : class
        {
            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode) return null;

            var stream = await response.Content.ReadAsStreamAsync();
            var json = new DataContractJsonSerializer(typeof(T));
            return json.ReadObject(stream) as T;
        }

        /* --------------------------------------------------------------------- */
        ///
        /// GetXmlAsync
        /// 
        /// <summary>
        /// XML 形式のデータを非同期で取得します。
        /// </summary>
        ///
        /* --------------------------------------------------------------------- */
        public static async Task<T> GetXmlAsync<T>(this HttpClient client, Uri uri) where T : class
        {
            var response = await client.GetAsync(uri);
            if (!response.IsSuccessStatusCode) return null;

            var stream = await response.Content.ReadAsStreamAsync();
            var xml = new XmlSerializer(typeof(T));
            return xml.Deserialize(stream) as T;
        }
    }
}
