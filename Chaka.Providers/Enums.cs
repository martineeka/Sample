using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.Providers
{
    public class StatusLogChange
    {
        public static readonly string NEW = "NEW";
        public static readonly string CHANGE = "CHANGE";
        public static readonly string DELETE = "DELETE";
    }
    public class TypeChartOrganization
    {
        public static readonly string HORIZONTAL = "Horizontal";
        public static readonly string VERTICAL = "Vertical";
    }

    public class OrgChangeStatus
    {
        public static readonly int NotChange = 0;
        public static readonly int Add = 1;
        public static readonly int Change = 2;
        public static readonly int NotActive = 3;
    }

}
