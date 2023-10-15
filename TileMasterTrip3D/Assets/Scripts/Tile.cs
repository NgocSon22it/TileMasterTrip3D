using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tile_Scriptable tile;

    private void OnMouseEnter()
    {
        gameObject.GetComponent<Outline>().OutlineWidth = 10f;
    }

    private void OnMouseDown()
    {
        Game_Manager.Instance.Insert_Tile(tile.Tile_Entity);
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().OutlineWidth = 0f;
    }
}
