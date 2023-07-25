using UnityEngine;
using UnityEngine.UI;

public class ClosePopup : MonoBehaviour
{
    public GameObject CloseButton;
    bool active;
    public void OpenAndClose()
    {
        if (active == false)
        {
            CloseButton.transform.gameObject.SetActive(true);
            active = true;
        }
        else
        {
            CloseButton.transform.gameObject.SetActive(false);
            active = false;
        }
    }

}
