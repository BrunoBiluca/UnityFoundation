namespace UnityFoundation.Code.Features
{
    public static class ObjectPoolingStrings
    {
        private const string MISSING_POOLED_OBJECT_COMPONENT
            = "Missing PooledObject component when instantiating object";

        private const string MISSING_POOLED_OBJECT_COMPONENT_WITH_TAG
            = "Missing PooledObject component when instantiating object using Tag <::tag::>";

        public static string MISSING_POOLED_OBJECT_COMPONENT_MSG()
            => MISSING_POOLED_OBJECT_COMPONENT;

        public static string MISSING_POOLED_OBJECT_COMPONENT_MSG(string tag)
            => MISSING_POOLED_OBJECT_COMPONENT_WITH_TAG.Replace("::tag::", tag);
    }
}