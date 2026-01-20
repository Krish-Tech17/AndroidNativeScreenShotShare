using UnityEngine;

public class AndroidShareService : IShareService
{
    public void ShareImage(string imageFilePath)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
        using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent"))
        using (AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent"))
        {
            intent.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            intent.Call<AndroidJavaObject>("setType", "image/png");

            // Get File Object
            using (AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", imageFilePath))
            {
                // Get FileProvider URI
                // Authority must match the one defined in AndroidManifest.xml: ${applicationId}.provider
                string packageName = currentActivity.Call<string>("getPackageName");
                string authority = packageName + ".provider";

                using (AndroidJavaClass fileProviderClass = new AndroidJavaClass("androidx.core.content.FileProvider"))
                {
                    using (AndroidJavaObject uri = fileProviderClass.CallStatic<AndroidJavaObject>("getUriForFile", currentActivity, authority, fileObject))
                    {
                        intent.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uri);
                        
                        // Grant Read Permissions to the receiving app
                        intent.Call<AndroidJavaObject>("addFlags", intentClass.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION"));
                    }
                }
            }

            // Start Activity with Chooser
            using (AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intent, "Share image via"))
            {
                    currentActivity.Call("startActivity", chooser);
            }
        }
#endif
    }
}
