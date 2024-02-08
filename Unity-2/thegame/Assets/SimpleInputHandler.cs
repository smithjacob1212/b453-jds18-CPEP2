using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SimpleInputHandler : MonoBehaviour
{
    public StudioEventEmitter clicking;
    public StudioEventEmitter endSound;
    public GameObject fmodManager;
    private void Start()
    {
       fmodManager = GameObject.FindGameObjectWithTag("FMODMANAGER");
        if (fmodManager != null)
        {
            EventInstance e = fmodManager.GetComponent<StudioEventEmitter>().EventInstance;
            FindObjectOfType<UpdateFMODLevel>().enabled = false;
            e.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
    public void OnActionTriggered()
    {
        RuntimeManager.PlayOneShot(clicking.EventReference);
        GameManager.Instance.LoadNextLevel();
    }
    public void OnCreditTriggered()
    {
        RuntimeManager.PlayOneShot(clicking.EventReference);
        GameManager.Instance.LoadMainMenu();

    }
}
