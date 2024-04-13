using UnityEngine;

public class SpriteRenderingModeSwitcher : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Material opaqueMaterial;
    public Material transparentMaterial;

    void Start()
    {
        // Log information about the connected Sprite Renderer component
        Debug.Log("Connected Sprite Renderer: " + spriteRenderer.gameObject.name);
    }

    // Function to switch rendering mode to opaque
    public void SwitchToOpaque()
    {
        spriteRenderer.material = opaqueMaterial;
        Debug.Log("Switched to opaque rendering mode.");
    }

    // Function to switch rendering mode to transparent
    public void SwitchToTransparent()
    {
        spriteRenderer.material = transparentMaterial;
        Debug.Log("Switched to transparent rendering mode.");
    }
}