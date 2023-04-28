using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    [CreateAssetMenu(
        menuName = UnityFoundationEditorConfig.BASE_CONTEXT_MENU_PATH + "Scene Loader/" + "Scene configuration",
        fileName = "scene_loader_config"
    )]
    public class SceneConfig : ScriptableObject
    {
        [field: SerializeField] public string SceneName { get; private set; }
    }
}