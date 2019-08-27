using System;
using System.Collections.Generic;
using Suilder.Exceptions;
using Suilder.Reflection;
using Suilder.Reflection.Builder;
using Xunit;

namespace Suilder.Test.Reflection.Builder
{
    public class DefaultPropertyProcessorTest : BaseTest
    {
        [Fact]
        public void Properties_Columns()
        {
            tableBuilder.Add<PropertyTest>();

            ITableInfo propertyInfo = tableBuilder.GetConfig<PropertyTest>();

            Assert.Equal(new string[] { "PropertyPublicGetSet" }, propertyInfo.Columns);
        }

        [Fact]
        public void Nested_Recursive_Ignore()
        {
            tableBuilder.Add<Recursive.Person>()
                .Ignore(x => x.Employee.Address1.Employee)
                .Ignore(x => x.Employee.Address2.Employee);

            ITableInfo propertyInfo = tableBuilder.GetConfig<Recursive.Person>();

            Assert.Equal(new string[] { "Id", "Name", "Employee.Address1.Street", "Employee.Address2.Street",
                "Employee.Salary" }, propertyInfo.Columns);
        }

        [Fact]
        public void Nested_Recursive_Invalid()
        {
            tableBuilder.Add<Recursive.Person>();

            Exception ex = Assert.Throws<InvalidConfigurationException>(() => tableBuilder.GetConfig());
            Assert.Equal($"The type \"{typeof(Recursive.Person)}\" has nested types with circular references "
                + $"that must be removed or ignored: \"Employee.Address1.Employee\".", ex.Message);
        }


        private class PropertyTest
        {
            public string PropertyPublicGetSet { get; set; }

            public string PropertyPublicGet { get; }

            public string PropertyPublicSet
            {
                set
                {
                    PropertyPrivate = PropertyPrivateStatic = fieldPublic = fieldPrivate = value;
                }
            }

            private string PropertyPrivate { get; set; }

            public static string PropertyPublicStatic { get; set; }

            private string PropertyPrivateStatic { get; set; }

            public string fieldPublic;

            private string fieldPrivate;

            public IList<string> List { get; set; }
        }

        private class Recursive
        {
            public class Person
            {
                public int Id { get; set; }

                public string Name { get; set; }

                public Employee Employee { get; set; }
            }

            [Nested]
            public class Employee
            {
                public Address Address1 { get; set; }

                public Address Address2 { get; set; }

                public decimal Salary { get; set; }
            }

            [Nested]
            public class Address
            {
                public string Street { get; set; }

                public Employee Employee { get; set; }
            }
        }
    }
}