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
    public static void ShowPopUp()
    {
        GameObject.SetActive(true);
        private Vector2 _squareYPosition = new Vector2( Player.transform.position.y+ 1f);
        
        GameObject.transform.position.x = Player.transform.position.x;
        GameObject.transform.position.y = Square_y_position; 

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