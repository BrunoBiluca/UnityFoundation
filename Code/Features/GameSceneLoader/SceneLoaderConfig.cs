using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.Code
{
    [CreateAssetMenu(
        menuName = UnityFoundationEditorConfig.BASE_CONTEXT_MENU_PATH + "Scene Loader/"  + "Scene Loader configuration",
        fileName = "scene_loader_config"
    )]
    public class SceneLoaderConfig : ScriptableObject
    {
        [field: SerializeField] public List<SceneConfig> ScenesToLoad { get; private set; }
        [field: SerializeField] public List<SceneConfig> ScenesToReload { get; private set; }
        [field: SerializeField] public List<SceneConfig> ScenesToUnLoad { get; private set; }
        [field: SerializeField] public GameObject TransitionPrefab { get; private set; }
    }
}