using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile_Hold : MonoBehaviour
{
    public Tile tile;

    public bool IsFill, IsBuy;

    public RectTransform MainPoint;

    [SerializeField] public GameObject Canvas;

    private void Awake()
    {
        CheckIsBuy();
    }

    public void Equip(Tile tile)
    {
        if (tile != null)
        {
            IsFill = true;
            this.tile = tile;
            tile.Set_Target(MainPoint);
        }
    }

    public void Unequip()
    {
        if (tile != null)
        {
            tile.Set_Earn();
            tile = null;
            IsFill = false;
        }
    }

    public void Clear()
    {
        if (tile != null)
        {
            tile = null;
            IsFill = false;
        }
    }

    public void CheckIsBuy()
    {
        if (IsBuy)
        {
            Canvas.SetActive(false);
        }
    }

    public void Buy()
    {
        IsBuy = true;
        CheckIsBuy();
    }
}
