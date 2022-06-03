#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityFoundation.Code;

namespace UnityFoundation.Tools.Spline
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class SplineFollowBehaviour : EnumX<SplineFollowBehaviour>
    {
        static SplineFollowBehaviour()
        {
        }

        public static readonly SplineFollowBehaviour oneGo
            = new SplineFollowBehaviour(0, "one_go");
        public static readonly SplineFollowBehaviour loop
            = new SplineFollowBehaviour(1, "loop");
        public static readonly SplineFollowBehaviour backAndForth
            = new SplineFollowBehaviour(2, "back_and_forth");

        public SplineFollowBehaviour(int index, string name) : base(index, name) { }
    }
}