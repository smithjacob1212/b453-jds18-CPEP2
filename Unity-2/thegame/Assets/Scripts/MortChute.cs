using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MortChute : MonoBehaviour
{
    private Collider2D colliderTriggerChute2D;
    public PlayerDeath player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerDeath>();
        colliderTriggerChute2D = GetComponent<Collider2D>();
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }

    private void Update()
    {
        if (player == null) player = FindObjectOfType<PlayerDeath>();
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (player == null) player = FindObjectOfType<PlayerDeath>();
            player.Die();
        }
    }

}
