using UnityEngine;

namespace SchoolRPG.Camera.Runtime
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;
        // For now, just follows player
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = player.transform.position + new Vector3(0, 0,-8);
        }
    }
}
