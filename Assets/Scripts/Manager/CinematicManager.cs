using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicManager : MonoBehaviour
{
    #region Variables

    public GameObject kali;
    public GameObject cam;

    public string startScene;
    //public string menuScene;

    #endregion
    

    private void SetUpKaliGameplay(bool enable)
    {
        kali.SetActive(enable);
        cam.SetActive(enable);
    }

    /// <summary>
    /// appele a la fin de une cinematique
    /// </summary>
    public void LoadFirstScene()
    {
        SceneManager.LoadSceneAsync(startScene, LoadSceneMode.Additive);
        SetUpKaliGameplay(true);
    }
}
