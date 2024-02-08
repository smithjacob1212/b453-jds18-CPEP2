using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform playerStart;
    public bool hasUnlockPink, hasUnlockBlue, hasUnlockYellow;
    public bool isLastLevel = false;
    public float soundLevel = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerStart = FindObjectOfType<PlayerStart>().transform;
        GameObject playerInstance = Instantiate(playerPrefab, playerStart.position, playerStart.rotation);
        PlayerController p = playerInstance.GetComponent<PlayerController>();
        if (hasUnlockBlue)
        {
            p.UnlockColor(ColorEnum.cyan);
        }
        if (hasUnlockYellow)
        {
            p.UnlockColor(ColorEnum.orange);
        }
        if (hasUnlockPink)
        {
            p.UnlockColor(ColorEnum.pink);
        }
    }
}
