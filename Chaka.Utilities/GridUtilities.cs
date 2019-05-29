using Chaka.ViewModels.CustomModel;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chaka.Utilities
{
    public static class GridUtilities
    {
        public static string ConvertKendoRequestToJson(DataSourceRequest request)
        {
            try
            {
                CustomDataSourceRequest customDataSourceRequest = new CustomDataSourceRequest();

                customDataSourceRequest.Page = request.Page;
                customDataSourceRequest.PageSize = request.PageSize;
                customDataSourceRequest.Sorts = request.Sorts != null ? request.Sorts.ToList() : null;
                customDataSourceRequest.Filters = new List<CustomDataSourceRequest.Filter>();

                if (request.Filters != null)
                {
                    foreach (var filter in request.Filters)
                    {
                        if (filter.GetType() == typeof(FilterDescriptor))
                        {
                            var filterItem = (FilterDescriptor)filter;

                            CustomDataSourceRequest.Filter customFilter = new CustomDataSourceRequest.Filter();
                            customFilter.FilterDescriptors = new List<CustomDataSourceRequest.FilterDescriptor>();
                            // (filterItem.Member, (FilterOperator)(filterItem.Operator), filterItem.Value);

                            customFilter.FilterType = "FilterDescriptor";

                            customFilter.FilterDescriptors.Add(new CustomDataSourceRequest.FilterDescriptor()
                            {
                                Member = filterItem.Member,
                                Operator = (int)filterItem.Operator,
                                Value = filterItem.Value,

                            });

                            customDataSourceRequest.Filters.Add(customFilter);
                        }
                        if (filter.GetType() == typeof(CompositeFilterDescriptor))
                        {
                            CompositeFilterDescriptor compositeFilterDescriptor = (CompositeFilterDescriptor)filter;

                            CustomDataSourceRequest.Filter compositeFilter = new CustomDataSourceRequest.Filter();
                            compositeFilter.FilterDescriptors = new List<CustomDataSourceRequest.FilterDescriptor>();

                            // (filterItem.Member, (FilterOperator)(filterItem.Operator), filterItem.Value);

                            compositeFilter.FilterType = "CompositeFilter";
                            compositeFilter.LogicalOperator = (int)compositeFilterDescriptor.LogicalOperator;

                            foreach (var compFilter in compositeFilterDescriptor.FilterDescriptors)
                            {
                                var filItem = (FilterDescriptor)compFilter;

                                compositeFilter.FilterDescriptors.Add(new CustomDataSourceRequest.FilterDescriptor()
                                {
                                    Member = filItem.Member,
                                    Operator = (int)filItem.Operator,
                                    Value = filItem.Value,

                                });
                            }

                            customDataSourceRequest.Filters.Add(compositeFilter);
                        }
                    }
                }
                

                var jsonCustom = JsonConvert.SerializeObject(customDataSourceRequest);

                return jsonCustom;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataSourceRequest ConvertToKendoFromCustomRequest(CustomDataSourceRequest customDataSourceRequest)
        {
            try
            {
                DataSourceRequest req = new DataSourceRequest();

                req.Page = customDataSourceRequest.Page;
                req.PageSize = customDataSourceRequest.PageSize;

                List<SortDescriptor> sorts = new List<SortDescriptor>();

                if (customDataSourceRequest.Sorts != null)
                {
                    foreach (var item in customDataSourceRequest.Sorts)
                    {
                        SortDescriptor newSort = new SortDescriptor();
                        newSort.Member = item.Member;
                        newSort.SortCompare = null; //item.SortCompare;
                        newSort.SortDirection = (ListSortDirection)item.SortDirection;
                        sorts.Add(newSort);
                    }
                }
               

                req.Sorts = sorts;

                List<IFilterDescriptor> filters = new List<IFilterDescriptor>();

                foreach (var item in customDataSourceRequest.Filters)
                {
                    if (item.FilterType == "FilterDescriptor")
                    {
                        var filItem = item.FilterDescriptors.FirstOrDefault();

                        FilterDescriptor filterDescriptor = new FilterDescriptor(filItem.Member, (FilterOperator)(filItem.Operator), filItem.Value);

                        filters.Add(filterDescriptor);
                    }
                    if (item.FilterType == "CompositeFilter")
                    {
                        CompositeFilterDescriptor compositeFilter = new CompositeFilterDescriptor();
                        compositeFilter.LogicalOperator = (FilterCompositionLogicalOperator)item.LogicalOperator;
                        foreach (var filterItem in item.FilterDescriptors)
                        {
                            FilterDescriptor filterDescriptor = new FilterDescriptor(filterItem.Member, (FilterOperator)(filterItem.Operator), filterItem.Value);

                            compositeFilter.FilterDescriptors.Add(filterDescriptor);
                        }
                        filters.Add(compositeFilter);
                    }

                }

                req.Filters = filters;

                return req;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
