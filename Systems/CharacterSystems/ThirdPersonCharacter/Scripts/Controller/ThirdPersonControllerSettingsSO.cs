using UnityEngine;

namespace UnityFoundation.ThirdPersonCharacter
{
    [CreateAssetMenu(
        menuName = ThridPersonEditorConfig.BASE_CONTEXT_MENU_PATH + "Controller settings",
        fileName = "new_third_person_controller_settings"
    )]
    public class ThirdPersonControllerSettingsSO : ScriptableObject
    {
        [field: SerializeField]
        public ThirdPersonController.PlayerSettings PlayerConfig { get; set; }

        [field: SerializeField]
        public ThirdPersonController.GroundedSettings GroundedConfig { get; set; }

        [field: SerializeField]
        public ThirdPersonController.CameraSettings CameraConfig { get; set; }
    }
}