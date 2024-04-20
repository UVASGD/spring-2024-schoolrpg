using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTest : MonoBehaviour
{
    [SerializeField] private PuzzleScriptableObject puzzleScriptableObject;

    private void Start() {
        Debug.Log(puzzleScriptableObject.completion[0]);
    }

}
