using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager UIInstance;

    public GameObject dialogueUI;

    public TextMeshProUGUI dialogueText;

    public void MajTextDialogue(string sentences)
    {
        dialogueText.text = sentences;
    }

    public void EnableTextDialogue(bool enabled)
    {
        dialogueUI.SetActive(enabled);
    }

}