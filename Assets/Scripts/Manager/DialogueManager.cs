using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager InstanceDialogue { get; set; }
    
    private Queue<string> sentences;
    public float speedDisplay;

    public PlayerInput playerInput;


    void Awake()
    {
        InstanceDialogue = this;
        sentences = new Queue<string>();
    }


    #region Dialogue

    /// <summary>
    /// Appelle un dialogue
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();
        if(dialogue.sentences.Length == 0) return;
        UIManager.UIInstance.EnableTextDialogue(true);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentece(dialogue);
    }

    /// <summary>
    /// Display the next sentences
    /// </summary>
    /// <param name="dialogue"></param>
    public void DisplayNextSentece(Dialogue dialogue)
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, dialogue));
    }

    /// <summary>
    /// Display the letter in the sentences
    /// </summary>
    /// <param name="sentence"></param>
    /// <param name="dialogue"></param>
    /// <returns></returns>
    public IEnumerator TypeSentence(string sentence, Dialogue dialogue)
    {
        string currentSentence = "";

        foreach (char letter in sentence.ToCharArray())
        {
            currentSentence += letter;
            UIManager.UIInstance.MajTextDialogue(currentSentence);
            yield return new WaitForSeconds(speedDisplay);
        }

        //yield return WaitForKeyDown(playerInput.);
        yield return new WaitForSeconds(2f);

        DisplayNextSentece(dialogue);
    }

    /// <summary>
    /// wait for a key press to next display sentence
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaitForKeyDown(bool canDestroy)
    {
        while (!canDestroy)
        {
            Debug.Log("Waiting key");
            yield return null;
        }
    }

    /// <summary>
    /// End the dialogues
    /// </summary>
    public void EndDialogue()
    {
        if(UIManager.UIInstance == null) return;
        UIManager.UIInstance.EnableTextDialogue(false);
    }
}

#endregion


[System.Serializable]
public class Dialogue
{
    public string npcName;
    public Sprite npcNameImage;

    [TextArea(3, 10)]
    public string[] sentences;
}