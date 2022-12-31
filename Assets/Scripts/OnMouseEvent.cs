using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class OnMouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ENTER");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("EXIT");

    }
}
