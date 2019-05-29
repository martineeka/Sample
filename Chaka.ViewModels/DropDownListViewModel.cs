using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels
{
    public class DropDownListViewModel
    {
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "Text")]
        public string Text { get; set; }
    }
}
