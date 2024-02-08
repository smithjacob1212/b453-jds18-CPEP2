using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Terrain : MonoBehaviour
{
    public Collider2D boxCollider2D;
    public ColorEnum color = ColorEnum.black;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ManagePlayerTerrain(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ManagePlayerTerrain(collision);
    }

    public void ManagePlayerTerrain(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ManageObstacleColor(this.color);
        }
        return;
    }
}
