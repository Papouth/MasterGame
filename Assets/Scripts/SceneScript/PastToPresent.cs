using UnityEngine;
using UnityEngine.SceneManagement;


public class PastToPresent : MonoBehaviour
{
    #region Variables
    [Header("Component")]
    [SerializeField] private Rigidbody rb;
    [HideInInspector]
    public bool canLift;

    [Tooltip("Le prefab du pass�")]
    public GameObject pastPrefab;

    [Tooltip("Le prefab du pr�sent")]
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
        if (rb == null) rb = GetComponentInChildren<Rigidbody>();

        // On commence dans le pr�sent
        isPresent = true;

        /*
        if (prefabState)
        {
            // On d�sactive le prefab du pass�
            pastPrefab.SetActive(false);
            prefabState = !prefabState;
        }
        */
    }

    private void Start()
    {
        if (isPresent)
        {
            pastPrefab.SetActive(false);
            presentPrefab.SetActive(true);
        }
    }

    private void Update()
    {
        SceneFinder();

        PushOnOff();

        LiftOnOff();
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
            //Debug.Log("On est dans le pass�");
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
    /// Le joueur peut pousser l'objet dans le pass�
    /// </summary>
    private void PushOnOff()
    {
        if (isPresent) rb.isKinematic = true;
        else if (!isPresent && !canLift) rb.isKinematic = false;
    }

    /// <summary>
    /// Le joueur ne peux pas porter l'objet dans le pr�sent
    /// </summary>
    private void LiftOnOff()
    {
        if (isPresent) canLift = false;
        else if (!isPresent) canLift = true;
    }
}