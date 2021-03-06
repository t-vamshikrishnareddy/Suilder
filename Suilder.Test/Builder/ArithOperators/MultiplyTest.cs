using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.ArithOperators
{
    public class MultiplyTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply
                .Add(person["Salary"])
                .Add(100m)
                .Add(200m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200m
            }, result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(person["Salary"], 100m, 200m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200m
            }, result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply.Add(new List<object> { person["Salary"], 100m, 200m });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200m
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IOperator op = sql.Multiply
                .Add(() => person.Salary)
                .Add(() => 100m)
                .Add(() => 200m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200m
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IOperator op = sql.Multiply.Add(() => person.Salary, () => 100m, () => 200m);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200m
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IOperator op = sql.Multiply.Add(new List<Expression<Func<object>>> { () => person.Salary, () => 100m,
                () => 200m });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200m
            }, result.Parameters);
        }

        [Fact]
        public void Expression()
        {
            Person person = null;
            IOperator op = (IOperator)sql.Val(() => person.Salary * 100 * 200);

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Salary\" * @p0 * @p1", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 100m,
                ["@p1"] = 200m
            }, result.Parameters);
        }

        [Fact]
        public void Empty_List()
        {
            IOperator op = sql.Multiply;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(op));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.Multiply
                .Add(person["Salary"])
                .Add(100m)
                .Add(200m);

            Assert.Equal("person.Salary * 100 * 200", op.ToString());
        }
    }
}