using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SchoolRPG.ProgressTracker.Runtime;

public class Stair : MonoBehaviour
{

    [SerializeField]
    private ProgressTracker progressTracker;

    [SerializeField]
    private int level; // Stair Id

    // Start is called before the first frame update
    void Start()
    {
        // if level completed, remove blocker
        if (progressTracker.levelTracker[level])
        {
            Debug.Log("Staircase unblocked on start");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
