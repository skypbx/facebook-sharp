// Copyright (c) 2020 Jonathan Rainier / skyPBX LLC. All rights reserved.

using System.Collections.Generic;
using Facebook.Lib.Attributes;
using Newtonsoft.Json;

namespace Facebook.Lib.Models.Me
{
    [Api("/me/accounts")]
    public class Accounts
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public List<Datum> Data { get; set; }

        [JsonProperty("paging", NullValueHandling = NullValueHandling.Ignore)]
        public Paging Paging { get; set; }
    }

    public class Datum
    {
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken { get; set; }

        [JsonProperty("category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

        [JsonProperty("category_list", NullValueHandling = NullValueHandling.Ignore)]
        public List<CategoryList> CategoryList { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("tasks", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tasks { get; set; }
    }

    public class CategoryList
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public class Paging
    {
        [JsonProperty("cursors", NullValueHandling = NullValueHandling.Ignore)]
        public Cursors Cursors { get; set; }
    }

    public class Cursors
    {
        [JsonProperty("before", NullValueHandling = NullValueHandling.Ignore)]
        public string Before { get; set; }

        [JsonProperty("after", NullValueHandling = NullValueHandling.Ignore)]
        public string After { get; set; }
    }
}