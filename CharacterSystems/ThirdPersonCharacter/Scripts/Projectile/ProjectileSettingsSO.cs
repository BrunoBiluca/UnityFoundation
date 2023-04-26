using UnityEngine;

namespace UnityFoundation.ThirdPersonCharacter
{

    [CreateAssetMenu(
        menuName = ThridPersonEditorConfig.BASE_CONTEXT_MENU_PATH + "Projectile Settings",
        fileName = "new_projectile_settings"
    )]
    public class ProjectileSettingsSO : ScriptableObject
    {
        [field: SerializeField] public Projectile.Settings Config { get; private set; }
    }
}