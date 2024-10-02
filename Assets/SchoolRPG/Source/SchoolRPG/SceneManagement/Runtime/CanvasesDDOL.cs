using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasesDDOL : MonoBehaviour
{
    // just a script to make sure the canvas children are all DDOL
    public static CanvasesDDOL Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
