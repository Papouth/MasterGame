using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generateur : CustomsTriggers
{
    #region Variable

    [HideInInspector] public bool valid;

    [SerializeField] private MeshRenderer meshIndicateur;
    [SerializeField] private Material materialToReplace;

    #endregion

    #region Built in methods
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        if (meshIndicateur == null) Debug.LogError("NO MESH INDICATEUR");
    }
    
    #endregion

    #region Customs Methods

    public override void Interact()
    {
        valid = true;

        Material[] materials = meshIndicateur.materials;
        materials[1] = materialToReplace;
        meshIndicateur.materials = materials; //A modifier selon modèle

    }

    #endregion
}
