using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCarryover : MonoBehaviour
{
    private bool carried = false;
    // Carry over only for one scene.
    private static AudioCarryover instance;
    void Awake()
    {
        if (!carried)
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
            carried = true;
        }
    }


}
