using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDrag : MonoBehaviour
{
    private Vector3 startPos;
    private float deltaX, deltaY;

    void Start(){
        startPos = Input.mousePosition;
        startPos = Camera.main.ScreenToWorldPoint(startPos);
        deltaX = startPos.x - transform.position.x;
        deltaY = startPos.y - transform.position.y;
    }
    
    void Update(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = new Vector3(mousePos.x - deltaX, mousePos.y - deltaY);
        Vector3Int cellPos = PlacementSystem.placementSystem.gridLayout.WorldToCell(pos);
        transform.position = PlacementSystem.placementSystem.gridLayout.CellToLocalInterpolated(cellPos);
    }

    private void LateUpdate(){
        if(Input.GetMouseButtonUp(0)){
            gameObject.GetComponent<PlacedObject>().CheckPlacement();
            PlacementSystem.placementSystem.UpdateHappiness();
            Destroy(this);
        }
    }
}
