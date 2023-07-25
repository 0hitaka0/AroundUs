using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public SignalSender contextOn;
    public SignalSender contextOff;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the dialog box initially when the game starts
        HideDialogBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            ToggleDialogBox();
        }
    }

    void ToggleDialogBox()
    {
        // Toggle the dialog box visibility and update the dialog text
        dialogBox.SetActive(!dialogBox.activeSelf);
        if (dialogBox.activeSelf)
        {
            dialogText.text = dialog;
        }
    }

    public void HideDialogBox()
    {
        // Hide the dialog box and reset the dialog text
        dialogBox.SetActive(false);
        dialogText.text = "";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            contextOff.Raise();
            playerInRange = false;
            HideDialogBox();
        }
    }
}
