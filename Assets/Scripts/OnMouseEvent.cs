using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Ce script sert majoritairement pour la telekinesy
/// </summary>
public class OnMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    #region Variables
    [Header("Telekinesy Parameters")]
    private Renderer objectRend;
    [Tooltip("Matériau affiché au passage de la souris")]
    [SerializeField] private Material mouseOverMat;
    [Tooltip("Matériau affiché après sélection")]
    [SerializeField] private Material selectedMat;
    private Material storedMat;
    private bool onSelect;
    #endregion


    private void Awake()
    {
        objectRend = GetComponent<Renderer>();
        storedMat = objectRend.material;
    }

    private void Update()
    {
        Reset();
    }

    private void Reset()
    {
        if (PlayerTelekinesie.telekinesyObject != gameObject)
        {
            onSelect = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!onSelect)
        {
            objectRend.material = mouseOverMat;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!onSelect)
        {
            objectRend.material = storedMat;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerInput.telekinesyKeyOn)
        {
            onSelect = !onSelect;

            if (onSelect)
            {
                PlayerTelekinesie.telekinesyObject = gameObject;
                objectRend.material = selectedMat;
            }
            else if (!onSelect)
            {
                PlayerTelekinesie.telekinesyObject = null;
                objectRend.material = mouseOverMat;
            }
        }
    }
}