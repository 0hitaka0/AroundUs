using UnityEngine;
using TMPro;
using System.Collections;

public class Smishing : MonoBehaviour
{
    public TextAsset inkJSON;
    public Phone phone;
    public CoinPopUp coinPopUp;
    public BadgePopUp badgePopUp;
    public Chat chat;

    private InkyDialogue inkyDialogue;
    private bool isPhoneNumberCorrect;
    private bool isAnswerCorrect;

    private void Start()
    {
        inkyDialogue = new InkyDialogue(inkJSON);
        isPhoneNumberCorrect = false;
        isAnswerCorrect = false;
    }

    public void OnSubmitPhoneNumber()
    {
        if (phone.IsPhoneNumberCorrect)
        {
            isPhoneNumberCorrect = true;
            StartDialogue();
        }
    }

    public void OnOptionSelected(string option)
    {
        if (isPhoneNumberCorrect && !isAnswerCorrect)
        {
            string response = inkyDialogue.ContinueDialogue(option);
            ProcessResponse(response);
        }
    }

    public void OnContinueButtonClicked()
    {
        badgePopUp.ShowBadgePopUp("New Badge");
    }

    private void StartDialogue()
    {
        string response = inkyDialogue.ContinueDialogue("");
        ProcessResponse(response);
    }

    private void ProcessResponse(string response)
    {
        switch (response)
        {
            case "why":
                chat.AppendTextToChat("[Assalammu'alaikum kak..]");
                break;
            case "want":
                chat.AppendTextToChat("[Kak tolong transferin uang]");
                break;
            case "transfer":
                chat.AppendTextToChat("[Aku butuh uang kak, tolong transferin]");
                break;
            case "swim":
                chat.AppendTextToChat("[Mau bayar renang kak]");
                break;
            case "sus":
                chat.AppendTextToChat("[Bukan kak, ini nanaaa]");
                break;
            case "thanks":
                chat.AppendTextToChat("[100.000]");
                break;
            case "END":
                break;
        }
    }
}
