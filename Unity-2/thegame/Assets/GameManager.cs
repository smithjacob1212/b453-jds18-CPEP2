using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private LevelManager levelManager;
    public Scene scenes;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
        }
        else
        {
            Instance = this;
            levelManager = GetComponent<LevelManager>();
        }
    }

    internal void LoadMainMenu()
    {
        levelManager.LoadLevel(0);
    }

    public void LoadNextLevel()
    {
        levelManager.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    internal void LoadCredit()
    {
        levelManager.LoadCredit();
    }
}
