using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSceneAssistant : MonoBehaviour
{

    public void StartSimpleSceneChange(string scene)
    {
        SceneManagerScript.instance.SimpleChangeScene(scene);
    }

    public void StartSuddenSceneChange(string scene)
    {
        SceneManagerScript.instance.SuddenChangeScene(scene);
    }
}
