using UnityEngine;
using Ink.Runtime;

public class InkyDialogue
{
    private Story story;
    public string currentResponse { get; private set; }

    public InkyDialogue(TextAsset inkJSON)
    {
        story = new Story(inkJSON.text);
        currentResponse = string.Empty;
    }

    public string ContinueDialogue(string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            story.ChooseChoiceIndex(-1);
            story.ContinueMaximally();
            currentResponse = input;
            story.ChooseChoiceIndex(GetChoiceIndex(input));
        }
        else
        {
            story.ContinueMaximally();
            currentResponse = story.currentText;
        }

        return currentResponse;
    }

    private int GetChoiceIndex(string input)
    {
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            if (story.currentChoices[i].text.Contains(input))
            {
                return i;
            }
        }

        return -1;
    }
}
