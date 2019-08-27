using Suilder.Reflection.Builder;
using Suilder.Test.Reflection.Builder.TableNested.Tables;
using Xunit;

namespace Suilder.Test.Reflection.Builder.TableNested
{
    public class TableNameTest : BaseTest
    {
        protected override void InitConfig()
        {
            tableBuilder.Add<Person>()
                .TableName("prefix_Person");

            tableBuilder.AddNested<Employee>();

            tableBuilder.Add<Department>()
                .TableName("prefix_Department");
        }

        [Fact]
        public void Table_Name()
        {
            ITableInfo personInfo = tableBuilder.GetConfig<Person>();
            ITableInfo deptInfo = tableBuilder.GetConfig<Department>();

            Assert.Equal("prefix_Person", personInfo.TableName);
            Assert.Equal("prefix_Department", deptInfo.TableName);
        }
    }
}