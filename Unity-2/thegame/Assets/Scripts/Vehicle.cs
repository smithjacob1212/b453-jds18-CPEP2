using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Vehicle : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }
}
