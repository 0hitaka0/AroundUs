using UnityEngine;
using TMPro;
using System.Collections;

public class Phone : MonoBehaviour
{
    public string correctPhoneNumber = "081234567890";
    public TextMeshProUGUI inputStatusText;
    public TMP_InputField phoneNumberInput;
    public GameObject inkyDisplayPanel;
    public GameObject optionPanel;
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalInputFieldPosition;
    private bool isShaking;

    public Chat chat;
    public bool IsPhoneNumberCorrect { get; private set; }

    private void Start()
    {
        originalInputFieldPosition = phoneNumberInput.transform.localPosition;
    }

    private void Update()
    {
        // Check if the Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmitPhoneNumber();
        }
    }

    public void OnSubmitPhoneNumber()
    {
        string inputPhoneNumber = phoneNumberInput.text;

        if (IsCorrectPhoneNumber(inputPhoneNumber))
        {
            // Correct phone number
            inputStatusText.text = "Nomor telepon benar";
            inkyDisplayPanel.SetActive(true);
            optionPanel.SetActive(true);

            // Set phone number status in the Chat script
            chat.SetChatStatus(true);
        }
        else
        {
            // Incorrect phone number
            inputStatusText.text = "Nomor telepon salah";
            ShakeInputField();
            inkyDisplayPanel.SetActive(false);
            optionPanel.SetActive(false);
            ResetPhoneNumberInput();

            // Set phone number status in the Chat script
            chat.SetChatStatus(false);
        }
    }

    private bool IsCorrectPhoneNumber(string phoneNumber)
    {
        return phoneNumber.Length == 12 && string.Equals(phoneNumber, correctPhoneNumber);
    }

    private void ShakeInputField()
    {
        if (!isShaking)
        {
            isShaking = true;
            StartCoroutine(ShakeCoroutine());
        }
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0f;
        Vector3 startPosition = phoneNumberInput.transform.localPosition;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            phoneNumberInput.transform.localPosition = startPosition + new Vector3(x, y, phoneNumberInput.transform.localPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        phoneNumberInput.transform.localPosition = originalInputFieldPosition;
        isShaking = false;
    }

    private void ResetPhoneNumberInput()
    {
        phoneNumberInput.text = string.Empty;
        phoneNumberInput.ActivateInputField();
    }
}
