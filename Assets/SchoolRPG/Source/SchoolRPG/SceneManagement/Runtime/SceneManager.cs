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
    private GameObject player;

    [SerializeField] private SceneEventChannel sceneEventChannel;
    [SerializeField] private ScreenFader screenFader;
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
        player = GameObject.FindGameObjectWithTag("Player");
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
        player.SetActive(false);// going into the door & disabling movement
        yield return StartCoroutine(screenFader.FadeOut());
        SaveData.SaveGame();
        SceneManager.LoadScene(scene);
        player.SetActive(true); // do I need this? Included just in case
        SaveData.LoadGame();
        yield return null;

        yield return StartCoroutine(screenFader.FadeIn());
        SaveData.SaveGame();// save the scene in case player dies
    }

    public IEnumerator ReloadSceneOnKill(string scene)
    {
        GameObject spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return StartCoroutine(screenFader.FadeOut());

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(scene);
        player.transform.position = spawnPoint.transform.position;
        player.GetComponent<SpriteRenderer>().enabled = true;
        yield return StartCoroutine(screenFader.FadeIn());
    }
}
