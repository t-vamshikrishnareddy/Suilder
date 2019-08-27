using System;
using Suilder.Builder;
using Suilder.Core;
using Suilder.Exceptions;
using Suilder.Functions;
using Suilder.Reflection.Builder;

namespace Suilder.Engines
{
    /// <summary>
    /// Contains the configuration of a SQL engine.
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// The engine options.
        /// </summary>
        /// <value>The engine options.</value>
        EngineOptions Options { get; }

        /// <summary>
        /// Compiles an <see cref="IQueryFragment"/> to a <see cref="QueryResult"/>.
        /// </summary>
        /// <param name="query">The <see cref="IQueryFragment"/>.</param>
        /// <returns>The <see cref="QueryResult"/>.</returns>
        QueryResult Compile(IQueryFragment query);

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        void AddFunction(string name);

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="nameSql">The tranlated name of the function.</param>
        void AddFunction(string name, string nameSql);

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="func">A custom delegate to compile the function.</param>
        void AddFunction(string name, FunctionCompile func);

        /// <summary>
        /// Register a function in the engine.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="nameSql">The tranlated name of the function.</param>
        /// <param name="func">A custom delegate to compile the function.</param>
        void AddFunction(string name, string nameSql, FunctionCompile func);

        /// <summary>
        /// Removes a registered function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        void RemoveFunction(string name);

        /// <summary>
        ///  Determines if the function is registered.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns><see langword="true"/> if the function is registered, otherwise, <see langword="false"/>.</returns>
        bool ContainsFunction(string name);

        /// <summary>
        /// Removes all registered functions.
        /// </summary>
        void ClearFunctions();

        /// <summary>
        /// Gets the function data.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The function data.</returns>
        IFunctionData GetFunction(string name);

        /// <summary>
        /// Escape a name.
        /// </summary>
        /// <param name="name">The name to escape.</param>
        /// <returns>The escaped name.</returns>
        string EscapeName(string name);

        /// <summary>
        /// Gets the registered types.
        /// </summary>
        /// <returns>The registered types.</returns>
        Type[] GetRegisteredTypes();

        /// <summary>
        /// Gets the information of the table.
        /// </summary>
        /// <param name="type">The table type.</param>
        /// <returns>The table info.</returns>
        /// <exception cref="InvalidConfigurationException">The type is not registered.</exception>
        ITableInfo GetInfo(Type type);

        /// <summary>
        /// Gets the information of the table.
        /// </summary>
        /// <typeparam name="T">The table type.</typeparam>
        /// <returns>The table info.</returns>
        /// <exception cref="InvalidConfigurationException">The type is not registered.</exception>
        ITableInfo<T> GetInfo<T>();

        /// <summary>
        /// Gets the information of the table or <see langword="null"/> if no element is found.
        /// </summary>
        /// <param name="type">The table type.</param>
        /// <returns>The table info.</returns>
        ITableInfo GetInfoOrDefault(Type type);

        /// <summary>
        /// Gets the information of the table or <see langword="null"/> if no element is found.
        /// </summary>
        /// <typeparam name="T">The table type.</typeparam>
        /// <returns>The table info.</returns>
        ITableInfo<T> GetInfoOrDefault<T>();

        /// <summary>
        /// Checks if the type is a table.
        /// </summary>
        /// <param name="type">The table type.</param>
        /// <returns><see langword="true"/> if the type is a table, otherwise, <see langword="false"/>.</returns>
        bool IsTable(Type type);

        /// <summary>
        /// Checks if the type is a table.
        /// </summary>
        /// <typeparam name="T">The table type.</typeparam>
        /// <returns><see langword="true"/> if the type is a table, otherwise, <see langword="false"/>.</returns>
        bool IsTable<T>();
    }
}