using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickCatcher : MonoBehaviour, IPointerClickHandler
{
    public ImageZoom imageZoom;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (imageZoom != null && imageZoom.IsZoomed())
        {
            imageZoom.ForceZoomOut();
        }
    }
}