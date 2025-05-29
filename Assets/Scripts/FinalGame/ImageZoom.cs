using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageZoom : MonoBehaviour, IPointerClickHandler
{
    private RectTransform rectTransform;
    private Vector2 originalSize;
    private Vector2 originalPosition;
    private bool isZoomed = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.sizeDelta;
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isZoomed)
        {
            ZoomIn();
        }
        else
        {
            ZoomOut();
        }
    }

    void ZoomIn()
    {
        rectTransform.sizeDelta = new Vector2(Screen.width * 0.3f, Screen.height * 0.65f);
        rectTransform.anchoredPosition = Vector2.zero;
        isZoomed = true;
    }

    void ZoomOut()
    {
        rectTransform.sizeDelta = originalSize;
        rectTransform.anchoredPosition = originalPosition;
        isZoomed = false;
    }

    public bool IsZoomed()
    {
        return isZoomed;
    }

    public void ForceZoomOut()
    {
        ZoomOut();
    }
}