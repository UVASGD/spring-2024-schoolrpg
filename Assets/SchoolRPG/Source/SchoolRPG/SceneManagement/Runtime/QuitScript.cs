using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitScript : MonoBehaviour
{
    public void Quit()
    {
        // Logs the quit event in the editor
        Debug.Log("Game is quitting...");

        // If the game is running in the Unity editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops the game in the editor
        #else
        Application.Quit(); // Quits the application in a build
        #endif
    }
}
