using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }
        GameObject camera = GameObject.FindWithTag("MainCamera");
        if (camera != null)
        {
            Destroy(camera);
        }
        GameObject saveData = GameObject.Find("SaveDataObject");
        if (saveData != null)
        {
            Destroy(saveData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
