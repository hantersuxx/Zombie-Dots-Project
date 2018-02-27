using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteResizerOnCamera : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Camera cam;

    void OnEnable()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        ApplyCameraSize();
    }

    public void ApplyCameraSize()
    {
        sprite.transform.localScale = new Vector3(1, 1, 1);

        float width = sprite.sprite.bounds.size.x;
        float height = sprite.sprite.bounds.size.y;

        float camHeight = cam.orthographicSize * 2f;
        float camWidth = camHeight / Screen.height * Screen.width;

        Vector3 xWidth = sprite.transform.localScale;
        xWidth.x = camWidth / width;
        sprite.transform.localScale = xWidth;

        Vector3 yHeight = sprite.transform.localScale;
        yHeight.y = camHeight / height;
        transform.localScale = yHeight;
    }
}
