using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using System.Collections;

public class Chat : MonoBehaviour
{
    public TextAsset inkJSON;
    public TextMeshProUGUI chatTextUI;
    public GameObject chatPanel;
    public GameObject optionPanel;
    public Button[] optionButtons;
    public ScrollRect scrollRect;
    public Scrollbar scrollbar;
    public CoinPopUp coinPopUp;
    private int coinsEarned = 0;

    private Story story;
    private bool isChatActive;

    private void Awake()
    {
        try
        {
            story = new Story(inkJSON.text);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error creating Story: " + e.Message);
            return;
        }

        Refresh();
    }
    private void Update()
    {
        Refresh();
    }

    public void SetChatStatus(bool isActive)
    {
        isChatActive = isActive;
        Refresh();
    }

    public void AppendTextToChat(string text)
    {
        chatTextUI.text += text + "\n";
        ScrollToBottom();
    }

    public void ClearChat()
    {
        chatTextUI.text = "";
        ScrollToBottom();
    }

    private void Refresh()
    {
        chatPanel.SetActive(isChatActive);
        optionPanel.SetActive(isChatActive && story.currentChoices.Count > 0);

        if (isChatActive)
        {
            while (story.canContinue)
            {
                string text = story.Continue();
                AppendTextToChat(text);

                coinsEarned = text.Contains("Udah ku transfer ya dek") ? 5 : (text.Contains("Penipu ya lu????") ? -2 : 0);

                if (coinsEarned != 0)
                {
                    coinPopUp.ShowPopUp(coinsEarned);
                }
            }
        }

        for (int i = 0; i < optionButtons.Length; i++)
        {
            bool showButton = i < story.currentChoices.Count;
            optionButtons[i].gameObject.SetActive(showButton);

            if (showButton)
            {
                var choice = story.currentChoices[i];
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.text;

                int capturedIndex = i; // Create a local variable to capture the correct index

                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => MakeChoice(capturedIndex)); // Use the captured index in the lambda expression
            }
        }

        ScrollToBottom();
    }

    public void MakeChoice(int choiceIndex)
    {
        Debug.Log("choice index: " + choiceIndex);
        ClearChat();
        if (choiceIndex >= 0 && choiceIndex < story.currentChoices.Count)
        {
            story.ChooseChoiceIndex(choiceIndex);
            //coinsEarned = story.currentChoices[choiceIndex].text.Contains("Udah ku transfer ya dek") ? 5 : -2;
            //coinsEarned = chatTextUI.text.Contains("Udah ku transfer ya dek") ? 5 : -2;
            //Debug.Log("coinsEarned: " + coinsEarned);

            //if (coinsEarned > 0)
            //{
            //    coinPopUp.ShowPopUp(coinsEarned);
            //}
        }
        else
        {
            Debug.LogError("Invalid choice index: " + choiceIndex);
        }

        Refresh(); // Call Refresh() method to update the UI after making a choice
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        //scrollRect.verticalNormalizedPosition = 0f;
    }
}
