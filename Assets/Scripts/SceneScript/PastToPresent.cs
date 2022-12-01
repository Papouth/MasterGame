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

        // On commence dans le pr�sent
        isPresent = true;
    }

    private void Update()
    {
        SceneFinder();

        PushOnOff();
    }
    #endregion


    /// <summary>
    /// D�termine la temporalit� dans laquelle le joueur se trouve actuellement
    /// </summary>
    private void SceneFinder()
    {
        // sceneState == false -> on est dans le pr�sent
        // sceneState == true -> on est dans le pass�

        if (!playerTemporel.sceneState)
        {
            //Debug.Log("On est dans le pr�sent");
            isPresent = true;
        }
        else if (playerTemporel.sceneState)
        {
            //Debug.Log("On est dans le pass�");
            isPresent = false;
        }
    }


    /// <summary>
    /// Fais en sorte que le joueur puisse ou non pousser l'objet en fonction de la temporalit� dans laquelle il se trouve
    /// </summary>
    private void PushOnOff()
    {
        if (isPresent) rb.isKinematic = true;
        else if (!isPresent) rb.isKinematic = false;
    }
}