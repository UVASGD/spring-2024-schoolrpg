using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //singleton
    public static SceneManager instance = null;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null){
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
