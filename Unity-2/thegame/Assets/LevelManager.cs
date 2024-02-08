using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Transition transitionBlack;
    public Transition transitionWhite;
    public float transitionTime = 1f;

    public void Start()
    {
        transitionBlack.FadeOut();
    }
    public void LoadLevel(int scene)
    {
        StartCoroutine(Load(scene));
    }
    public void LoadCredit()
    {
        StartCoroutine(LoadEnd());
    }

    IEnumerator Load(int scene)
    {
        transitionWhite.gameObject.SetActive(false);
        transitionBlack.FadeIn();
        yield return new WaitForSeconds(transitionTime);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (operation.isDone == false)
        {
            yield return null;
            Debug.Log("Progress loading = " + operation.progress);
        }
        transitionBlack.FadeOut();
    }

    IEnumerator LoadEnd()
    {
        transitionWhite.gameObject.SetActive(true);
        yield return new WaitForSeconds(transitionTime);
        AsyncOperation operation = SceneManager.LoadSceneAsync("credit");
        while (operation.isDone == false)
        {
            yield return null;
            Debug.Log("Progress loading = " + operation.progress);
        }
        transitionWhite.FadeOut();
    }
}
