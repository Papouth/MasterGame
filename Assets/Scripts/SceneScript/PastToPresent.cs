using UnityEngine;
using UnityEngine.SceneManagement;


public class PastToPresent : MonoBehaviour
{
    #region Variables
    [Header("Component")]
    [SerializeField] private Rigidbody rb;
    [HideInInspector]
    public bool canLift;

    [Tooltip("Le prefab du passé")]
    public GameObject pastPrefab;

    [Tooltip("Le prefab du présent")]
    public GameObject presentPrefab;


    [Header("Scenes Infos")]
    [SerializeField] private bool isPresent;
    private bool prefabState;
    [Tooltip("La scène dans laquelle la caisse se trouve")]
    public string actualScene;
    private Scene scene;
    private bool alreadyCheck;


    [Header("Player Components")]
    private PlayerTemporel playerTemporel;
    #endregion

    #region Built In Methods
    private void Awake()
    {
        playerTemporel = FindObjectOfType<PlayerTemporel>();

        rb = GetComponent<Rigidbody>();
        if (rb == null) rb = GetComponentInChildren<Rigidbody>();

        // On commence dans le présent
        isPresent = true;
    }

    private void Start()
    {
        scene = SceneManager.GetSceneByName(actualScene);
        alreadyCheck = false;

        if (scene.isLoaded && isPresent)
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);

            pastPrefab.SetActive(false);
            presentPrefab.SetActive(true);
        }
    }

    private void Update()
    {
        if (!scene.isLoaded && !alreadyCheck)
        {
            alreadyCheck = true;
            gameObject.SetActive(false);
        }

        SceneFinder();

        PushOnOff();

        LiftOnOff();
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
    /// Le joueur peut pousser l'objet dans le passé
    /// </summary>
    private void PushOnOff()
    {
        if (isPresent) rb.isKinematic = true;
        else if (!isPresent && !canLift) rb.isKinematic = false;
    }

    /// <summary>
    /// Le joueur ne peux pas porter l'objet dans le présent
    /// </summary>
    private void LiftOnOff()
    {
        if (isPresent) canLift = false;
        else if (!isPresent) canLift = true;
    }
}