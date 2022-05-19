using System;

namespace UnityFoundation.Code.Guard
{
    public static class Guard
    {
        public static T Required<T>(T param, string field)
        {
            return param != null
                ? param
                : throw new ArgumentException($"{field} is required");
        }
    }
}