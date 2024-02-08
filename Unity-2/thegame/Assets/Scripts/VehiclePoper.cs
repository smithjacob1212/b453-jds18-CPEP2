using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePoper : MonoBehaviour
{
    public GameObject vehicle;
    public Vector3 popping = new Vector3(-50,0,30);
    public float vehicleRate = 10f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateVehicle", 5f, vehicleRate);
    }

    private void InstantiateVehicle()
    {
        Instantiate(
            vehicle,
            new Vector3(
                popping.x, 
                UnityEngine.Random.Range(-5,5), 
                popping.z
                ),
            Quaternion.Euler(0, 0, 0)
            );
    }
}
