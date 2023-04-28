using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityFoundation.Code
{
    public class GameSceneLoader : MonoBehaviour
    {
        [field: SerializeField] public bool ExecuteOnAwake { get; private set; }

        [field: SerializeField] public SceneLoaderConfig Config { get; private set; }

        public event Action OnAllScenesLoaded;

        private bool areScenesLoaded;
        private bool areScenesReloaded;
        private bool areScenesUnloaded;

        private GameObject transitionObj;

        public void Awake()
        {
            if(ExecuteOnAwake)
                Execute();
        }

        public void Execute()
        {
            areScenesLoaded = false;
            areScenesReloaded = false;
            areScenesUnloaded = false;

            if(Config.TransitionPrefab != null)
            {
                transitionObj = Instantiate(Config.TransitionPrefab);
                DontDestroyOnLoad(transitionObj);
            }

            AsyncProcessor.I.StartCoroutine(Load(Config.ScenesToLoad));
            AsyncProcessor.I.StartCoroutine(Unload(Config.ScenesToUnLoad));
            AsyncProcessor.I.StartCoroutine(Reload(Config.ScenesToReload));
            AsyncProcessor.I.StartCoroutine(WatingExecution());
        }

        private IEnumerator Reload(List<SceneConfig> scenesToReload)
        {
            if(scenesToReload.Count == 0)
            {
                areScenesReloaded = true;
                yield break;
            }

            var activeScene = scenesToReload[0].name;
            var operation = SceneManager.LoadSceneAsync(activeScene);
            while(!operation.isDone)
                yield return null;

            yield return Unload(scenesToReload.Skip(1).ToList());
            yield return Load(scenesToReload.Skip(1).ToList());

            areScenesReloaded = true;
        }

        private IEnumerator WatingExecution()
        {

            while(!areScenesLoaded || !areScenesUnloaded || !areScenesReloaded)
                yield return null;

            if(transitionObj != null)
                Destroy(transitionObj);

            OnAllScenesLoaded?.Invoke();
        }

        private IEnumerator Load(List<SceneConfig> scenes)
        {
            yield return LoadMainScene(scenes[0].SceneName);

            foreach(var sceneConfig in scenes.Skip(1))
            {
                var sceneName = sceneConfig.SceneName;
                var scene = SceneManager.GetSceneByName(sceneName);
                if(scene.IsValid())
                    yield return LoadSceneFromHierarchy(scene);
                else
                    yield return LoadSceneAdditive(sceneName);

                Debug.Log($"{sceneName} was loaded.");
            }
            areScenesLoaded = true;
        }

        private IEnumerator LoadMainScene(string name)
        {
            var scene = SceneManager.GetSceneByName(name);
            if(scene.IsValid())
            {
                yield return LoadSceneFromHierarchy(scene);
            }
            else
            {
                var operation = SceneManager.LoadSceneAsync(name);
                while(!operation.isDone)
                    yield return null;
            }
        }

        private IEnumerator LoadSceneFromHierarchy(Scene scene)
        {
            Debug.Log("Load scene from hierarchy: " + scene.name);
            while(!scene.isLoaded)
                yield return null;
        }

        private IEnumerator LoadSceneAdditive(string sceneName)
        {
            Debug.Log("Loading additivelly: " + sceneName);
            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while(!operation.isDone)
                yield return null;
        }

        private IEnumerator Unload(List<SceneConfig> scenes)
        {
            foreach(var sceneConfig in scenes)
            {
                var sceneName = sceneConfig.SceneName;
                var scene = SceneManager.GetSceneByName(sceneName);
                if(!scene.IsValid())
                    continue;

                var operation = SceneManager.UnloadSceneAsync(sceneName);
                if(operation == null)
                    continue;

                while(!operation.isDone)
                    yield return null;

                Debug.Log("Scene unloaded: " + sceneName);
            }
            areScenesUnloaded = true;
        }
    }
}