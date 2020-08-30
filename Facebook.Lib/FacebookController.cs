// Copyright (c) 2020 Jonathan Rainier / skyPBX LLC. All rights reserved.

using System;
using System.ComponentModel;
using Facebook.Lib.Enums;
using Facebook.Lib.Extensions;

namespace Facebook.Lib
{
    public class FacebookController
    {
        private readonly string _token;
        private readonly ApiVersion _version;

        public FacebookController(string token, ApiVersion version)
        {
            _token = token;
            _version = version;

            BaseUri = new Uri("https://graph.facebook.com/" +
                              version.GetAttributeOfType<DescriptionAttribute>().Description);
        }

        public Uri BaseUri { get; }
    }
}