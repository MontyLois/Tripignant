using UnityEngine;

namespace PhotoSystem
{
    public class PhotoCapture : MonoBehaviour, IUsable
    {
        //camera from where to take the photo
        public Camera polaroidCamera;
        //Texture of the photo
        public RenderTexture renderTexture;
        [SerializeField]
        public int heighDistance;

        public Transform cameraPosition;
        public Transform playercameraPostion;
    
        void TakePhoto()
        {
            Debug.Log("taking photo");
            // Temporarily store the current RenderTexture active
            RenderTexture currentRT = RenderTexture.active;

            // Set the camera's target texture and activate it
            polaroidCamera.targetTexture = renderTexture;
            RenderTexture.active = renderTexture;

            // Create a new Texture2D with the same dimensions as the Render Texture
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

            // Render the camera's view
            polaroidCamera.Render();

            // Read the pixels from the Render Texture into the Texture2D
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();

            // Reset the RenderTexture.active and camera target texture
            RenderTexture.active = currentRT;
            polaroidCamera.targetTexture = null;
        
        }

        private void move()
        {
            Vector3 position = new Vector3(playercameraPostion.position.x, playercameraPostion.position.y - heighDistance,
                playercameraPostion.position.z);
            cameraPosition.SetPositionAndRotation(position, playercameraPostion.rotation);
        }

        // Example: Capture the screen when pressing the "R" key
        void Update()
        {
            move();
            /*
            if (Input.GetKeyDown(KeyCode.R))
            {
                TakePhoto();
            }*/
        }

        public void Use()
        {
            TakePhoto();
        }
    }
}