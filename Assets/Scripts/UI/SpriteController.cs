using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public Material material;

    //public Sprite sprite;

    public SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer.enabled = false;
    }
    void Update()
    {
        CambiarAlbedo(spriteRenderer.sprite);
    }

    void CambiarAlbedo(Sprite nuevoSprite)
    {
        material.SetTexture("_MainTex", nuevoSprite.texture);
    }


}
