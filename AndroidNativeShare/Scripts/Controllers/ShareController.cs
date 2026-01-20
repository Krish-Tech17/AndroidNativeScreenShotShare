using UnityEngine;

public class ShareController : MonoBehaviour
{
    private ScreenshotService screenshotService;
    private IShareService shareService;

    private void Awake()
    {
        screenshotService = gameObject.AddComponent<ScreenshotService>();
        shareService = new AndroidShareService();
    }

    public void ShareCurrentView()
    {
        Debug.Log("ShareCurrentView method called.");
        StartCoroutine(
            screenshotService.CaptureScreenshot(
                (imagePath) =>
                {
                     if (string.IsNullOrEmpty(imagePath))
                    {
                        Debug.LogError("ShareController: Screenshot capture failed.");
                        return;
                    }
                    
                    Debug.Log($"ShareController: Screenshot captured at {imagePath}. Initiating share...");
                    shareService.ShareImage(imagePath);
                }
            )
        );
    }
}
