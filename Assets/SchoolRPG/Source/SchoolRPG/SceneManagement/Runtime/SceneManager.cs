using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    //singleton
    public static SceneManagerScript instance = null;

    private string lastDoorUsed;

    [SerializeField] private SceneEventChannel sceneEventChannel;
    [SerializeField] private ScreenFader screenFader;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void SetLastDoorUsed(string doorID)
    {
        lastDoorUsed = doorID;
    }

    public string GetLastDoorUsed()
    {
        return lastDoorUsed;
    }

    private void OnEnable()
    {
        sceneEventChannel.OnOpenDoorRequested += HandleSceneChangeRequest;
    }

    private void OnDisable()
    {
        sceneEventChannel.OnOpenDoorRequested -= HandleSceneChangeRequest;
    }

    private void HandleSceneChangeRequest(string scene)
    {
        StartCoroutine(ChangeScene(scene));
    }
    private IEnumerator ChangeScene(string scene)
    {
        yield return StartCoroutine(screenFader.FadeOut());

        SceneManager.LoadScene(scene);

        yield return null;

        yield return StartCoroutine(screenFader.FadeIn());
    }
}
