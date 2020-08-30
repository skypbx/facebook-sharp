// Copyright (c) 2020 Jonathan Rainier / skyPBX LLC. All rights reserved.

using Facebook.Lib.Attributes;
using Newtonsoft.Json;

namespace Facebook.Lib.Models
{
    [Api("/{page-id}/feed")]
    public class Feed
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }
}