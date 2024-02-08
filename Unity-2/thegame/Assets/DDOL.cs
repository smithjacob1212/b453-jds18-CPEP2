using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DDOL[] ddols = FindObjectsOfType<DDOL>();

        if (ddols.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
