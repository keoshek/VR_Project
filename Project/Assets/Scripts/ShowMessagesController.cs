using TMPro;
using UnityEngine;

public class ShowMessagesController : MonoBehaviour
{
    [Tooltip("The text mesh the message is output to")]
    [SerializeField] private TextMeshProUGUI messageOutput = null;

    [Tooltip("The list of messages that are shown")]
    [TextArea][SerializeField] private string[] messages;

    private int index = 0;

    public void NextMessage()
    {
        int newIndex = ++index % messages.Length;

        if (newIndex >= index)
        {
            ShowMessage();
        }
    }

    public void PreviousMessage()
    {
        index = --index % messages.Length;
        ShowMessage();
    }

    private void ShowMessage()
    {
        messageOutput.text = messages[Mathf.Abs(index)];
    }

    public void ShowMessageAtIndex(int value)
    {
        index = value;
        ShowMessage();
    }
}
