using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public static class WebRequests {

    private class WebRequestsMonoBehaviour : MonoBehaviour { }

    private static WebRequestsMonoBehaviour webRequestsMonoBehaviour;

    private static WebRequestsMonoBehaviour MonoBehaviourRef() {
        if(webRequestsMonoBehaviour == null) {
            webRequestsMonoBehaviour = new GameObject("WebRequests").AddComponent<WebRequestsMonoBehaviour>();
        }
        return webRequestsMonoBehaviour;
    }

    public static void Get(string url, Action<string> onSuccess, Action<string> onError) {
        MonoBehaviourRef().StartCoroutine(OnGet(url, onSuccess, onError));
    }

    public static IEnumerator OnGet(string url, Action<string> onSuccess, Action<string> onError) {
        using var request = UnityWebRequest.Get(url); 
        
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError) {
            onError($"Erro ao conectar ao servidor ({request.responseCode})");
            yield break;
        }

        onSuccess(request.downloadHandler.text);
    }

    public static void GetImage(string url, Action<Texture2D> onSuccess, Action<string> onError) {
        MonoBehaviourRef().StartCoroutine(OnGetImage(url, onSuccess, onError));
    }

    public static IEnumerator OnGetImage(string url, Action<Texture2D> onSuccess, Action<string> onError) {
        using var request = UnityWebRequestTexture.GetTexture(url); 
        
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.ConnectionError) {
            onError($"Erro ao conectar ao servidor ({request.responseCode})");
            yield break;
        }

        DownloadHandlerTexture downloadHandler = request.downloadHandler as DownloadHandlerTexture;
        onSuccess(downloadHandler.texture);
    }
}
