using UnityEngine;
using TMPro;

public class BadgePopUp : MonoBehaviour
{
    public GameObject popUpPanel;
    public TextMeshProUGUI badgeText;
    public GameObject continueButton;

    public void ShowBadgePopUp(string badgeName)
    {
        badgeText.text = badgeName;
        popUpPanel.SetActive(true);
    }

    public void ContinueButtonClicked()
    {
        popUpPanel.SetActive(false);
    }
}
