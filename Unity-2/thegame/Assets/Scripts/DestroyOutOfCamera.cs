using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DestroyOutOfCamera : MonoBehaviour
{
    private bool hasPop = false;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasPop && this.sprite.IsVisibleFrom(Camera.main) == true)
        {
            hasPop = true;
        }
        if (this.sprite.IsVisibleFrom(Camera.main) == false)
        {
            if (!hasPop)
            {
                return;
            }
            Destroy(this.gameObject, 1f);
        }
    }
}
