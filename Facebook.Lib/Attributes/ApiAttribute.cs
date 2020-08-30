// Copyright (c) 2020 Jonathan Rainier / skyPBX LLC. All rights reserved.

using System;

namespace Facebook.Lib.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ApiAttribute : Attribute
    {
        public ApiAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; set; }
    }
}