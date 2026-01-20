using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class ScreenshotService : MonoBehaviour
{
    public IEnumerator CaptureScreenshot(Action<string> onCompleted)
    {
        // Wait for end of frame to ensure UI is rendered
        yield return new WaitForEndOfFrame();

        try
        {
            // Create a texture the size of the screen, RGB24 format
            int width = Screen.width;
            int height = Screen.height;
            Texture2D screenshotTexture = new Texture2D(width, height, TextureFormat.RGB24, false);

            // Read screen contents into the texture
            screenshotTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshotTexture.Apply();

            // Encode texture to PNG
            byte[] bytes = screenshotTexture.EncodeToPNG();
            Destroy(screenshotTexture);

            // Define path
            string fileName = $"AR_Screenshot_{DateTime.Now.Ticks}.png"; 
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            // Write to file
            File.WriteAllBytes(filePath, bytes);
            
            Debug.Log($"Screenshot saved to: {filePath}");

            // Callback
            onCompleted?.Invoke(filePath);
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to capture screenshot: {e.Message}");
            onCompleted?.Invoke(null);
        }
    }
}
