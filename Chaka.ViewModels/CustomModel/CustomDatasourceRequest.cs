using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.CustomModel
{

    public class CustomDataSourceRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public List<Kendo.Mvc.SortDescriptor> Sorts { get; set; }
        public List<Filter> Filters { get; set; }
        public List<object> Groups { get; set; }
        public List<object> Aggregates { get; set; }
        public List<string> SelectedID { get; set; }
        public List<string> UnselectedID { get; set; }
       

       

        public class FilterDescriptor
        {
            public string ConvertedValue { get; set; }
            public string Member { get; set; }
            public object MemberType { get; set; }
            public int Operator { get; set; }
            public object Value { get; set; }

            public List<FilterDescriptor> FilterDescriptors { get; set; }
        }

        public class Filter
        {
            public string FilterType { get; set; }
            public int LogicalOperator { get; set; }
            public List<FilterDescriptor> FilterDescriptors { get; set; }

        
        }


    }
}
