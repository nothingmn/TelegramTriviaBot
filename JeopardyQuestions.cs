using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace DisneyBot
{

    public partial class JeopardyQuestion
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("air_date")]
        public DateTimeOffset AirDate { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("round")]
        public string Round { get; set; }

        [JsonProperty("show_number")]
        public long ShowNumber { get; set; }
    }
}