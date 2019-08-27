using System.Collections.Generic;

namespace Suilder.Test.Reflection.Builder.TablePerHierarchy.Tables
{
    public class Department : BaseConfig
    {
        public virtual Employee Boss { get; set; }

        public virtual List<Employee> Employees { get; set; }
    }
}