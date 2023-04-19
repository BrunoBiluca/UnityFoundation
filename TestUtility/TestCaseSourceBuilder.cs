using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityFoundation.TestUtility
{
    /// <summary>
    /// Default class to help creating testing with parameters.
    /// </summary>
    public class TestCaseSourceBuilder
    {
        private List<TestCaseData> data;

        private string metaName = "";
        private object[] metaArguments = new object[0];

        public TestCaseSourceBuilder()
        {
            data = new List<TestCaseData>();
        }

        /// <summary>
        /// Arguments used in all tests registered after it. 
        /// These arguments are put in the begging of the TestCaseData
        /// </summary>
        public TestCaseSourceBuilder Meta(string name, params object[] arguments)
        {
            metaName = name;
            metaArguments = arguments;
            return this;
        }

        /// <summary>
        /// Register a TestCaseData
        /// </summary>
        /// <param name="name">Test name (shown on Test Runner)</param>
        /// <param name="arguments">Test parameters</param>
        public TestCaseSourceBuilder Test(string name, params object[] arguments)
        {
            var args = metaArguments.Concat(arguments).ToArray();
            data.Add(new TestCaseData(args).SetName(CreateTestName(name)));
            return this;
        }

        public TestCaseSourceBuilder Test(string name, params (string, object)[] arguments)
        {
            var testName = name;
            var args = new List<object>();
            foreach(var argument in arguments)
            {
                testName += $" {argument.Item1} {argument.Item2}";
                args.Add(argument.Item2);
            }
            return Test(testName, args.ToArray());
        }

        public TestCaseSourceBuilder Test(
            string name,
            string argumentName1, object argument1
        )
        {
            var testName = name;
            var args = new List<object>() { argument1 };
            testName += $" {argumentName1} {argument1}";
            return Test(testName, args.ToArray());
        }

        public TestCaseSourceBuilder Test(
            string name,
            string argumentName1, object argument1,
            string argumentName2, object argument2
        )
        {
            var testName = name;
            var args = new List<object>() { argument1, argument2 };
            testName += $" {argumentName1} {argument1}";
            testName += $" {argumentName2} {argument2}";
            return Test(testName, args.ToArray());
        }

        public TestCaseSourceBuilder Test(
            string name,
            string argumentName1, object argument1,
            string argumentName2, object argument2,
            string argumentName3, object argument3
        )
        {
            var testName = name;
            var args = new List<object>() { argument1, argument2, argument3 };
            testName += $" {argumentName1} {argument1}";
            testName += $" {argumentName2} {argument2}";
            testName += $" {argumentName3} {argument3}";
            return Test(testName, args.ToArray());
        }

        public TestCaseSourceBuilder Test(string name, Func<object> initializationCallback)
        {
            return Test(name, initializationCallback());
        }

        private string CreateTestName(string testName)
        {
            var name = "";
            if(metaArguments.Length > 0)
                name += "given " + metaName + " test ";

            return name + testName;
        }

        public IEnumerable<TestCaseData> Build()
        {
            foreach(var test in data)
            {
                yield return test;
            }
        }
    }
}