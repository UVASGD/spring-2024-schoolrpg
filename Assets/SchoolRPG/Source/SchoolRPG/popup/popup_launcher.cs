using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using Vector2 = System.Numerics.Vector2;


public class popup_launcher : MonoBehaviour
{
    public GameObject gameObject; 
    // Reference to the pop-up window GameObject

    public void start()
    {
        gameObject.transform.localPosition = new Vector3(1f, 1f,0);
    }

    // Method to hide the pop-up window
    public void ShowPopUp()
    {
        gameObject.SetActive(true);
        
        
        

    }

    public  void HidePopUp()
    {
        gameObject.SetActive(false);
        // Deactivate the pop-up windowsSquare.SetActive(false);
        
        // Alternatively, you can destroy the pop-up window:
        // Destroy(popUpWindow);
    }
}


