using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShareOnSocialMedia : MonoBehaviour
{
    public GameObject sharePopUp;
    public Button shareButton;

    private void Start()
    {
        shareButton.onClick.AddListener(ShowSharePopUp);
    }

    public void ShowSharePopUp()
    {
        sharePopUp.SetActive(true);
    }

    public void CloseSharePopUp()
    {
        sharePopUp.SetActive(false);
    }

    public void ShareScreenshotToInstagram()
    {
        StartCoroutine(CaptureScreenshotAndShareToInstagram());
    }

    private IEnumerator CaptureScreenshotAndShareToInstagram()
    {
        // Delay a frame to allow the UI to update
        yield return null;

        // Take a screenshot of the game
        string screenshotFileName = "AroundUs.png";
        string screenshotPath = Application.persistentDataPath + "/" + screenshotFileName;
        ScreenCapture.CaptureScreenshot(screenshotFileName);

        // Wait until the screenshot is saved
        while (!System.IO.File.Exists(screenshotPath))
        {
            yield return null;
        }

        // Share the screenshot to Instagram using the Android plugin
        string shareMessage = "Check out my game screenshot!";
        using (AndroidJavaClass shareHelperClass = new AndroidJavaClass("com.yourcompany.androidplugin.ShareHelper"))
        {
            shareHelperClass.CallStatic("ShareScreenshotToInstagram", screenshotPath, shareMessage);
        }

        // Close the share pop-up after sharing
        CloseSharePopUp();
    }
}
