using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chaka.WEB.Models
{
    public class TestClass
    {
        public IEnumerable<ListTest> List { get; set; }
    }
    public class ListTest
    {
        public int ID { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
    }
}
