using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraController : MonoBehaviour
{
    public Transform player;
    public SceneManager sceneManager;
    // For now, just follows player
    

    private static CameraController instance;
    void Awake()
    {
        /*List<string> cutscenes = new List<string>{ "Final Class", "Letter Scene", "Hospital" };
        if (cutscenes.Contains(SceneManager.GetActiveScene().name))
        {
            Destroy(gameObject);
            Debug.Log("Final Class Cam deleted");
        }
        else
        {*/
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
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (var source in allAudioSources)
        {
            Debug.Log(source);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + new Vector3(0, 0, -8);
    }
}
