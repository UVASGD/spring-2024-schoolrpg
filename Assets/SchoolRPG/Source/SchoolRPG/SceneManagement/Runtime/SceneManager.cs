using SchoolRPG.SceneManagement.Runtime;
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
    private ScreenFader screenFader;
    [SerializeField] private SaveData SaveData;

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

    private void Start()
    {
        screenFader = GameObject.Find("FadeCanvas").GetComponent<ScreenFader>();
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
        Debug.Log(screenFader);
        yield return StartCoroutine(screenFader.FadeOut());
        SaveData.SaveGame();
        SceneManager.LoadScene(scene);
        SaveData.LoadGame();
        screenFader = GameObject.Find("FadeCanvas").GetComponent<ScreenFader>();
        Debug.Log(screenFader);
        yield return null;

        //yield return StartCoroutine(screenFader.FadeIn());
    }

    // For cutscenes, without save data

    public IEnumerator PlayerlessChangeScene(string scene)
    {
        yield return StartCoroutine(screenFader.FadeOut());
        SceneManager.LoadScene(scene);
        yield return null;

        yield return StartCoroutine(screenFader.FadeIn());
    }

    public void SimpleChangeScene(string scene)
    {
        StartCoroutine(PlayerlessChangeScene(scene));
    }

    public void SuddenChangeScene(string scene)
    {
        screenFader.SetFadeDuration(0f);
        StartCoroutine(PlayerlessChangeScene(scene));
        screenFader.SetFadeDuration(1f);
    }
}
