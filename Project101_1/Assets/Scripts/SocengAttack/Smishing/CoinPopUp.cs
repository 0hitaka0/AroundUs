using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinPopUp : MonoBehaviour
{
    public GameObject coinEarnPopup;
    public Button continueButton;
    public GameObject badgePopup;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI coinText;

    private bool isPopUpVisible = false;

    public void ShowPopUp(int coinsEarned)
    {
        if (!isPopUpVisible)
        {
            coinEarnPopup.SetActive(true);
            StartCoroutine(BlinkContinueButton());

            if (coinsEarned > 0)
            {
                headerText.text = "Congrats!";
                coinText.text = "Earned " + coinsEarned + " coins!";
                continueButton.onClick.AddListener(() => proceedToBadgePopUp());
            }
            else if (coinsEarned < 0)
            {
                headerText.text = "Dang!";
                coinText.text = "Lost " + Mathf.Abs(coinsEarned) + " coins!";
                continueButton.onClick.AddListener(() => retryStage());
            }

            isPopUpVisible = true;
        }
    }

    private IEnumerator BlinkContinueButton()
    {
        while (isPopUpVisible)
        {
            //continueButton.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            //continueButton.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void proceedToBadgePopUp()
    {
        coinEarnPopup.SetActive(false);
        badgePopup.SetActive(true);
        isPopUpVisible = false;
    }

    public void retryStage()
    {
        coinEarnPopup.SetActive(false);
        isPopUpVisible = false;
    }

}
