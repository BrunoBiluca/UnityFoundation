using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestWebRequests : MonoBehaviour {
    [SerializeField] public Text text;
    [SerializeField] public SpriteRenderer sprite;
    void Start() {
        var url = "http://localhost:3000/";
        WebRequests.Get(url,
            (string response) => { text.text = response; },
            (string error) => { Debug.LogError(error); }
        );

        var imageUrl = "http://localhost:3000/image.jpg";
        WebRequests.GetImage(imageUrl,
            (Texture2D texture) => {
                sprite.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            },
            (string error) => { Debug.LogError(error); }
        );
    }
}
