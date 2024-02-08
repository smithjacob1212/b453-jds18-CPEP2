using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class PlayerDeath : MonoBehaviour
{
    public StudioEventEmitter dying;
    public CheckpointManager checkpointManager;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkpointManager = FindObjectOfType<CheckpointManager>();
    }

    // Start is called before the first frame update
    public void Die()
    {
        RuntimeManager.PlayOneShot(dying.EventReference);
        Vector3 destinationPos = checkpointManager.GetCurrentCheckPoint().transform.position;
        transform.position = destinationPos;
        rb.velocity = Vector2.zero;
    }
}
