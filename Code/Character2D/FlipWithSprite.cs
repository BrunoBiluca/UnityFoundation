using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWithSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.localPosition;
    }

    private void Start()
    {
        spriteRenderer = GetComponentInParent<Player>().GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var direction = spriteRenderer.flipX ? -1 : 1;
        transform.localPosition = new Vector3(
            direction * originalPosition.x,
            originalPosition.y,
            originalPosition.z
        );
    }
}
