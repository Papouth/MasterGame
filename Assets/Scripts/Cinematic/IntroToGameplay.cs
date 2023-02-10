using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroToGameplay : MonoBehaviour
{
    public string sceneGameplay;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        SceneManager.LoadScene(sceneGameplay);
    }
}
