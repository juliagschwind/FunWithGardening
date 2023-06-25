using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class PlacementSystem : MonoBehaviour
{

    private int currentId;
    public static Dictionary<int, PlacedObject> storage;

    public GridLayout gridLayout;
    public static PlacementSystem placementSystem;
    public Tilemap MainTilemap;
    public TileBase taken;
    public Camera cam;

    public BoundsInt area;
    public BoundsInt areaCompanions;

    public GridLayout gridLayoutCompanions;
    public Tilemap CompanionsTilemap;

    void Awake(){
        placementSystem = this;
        storage = new Dictionary<int, PlacedObject>();
    }

    public void InitializeWithObject(GameObject plant, Vector3 pos, Image img, string species){
        // Get plant which should be allocated
        Item plantItem = ItemStorage.storage[species];

        // Get the position on the map where it should be allocated
        pos.z = 0;
        Vector3Int cellPos = gridLayout.WorldToCell(pos);
        Vector3 position = gridLayout.CellToLocalInterpolated(cellPos);

        //Initialize the component which should be placed
        if(plant.TryGetComponent<Planted>(out Planted item)){
            item.Initialize(img.sprite, cam, species);
        }

        // Place the object
        GameObject newplant = Instantiate(plant, position, Quaternion.identity);
        newplant.transform.SetParent(MainTilemap.transform);
        PlacedObject temp = newplant.transform.GetComponent<PlacedObject>();
        temp.item = plantItem;
        temp.cam = cam;
        temp.ID = currentId;
        
        temp.gameObject.AddComponent<PlantDrag>(); //responsible for relocating the object
        storage.Add(currentId, temp);
        currentId++;
    }

    //Checks if an item can be placed on the given area
    public bool CanTakeArea(BoundsInt area){
        TileBase[] array = GetTilesBlock(area, MainTilemap);
        foreach(var b in array){
            if(b==taken){
                return false;
            }
        }
        return true;
    }

    public void UpdateHappiness(){
        foreach(KeyValuePair<int, PlacedObject> entry in storage){
            entry.Value.HappinessUpdate();
        }
    }
    public void TakeArea(BoundsInt area){
        SetTilesBlock(area, taken, MainTilemap);
    }

    public void TakeAreaCompanion(BoundsInt area, TileBase tile){
        SetTilesBlock(area, tile, CompanionsTilemap);
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap map){
        TileBase[] array = new TileBase[area.size.x * area.size.y];
        int counter = 0;

        foreach(var v in area.allPositionsWithin){
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = map.GetTile(pos);
            counter++;
        }
        return array; 
    }


    private static void SetTilesBlock(BoundsInt area, TileBase tilebase, Tilemap map){
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y];
        FillTiles(tileArray, tilebase);
        map.SetTilesBlock(area, tileArray);
    }


    private static void FillTiles(TileBase[] array, TileBase tilebase){
        for(int i = 0; i<array.Length; i++){
            array[i] = tilebase;
        }
    }

    public void ClearArea(BoundsInt area, Tilemap tilemap){
        SetTilesBlock(area, null, tilemap);
    }
}
