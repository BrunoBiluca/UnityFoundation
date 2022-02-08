using UnityEngine;

namespace Assets.UnityFoundation.UnityAdapter
{
    public class AnimatorController : IAnimator
    {
        private readonly Animator animator;

        public AnimatorController(Animator animator)
        {
            this.animator = animator;
        }

        public void SetTrigger(string name) => animator.SetTrigger(name);

        public void SetBool(string name, bool value) => animator.SetBool(name, value);

        public bool GetBool(string name) => animator.GetBool(name);
    }
}