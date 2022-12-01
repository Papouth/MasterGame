using UnityEngine;
using UnityEngine.SceneManagement;


public class PastToPresent : MonoBehaviour
{
    #region Variables
    [Header("Component")]
    [SerializeField] private Rigidbody rb;

    [Tooltip("Le prefab du passé")]
    public GameObject pastPrefab;

    [Tooltip("Le prefab du présent")]
    public GameObject presentPrefab;


    [Header("Scenes Infos")]
    [SerializeField] private bool isPresent;
    private bool prefabState;

    [Header("Player Components")]
    private PlayerTemporel playerTemporel;
    #endregion

    #region Built In Methods
    private void Awake()
    {
        playerTemporel = FindObjectOfType<PlayerTemporel>();

        rb = GetComponent<Rigidbody>();

        // On commence dans le présent
        isPresent = true;

        if (prefabState)
        {
            // On désactive le prefab du passé
            pastPrefab.SetActive(false);
            prefabState = !prefabState;
        }
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


            // Si je n'ai pas encore modifier le prefab
            if (prefabState)
            {
                pastPrefab.SetActive(false);
                presentPrefab.SetActive(true);
                prefabState = !prefabState;
            }
        }
        else if (playerTemporel.sceneState)
        {
            //Debug.Log("On est dans le passé");
            isPresent = false;


            // Si je n'ai pas encore modifier le prefab
            if (!prefabState)
            {
                presentPrefab.SetActive(false);
                pastPrefab.SetActive(true);
                prefabState = !prefabState;
            }
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