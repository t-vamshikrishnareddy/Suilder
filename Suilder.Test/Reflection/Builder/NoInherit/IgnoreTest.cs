using System.Collections.Generic;
using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.Builder.NoInherit.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder.NoInherit
{
    public class IgnoreTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .Ignore(x => x.Guid)
                .Ignore(x => x.Address)
                .Ignore(x => x.DepartmentId)
                .Ignore(x => x.Department);

            tableBuilder.Add<Department>()
                .Ignore(x => x.Guid)
                .Ignore(x => x.Boss);
        }

        [Fact]
        public void Primary_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id" }, personInfo.PrimaryKeys);
            Assert.Equal(new string[] { "Id" }, deptInfo.PrimaryKeys);
        }

        [Fact]
        public void Foreign_Keys()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { }, personInfo.ForeignKeys);
            Assert.Equal(new string[] { }, deptInfo.ForeignKeys);
        }

        [Fact]
        public void Columns()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Name", "SurName" }, personInfo.Columns);
            Assert.Equal(new string[] { "Id", "Name" }, deptInfo.Columns);
        }

        [Fact]
        public void Column_Names_Dic()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Name"] = "Name",
                ["SurName"] = "SurName"
            }, personInfo.ColumnNamesDic);

            Assert.Equal(new Dictionary<string, string>
            {
                ["Id"] = "Id",
                ["Name"] = "Name"
            }, deptInfo.ColumnNamesDic);
        }

        [Fact]
        public void Column_Names()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal(new string[] { "Id", "Name", "SurName" }, personInfo.ColumnNames);
            Assert.Equal(new string[] { "Id", "Name" }, deptInfo.ColumnNames);
        }
    }
}