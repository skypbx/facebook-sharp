// Copyright (c) 2020 Jonathan Rainier / skyPBX LLC. All rights reserved.

using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Facebook.Lib.Extensions
{
    public static class ContentExtension
    {
        public static StringContent AsJson(this object o)
        {
            return new StringContent(JsonConvert.SerializeObject(o), Encoding.UTF8, "application/json");
        }
    }
}