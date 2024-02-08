using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class SimpleMover : MonoBehaviour
{

    public Vector3 movement;
    private Vector3 position;

    private void Start()
    {
        position = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        position.x += movement.x * Time.deltaTime;
        position.y += movement.y * Time.deltaTime;
        position.z += movement.z * Time.deltaTime;
        transform.position = position;
    }
}
