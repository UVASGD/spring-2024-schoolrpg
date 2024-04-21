using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using Vector2 = System.Numerics.Vector2;

namespace pop_up_launcher{
public class popup_launcher : MonoBehaviour
{
    // Reference to the pop-up window GameObject
    
    public static GameObject Player = GameObject.Find("Player"); 
    
    // Method to hide the pop-up window
    public void ShowPopUp()
    {
        GameObject.SetActive(true);
        
        
        GameObject.Transform.localPosition.x = 1f;
        GameObject.Transform.localPosition.y = 1f; 

    }

    public static void HidePopUp()
    {
        GameObject.SetActive(false);
        // Deactivate the pop-up windowsSquare.SetActive(false);
        
        // Alternatively, you can destroy the pop-up window:
        // Destroy(popUpWindow);
    }
}


}