using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Suilder.Builder;
using Suilder.Engines;
using Suilder.Exceptions;

namespace Suilder.Core
{
    /// <summary>
    /// Implementation of <see cref="IArithOperator"/>.
    /// </summary>
    public class ArithOperator : QueryFragmentList<object, object>, IArithOperator
    {
        /// <summary>
        /// The operator.
        /// </summary>
        /// <value>The operator.</value>
        public string Op { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArithOperator"/> class.
        /// </summary>
        /// <param name="op">The operator.</param>
        public ArithOperator(string op)
        {
            this.Op = op;
        }

        /// <summary>
        /// Adds a value to the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IArithOperator"/>.</param>
        public override void Add(Expression<Func<object>> value)
        {
            Values.Add(SqlBuilder.Instance.Val(value));
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IArithOperator"/>.</param>
        public override void Add(params Expression<Func<object>>[] values)
        {
            Values.AddRange(values.Select(x => SqlBuilder.Instance.Val(x)));
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IArithOperator"/>.</param>
        public override void Add(IEnumerable<Expression<Func<object>>> values)
        {
            Values.AddRange(values.Select(x => SqlBuilder.Instance.Val(x)));
        }

        /// <summary>
        /// Adds a value to the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        IArithOperator IArithOperator.Add(object value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        IArithOperator IArithOperator.Add(params object[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        IArithOperator IArithOperator.Add(IEnumerable<object> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds a value to the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="value">The value to add to the <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        IArithOperator IArithOperator.Add(Expression<Func<object>> value)
        {
            Add(value);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified array to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The array whose elements should be added to the end of the
        /// <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        IArithOperator IArithOperator.Add(params Expression<Func<object>>[] values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="IArithOperator"/>.
        /// </summary>
        /// <param name="values">The collection whose elements should be added to the end of the
        /// <see cref="IArithOperator"/>.</param>
        /// <returns>The arithmetic operator.</returns>
        IArithOperator IArithOperator.Add(IEnumerable<Expression<Func<object>>> values)
        {
            Add(values);
            return this;
        }

        /// <summary>
        /// Compiles the fragment.
        /// </summary>
        /// <param name="queryBuilder">The query builder.</param>
        /// <param name="engine">The engine.</param>
        public override void Compile(QueryBuilder queryBuilder, IEngine engine)
        {
            if (Values.Count == 0)
                throw new CompileException("List is empty.");

            string separator = " " + Op + " ";
            foreach (object value in Values)
            {
                queryBuilder.WriteValue(value).Write(separator);
            }
            queryBuilder.RemoveLast(separator.Length);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return ToStringBuilder.Build(b => b.Join(" " + Op + " ", Values, (x) => b.WriteValue(x)));
        }
    }
}