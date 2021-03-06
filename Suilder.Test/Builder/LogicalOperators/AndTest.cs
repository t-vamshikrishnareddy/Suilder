using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Extensions;
using Suilder.Test.Builder.Tables;
using Xunit;

namespace Suilder.Test.Builder.LogicalOperators
{
    public class AndTest : BuilderBaseTest
    {
        [Fact]
        public void Add()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.And
                .Add(person["Id"].Eq(1))
                .Add(person["Active"].Eq(true))
                .Add(person["Name"].Like("%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 AND \"person\".\"Active\" = @p1 "
                + "AND \"person\".\"Name\" LIKE @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%SomeName%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Params()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.And.Add(person["Id"].Eq(1), person["Active"].Eq(true), person["Name"].Like("%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 AND \"person\".\"Active\" = @p1 "
                + "AND \"person\".\"Name\" LIKE @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%SomeName%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Enumerable()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.And.Add(new List<IQueryFragment> { person["Id"].Eq(1), person["Active"].Eq(true),
                person["Name"].Like("%SomeName%") });

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 AND \"person\".\"Active\" = @p1 "
                + "AND \"person\".\"Name\" LIKE @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%SomeName%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression()
        {
            Person person = null;
            IOperator op = sql.And
                .Add(() => person.Id == 1)
                .Add(() => person.Active)
                .Add(() => person.Name.Like("%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 AND \"person\".\"Active\" = @p1 "
                + "AND \"person\".\"Name\" LIKE @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%SomeName%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Params()
        {
            Person person = null;
            IOperator op = sql.And.Add(() => person.Id == 1, () => person.Active, () => person.Name.Like("%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 AND \"person\".\"Active\" = @p1 "
                + "AND \"person\".\"Name\" LIKE @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%SomeName%"
            }, result.Parameters);
        }

        [Fact]
        public void Add_Expression_Enumerable()
        {
            Person person = null;
            IOperator op = sql.And.Add(new List<Expression<Func<bool>>> { () => person.Id == 1, () => person.Active,
                () => person.Name.Like("%SomeName%")});

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 AND \"person\".\"Active\" = @p1 "
                + "AND \"person\".\"Name\" LIKE @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%SomeName%"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_And()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 & person.Active & person.Name.Like("%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 AND \"person\".\"Active\" = @p1 "
                + "AND \"person\".\"Name\" LIKE @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%SomeName%"
            }, result.Parameters);
        }

        [Fact]
        public void Expression_AndAlso()
        {
            Person person = null;
            IOperator op = sql.Op(() => person.Id == 1 && person.Active && person.Name.Like("%SomeName%"));

            QueryResult result = engine.Compile(op);

            Assert.Equal("\"person\".\"Id\" = @p0 AND \"person\".\"Active\" = @p1 "
                + "AND \"person\".\"Name\" LIKE @p2", result.Sql);
            Assert.Equal(new Dictionary<string, object>
            {
                ["@p0"] = 1,
                ["@p1"] = true,
                ["@p2"] = "%SomeName%"
            }, result.Parameters);
        }

        [Fact]
        public void Empty_List()
        {
            IOperator op = sql.And;

            Exception ex = Assert.Throws<CompileException>(() => engine.Compile(op));
            Assert.Equal("List is empty.", ex.Message);
        }

        [Fact]
        public void To_String()
        {
            IAlias person = sql.Alias("person");
            IOperator op = sql.And
                .Add(person["Id"].Eq(1))
                .Add(person["Active"].Eq(true))
                .Add(person["Name"].Like("%SomeName%"));

            Assert.Equal("person.Id = 1 AND person.Active = true AND person.Name LIKE \"%SomeName%\"", op.ToString());
        }
    }
}