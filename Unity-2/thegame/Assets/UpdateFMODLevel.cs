using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class UpdateFMODLevel : MonoBehaviour
{
    public StudioEventEmitter music;
    public EventInstance musicEvent;
    public LevelInitializer levelInitalizer;
    public float levelValue = 0f;

    void Start()
    {
        music = GetComponentInChildren<StudioEventEmitter>();
        musicEvent = RuntimeManager.CreateInstance(music.EventReference);
        musicEvent.setParameterByName("Level", levelValue);
        musicEvent.start();
    }

    private void Update()
    {
        if (levelInitalizer == null)
        {
            levelInitalizer = FindObjectOfType<LevelInitializer>();
        } else
        {
            return;
        }
        levelValue = levelInitalizer.soundLevel;
        if (levelValue >= 5)
        {
            levelValue = 4.99f;
        }
        musicEvent.setParameterByName("Level", levelValue);
    }
}
