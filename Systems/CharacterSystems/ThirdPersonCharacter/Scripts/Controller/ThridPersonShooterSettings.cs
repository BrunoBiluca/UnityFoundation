using UnityEngine;

namespace UnityFoundation.ThirdPersonCharacter
{
    [CreateAssetMenu(
        menuName = ThridPersonEditorConfig.BASE_CONTEXT_MENU_PATH + "Third Person Shooter Settings",
        fileName = "new_thrid_person_shooter_settings"
    )]
    public class ThridPersonShooterSettings : ScriptableObject
    {
        [SerializeField] public float normalSensitivity = 0.8f;
        [SerializeField] public float aimSensitivity = 0.1f;
        [SerializeField] public float aimSpeed = 10f;
        [SerializeField] public float rotateCharacterSpeed = 10f;
    }
}