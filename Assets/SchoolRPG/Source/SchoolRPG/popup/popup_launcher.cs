using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using Vector2 = System.Numerics.Vector2;

namespace pop_up_launcher{
public class popup_launcher : MonoBehaviour
{
    // Reference to the pop-up window GameObject
    public  GameObject Square;
    public static GameObject Player; 
    
    // Method to hide the pop-up window
    public void ShowPopUp()
    {
        private Vector2 Square_y_position = new Vector2( Player.transform.position.y+ 1f);
        Square.SetActive(true);
        Square.position.x = Player.transform.position.x;
        Square.position.y = Square_y_position; 

    }

    public void HidePopUp()
    {
        // Deactivate the pop-up window
        Square.SetActive(false);
        
        // Alternatively, you can destroy the pop-up window:
        // Destroy(popUpWindow);
    }
}


}