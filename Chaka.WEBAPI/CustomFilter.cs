using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chaka.WEBAPI
{
    public class CustomFilter : IFilterDescriptor
    {
        public Expression CreateFilterExpression(Expression instance)
        {
            throw new NotImplementedException();
        }
    }
}
