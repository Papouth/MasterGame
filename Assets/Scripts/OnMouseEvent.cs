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
    [SerializeField] private Material selectedMat;
    private Material storedMat;
    [SerializeField] private LayerMask telekinesyLayer;
    #endregion


    private void Awake()
    {
        objectRend = GetComponent<Renderer>();
        storedMat = objectRend.material;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        objectRend.material = selectedMat;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        objectRend.material = storedMat;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerInput.telekinesyKeyOn)
        {
            PlayerTelekinesie.telekinesyObject = gameObject;
        }
    }
}