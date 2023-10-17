using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [Header("Instance")]
    public static Game_Manager Instance;

    [Header("Configure")]
    [SerializeField] Level_Scriptable level_Scriptable;
    [SerializeField] List<Tile_Hold> listTile_Hold = new List<Tile_Hold>();
    int Index_Insert, Index_Remove, countDuplicate;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Debug.Log(level_Scriptable.Level_Entity.ListTile.Count);
    }

    public void Insert_Tile(Tile tile)
    {
        Index_Insert = GetIndex_Insert(tile.tile_Scriptable.Tile_Entity.Id);

        for (int i = listTile_Hold.Count - 1; i >= Index_Insert + 1; i--)
        {
            listTile_Hold[i].Equip(listTile_Hold[i - 1].tile);
        }

        listTile_Hold[Index_Insert].Equip(tile);

        Check_Duplicate(tile);

        if (CheckOver())
        {
            Debug.Log("Thua nha");
        }
    }

    public bool CheckOver()
    {
        foreach (var tile in listTile_Hold)
        {
            if (!tile.IsFill) { return false; }
        }

        return true;
    }

    public void Check_Duplicate(Tile tile)
    {
        countDuplicate = 0;

        foreach (var tile_hold in listTile_Hold)
        {
            if (tile_hold.IsFill && tile_hold.tile.tile_Scriptable.Tile_Entity == tile.tile_Scriptable.Tile_Entity)
            {
                countDuplicate++;
            }
        }

        if (countDuplicate == 3)
        {
            StartCoroutine(Delay(tile));
        }

    }

    public int GetIndex_Insert(string id)
    {
        for (int i = listTile_Hold.Count - 1; i >= 0; i--)
        {
            if (listTile_Hold[i].IsFill && listTile_Hold[i].tile.tile_Scriptable.Tile_Entity.Id.Equals(id))
            {
                return i + 1;
            }
        }

        return 0;
    }

    public int GetIndex_Remove(string id)
    {
        for (int i = 0; i < listTile_Hold.Count; i++)
        {
            if (listTile_Hold[i].IsFill && listTile_Hold[i].tile.tile_Scriptable.Tile_Entity.Id.Equals(id))
            {
                return i;
            }
        }

        return 0;
    }

    IEnumerator Delay(Tile tile)
    {
        yield return new WaitForSeconds(0.7f);

        Index_Remove = GetIndex_Remove(tile.tile_Scriptable.Tile_Entity.Id);

        for (int i = Index_Remove; i <= listTile_Hold.Count - 1; i++)
        {

            if (i < Index_Remove + 3)
            {
                listTile_Hold[i].Unequip();
            }

            if (i < listTile_Hold.Count - 3)
            {
                listTile_Hold[i].Equip(listTile_Hold[i + 3].tile);

                listTile_Hold[i + 3].Clear();
            }

        }
    }

}
