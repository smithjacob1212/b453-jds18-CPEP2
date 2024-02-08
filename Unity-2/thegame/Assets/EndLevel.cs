using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private LevelInitializer levelManager;

    public void Start()
    {
        levelManager = FindObjectOfType<LevelInitializer>();
        if (levelManager == null)
        {
            throw new System.Exception("No LevelInitializer found !");
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            if (levelManager.isLastLevel)
            {
                GameManager.Instance.LoadCredit();
                return;
            }
            GameManager.Instance.LoadNextLevel();
            return;
        }
    }
}
