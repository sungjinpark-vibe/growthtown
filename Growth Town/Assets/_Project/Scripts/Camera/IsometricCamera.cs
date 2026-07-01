using UnityEngine;

namespace LifeTown.CameraSystem
{
    /// <summary>
    /// Sets up the camera to an Isometric angle (30 degrees X, 45 degrees Y) and orthographic projection.
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class IsometricCamera : MonoBehaviour
    {
        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            
            // Typical Isometric rotation
            transform.rotation = Quaternion.Euler(30f, 45f, 0f);
            
            // Ensure camera is orthographic for true isometric feel
            if (cam != null)
            {
                cam.orthographic = true;
            }
        }

        // Add additional panning/zooming logic as needed
    }
}
