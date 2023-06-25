using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static Canvas canvas;
    public Image img;
    private RectTransform t;
    private CanvasGroup group;
    private Vector3 prev;
    public GameObject plantedPrefab;
    public TextMeshProUGUI species;

    private void Awake(){
        t = img.GetComponent<RectTransform>();
        group = img.GetComponent<CanvasGroup>();   
        prev = t.anchoredPosition;     
    }

    public void OnBeginDrag(PointerEventData pointer){
        group.blocksRaycasts = false;
        img.maskable = false;
    }

    public void OnDrag(PointerEventData pointer){
        t.anchoredPosition += pointer.delta/canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData pointer){        
        group.blocksRaycasts = true;
        img.maskable = true;
        Vector3 position = new Vector3(t.position.x, t.position.y);

        t.anchoredPosition = prev;
        position = Camera.main.ScreenToWorldPoint(position);
        PlacementSystem.placementSystem.InitializeWithObject(plantedPrefab, position, img, species.text);
    }
}
