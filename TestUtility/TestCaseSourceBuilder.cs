using NUnit.Framework;
using System.Collections.Generic;

namespace UnityFoundation.TestUtility
{
    public class TestCaseSourceBuilder
    {
        private List<TestCaseData> data;

        public TestCaseSourceBuilder()
        {
            data = new List<TestCaseData>();
        }

        public TestCaseSourceBuilder Test(string name, params object[] arguments)
        {
            data.Add(new TestCaseData(arguments).SetName(name));
            return this;
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