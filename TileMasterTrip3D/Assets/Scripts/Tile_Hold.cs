using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile_Hold : MonoBehaviour
{
    [SerializeField] Image Tile_Image;

    public Tile_Entity tile = null;

    public bool IsFill;

    public void Equip(Tile_Entity tile_Entity)
    {
        tile = tile_Entity;
        Tile_Image.sprite = tile.Image;
        IsFill = true;
    }

    public void Unequip()
    {
        tile = null;
        Tile_Image.sprite = null;
        IsFill = false;
    }

}
