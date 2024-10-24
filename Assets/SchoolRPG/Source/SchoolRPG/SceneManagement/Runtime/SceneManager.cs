using SchoolRPG.Character.Runtime;
using SchoolRPG.SceneManagement.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    //singleton
    public static SceneManagerScript instance = null;

    private string lastDoorUsed = "";

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
        screenFader = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<ScreenFader>();
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
        sceneEventChannel.OnPlayerDeathReload += HandlePlayerDeathReloadScene;
    }

    private void OnDisable()
    {
        sceneEventChannel.OnOpenDoorRequested -= HandleSceneChangeRequest;
        sceneEventChannel.OnPlayerDeathReload -= HandlePlayerDeathReloadScene;

    }

    private void HandleSceneChangeRequest(string scene)
    {
        StartCoroutine(ChangeScene(scene, true));
    }

    private void HandlePlayerDeathReloadScene(string scene)
    {
        StartCoroutine(ChangeScene(scene, false));
    }

    /// <summary>
    /// Handles scene changes. Will do fade in and fade out, and save the game state before loading into the new scene, where it will load the data into the new one. Handles scene reloads by making the loaded scene the same as the current one and setting "save" to false. 
    /// </summary>
    /// <param name="scene">The scene name. Use the same scene name for scene reloads. </param>
    /// <param name="save"> Whether to make this act as a scene reload or as a scene transition. If save == false, the scene name needs to be the same, otherwise items collected in previous scenes, etc. will not carry over to the new scene.</param>
    /// <returns></returns>
    private IEnumerator ChangeScene(string scene, bool save)
    {
        screenFader = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<ScreenFader>();
        yield return StartCoroutine(screenFader.FadeOut());
        if (save) SaveData.SaveGame();
        SceneManager.LoadScene(scene);

        SaveData.LoadGame();
        screenFader = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<ScreenFader>();

        CharacterMovementComponent playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovementComponent>();
        playerMovement.Activate();
        playerMovement.Move(Vector2.zero);

        yield return null;

        //yield return StartCoroutine(screenFader.FadeIn());
    }

    // For changing scenes without using the save data
    public IEnumerator PlayerlessChangeScene(string scene)
    {
        screenFader = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<ScreenFader>();
        yield return StartCoroutine(screenFader.FadeOut());
        SceneManager.LoadScene(scene);
        
        screenFader = GameObject.FindGameObjectWithTag("FadeCanvas").GetComponent<ScreenFader>();
   

        yield return null;
        //yield return StartCoroutine(screenFader.FadeIn());
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
