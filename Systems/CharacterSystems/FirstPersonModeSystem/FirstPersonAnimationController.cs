using Assets.UnityFoundation.UnityAdapter;

namespace UnityFoundation.FirstPersonModeSystem
{
    public class FirstPersonAnimationParams
    {
        public const string AIM = "aim";
        public const string FIRE = "fire";
        public const string RELOAD = "reload";
        public const string WALKING = "walking";
    }

    // TODO: transformar essa classe de Animation Controller em abstrata, para usar em outros projetos
    // utilizar um animation controller ir� facilitar na verifica��o de problemas com o Mechanin, 
    // e a garantir desacoplamento do comportamento do objeto da anima��o que ele est� desempenhando
    public class FirstPersonAnimationController
    {
        private readonly IAnimator animator;

        public FirstPersonAnimationController(IAnimator animator)
        {
            this.animator = animator;
        }

        public void ToggleAim()
        {
            animator.SetBool(
                FirstPersonAnimationParams.AIM,
                !animator.GetBool(FirstPersonAnimationParams.AIM)
            );
        }

        public void Walking(bool state)
        {
            animator.SetBool(FirstPersonAnimationParams.WALKING, state);
        }

        public void Fire()
        {
            animator.SetTrigger(FirstPersonAnimationParams.FIRE);
        }

        public void Reload()
        {
            animator.SetTrigger(FirstPersonAnimationParams.RELOAD);
        }
    }
}