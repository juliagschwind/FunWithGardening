using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class CompanionSystem : MonoBehaviour
{
    public GridLayout gridLayout;
    public static CompanionSystem companionSystem;
    public Tilemap MainTilemap;
    public Camera cam;

    void Awake(){
        companionSystem = this;
    }

    public int GetCompanions(BoundsInt area, TileBase tile){
        TileBase[] array = GetTilesBlock(area, MainTilemap);
        int count = 0;
        foreach(var b in array){
            if(b==tile){
                count++;
            }
        }
        return count;
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
}
