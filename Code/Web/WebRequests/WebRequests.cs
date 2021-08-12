using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.UnityFoundation.Code.Web
{
    public static class WebRequests
    {
        private class WebRequestsMonoBehaviour : MonoBehaviour { }

        private static WebRequestsMonoBehaviour webRequestsMonoBehaviour;

        private static WebRequestsMonoBehaviour MonoBehaviourRef()
        {
            if(webRequestsMonoBehaviour == null)
            {
                webRequestsMonoBehaviour = new GameObject("WebRequests")
                    .AddComponent<WebRequestsMonoBehaviour>();
            }
            return webRequestsMonoBehaviour;
        }

        public static void Get(string url, Action<string> onSuccess, Action<string> onError)
        {
            MonoBehaviourRef().StartCoroutine(OnGet(url, onSuccess, onError));
        }

        private static IEnumerator OnGet(string url, Action<string> onSuccess, Action<string> onError)
        {
            using var request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError)
            {
                onError($"Erro ao conectar ao servidor ({request.responseCode})");
                yield break;
            }

            if(request.responseCode != 200)
            {
                onError(
                    $"Erro na requisição "
                    + $"com código ({request.responseCode}) "
                    + $"- error: {request.error}"
                );
                yield break;
            }

            onSuccess(request.downloadHandler.text);
        }

        public static void Post(
            string url, string body, Action<string> onSuccess, Action<string> onError
        )
        {
            MonoBehaviourRef().StartCoroutine(OnPost(url, body, onSuccess, onError));
        }

        private static IEnumerator OnPost(
            string url, string body, Action<string> onSuccess, Action<string> onError
        )
        {
            using var request = UnityWebRequest.Post(url, body);

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError)
            {
                onError($"Erro ao conectar ao servidor ({request.responseCode})");
                yield break;
            }

            onSuccess(request.downloadHandler.text);
        }

        public static void GetImage(string url, Action<Texture2D> onSuccess, Action<string> onError)
        {
            MonoBehaviourRef().StartCoroutine(OnGetImage(url, onSuccess, onError));
        }

        private static IEnumerator OnGetImage(string url, Action<Texture2D> onSuccess, Action<string> onError)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);

            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.ConnectionError)
            {
                onError($"Erro ao conectar ao servidor ({request.responseCode})");
                yield break;
            }

            DownloadHandlerTexture downloadHandler = request.downloadHandler as DownloadHandlerTexture;
            onSuccess(downloadHandler.texture);
        }
    }
}