using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Share : MonoBehaviour
{
    private const string InstagramPackageName = "com.instagram.android";

    public string screenshotFileName = "aroundus.png";
    public string shareCaption = "Mereka di sekelilingmu";

    public void ShareScreenshot()
    {
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        byte[] screenshotBytes = screenshotTexture.EncodeToPNG();
        Destroy(screenshotTexture);

        string screenshotPath = Application.persistentDataPath + "/" + screenshotFileName;
        System.IO.File.WriteAllBytes(screenshotPath, screenshotBytes);

        string uri = "file://" + screenshotPath;
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);

                string filePath = Application.persistentDataPath + "/sharedImage.png";
                System.IO.File.WriteAllBytes(filePath, texture.EncodeToPNG());

                string instagramPackageUri = "instagram://library?AssetPath=" + filePath;

                // Check if Instagram app is installed
                if (IsPackageInstalled(InstagramPackageName))
                {
                    // Open Instagram app with the shared image
                    Application.OpenURL(instagramPackageUri);
                }
                else
                {
                    // Instagram app is not installed, show a message or redirect to the Instagram website
                    Debug.Log("Instagram app is not installed.");
                }
            }
            else
            {
                Debug.Log("Failed to load screenshot: " + www.error);
            }
        }
    }

    private bool IsPackageInstalled(string packageName)
    {
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject launchIntent = null;

        // Get the Launch Intent for the package
        if (packageManager != null)
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", packageName);
        }

        return launchIntent != null;
    }
}
