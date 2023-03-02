using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroToGameplay : MonoBehaviour
{
    public string animIntro = "Anim_Intro 1";
    private PlayerTemporel playerTempo;


    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        playerTempo = FindObjectOfType<PlayerTemporel>();
        playerTempo.GetComponent<CharacterController>().enabled = true;


        playerTempo.ChangeStringName("Tutoriel_01_Passe", "Tutoriel_01_Present");

        SceneManager.LoadScene(playerTempo.present, LoadSceneMode.Additive);

        SceneManager.LoadScene(playerTempo.past, LoadSceneMode.Additive);

        playerTempo.ChangeSceneToLoad("Tutoriel_01_Passe", "Tutoriel_01_Present");


        SceneManager.UnloadSceneAsync(animIntro);
    }
}