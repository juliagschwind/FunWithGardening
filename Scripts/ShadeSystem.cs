using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class ShadeSystem : MonoBehaviour
{

    public GridLayout gridLayout;
    public static ShadeSystem shadeSystem;
    public Tilemap MainTilemap;
    public TileBase shade;
    public Camera cam;

    void Awake(){
        shadeSystem = this;
    }

    public int GetShade(BoundsInt area){
        TileBase[] array = GetTilesBlock(area, MainTilemap);
        int shadeCount = 0;
        foreach(var b in array){
            if(b==shade){
                shadeCount++;
            }
        }
        return shadeCount;
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
