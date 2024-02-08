using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public CheckPoint[] checkPoints;

    public void Start()
    {
        checkPoints = GetComponentsInChildren<CheckPoint>();
    }
    public CheckPoint GetCurrentCheckPoint()
    {
        for(int i = checkPoints.Length - 1; i >= 0; i--)
        {
            if(checkPoints[i].activated) return checkPoints[i];
        }
        return checkPoints[0];
    }
}
