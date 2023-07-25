using System.Collections;
using UnityEngine;

public class SharePopUp : MonoBehaviour
{
    public GameObject sharePanel;

    public void ShowPopUp()
    {
        StartCoroutine(ShowPopUpCoroutine());
    }

    private IEnumerator ShowPopUpCoroutine()
    {
        // Show the share pop-up
        sharePanel.SetActive(true);

        // Wait until the share pop-up is closed
        yield return new WaitUntil(() => !sharePanel.activeSelf);

        // Perform any actions after the share pop-up is closed
        // For example, you can resume the game or show another pop-up
    }
}
