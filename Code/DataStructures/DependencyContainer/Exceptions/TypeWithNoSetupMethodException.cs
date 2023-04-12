using System;
using System.Linq;

namespace UnityFoundation.Code
{
    public class TypeWithNoSetupMethodException : Exception
    {
        const string msg = "<type> has no setup method\n<all_types>";

        public TypeWithNoSetupMethodException(Type type)
            : base(
                msg
                  .Replace("<type>", type.ToString())
                  .Replace(
                    "<all_types>",
                    string.Join("\n", type.GetMethods().Select(m => m.ToString()))
                )
            )
        {
        }
    }
}
