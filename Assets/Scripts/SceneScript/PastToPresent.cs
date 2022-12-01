using UnityEngine;
using UnityEngine.SceneManagement;


public class PastToPresent : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Rigidbody rb; 

    [Header("Scenes Infos")]
    [SerializeField] private bool isPresent;

    [Header("Player Components")]
    private PlayerTemporel playerTemporel;


    #region Built In Methods
    private void Awake()
    {
        playerTemporel = FindObjectOfType<PlayerTemporel>();

        rb = GetComponent<Rigidbody>();

        // On commence dans le présent
        isPresent = true;
    }

    private void Update()
    {
        SceneFinder();

        PushOnOff();
    }
    #endregion


    /// <summary>
    /// Détermine la temporalité dans laquelle le joueur se trouve actuellement
    /// </summary>
    private void SceneFinder()
    {
        // sceneState == false -> on est dans le présent
        // sceneState == true -> on est dans le passé

        if (!playerTemporel.sceneState)
        {
            //Debug.Log("On est dans le présent");
            isPresent = true;
        }
        else if (playerTemporel.sceneState)
        {
            //Debug.Log("On est dans le passé");
            isPresent = false;
        }
    }


    /// <summary>
    /// Fais en sorte que le joueur puisse ou non pousser l'objet en fonction de la temporalité dans laquelle il se trouve
    /// </summary>
    private void PushOnOff()
    {
        if (isPresent) rb.isKinematic = true;
        else if (!isPresent) rb.isKinematic = false;
    }
}