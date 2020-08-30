// Copyright (c) 2020 Jonathan Rainier / skyPBX LLC. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Facebook.Lib.Interface
{
    public interface IFacebookService
    {
        FacebookController Controller { get; set; }

        Task<T> GetAsync<T>(Dictionary<string, string> parameters = default);

        Task<T> PostAsync<T>(object content, Dictionary<string, string> urlParameters = default,
            Dictionary<string, string> queryParameters = default);
    }
}