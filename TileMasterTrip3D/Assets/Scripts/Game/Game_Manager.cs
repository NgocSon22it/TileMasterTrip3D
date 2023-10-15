using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [Header("Instance")]
    public static Game_Manager Instance;

    [SerializeField] List<Tile_Hold> listTile_Hold = new List<Tile_Hold>();

    private void Awake()
    {
        Instance = this;
    }

    public void Insert_Tile(Tile_Entity tile_Entity)
    {
        if (listTile_Hold[0].IsFill)
        {
            for (int i = listTile_Hold.Count - 1; i >= 1; i--)
            {
                listTile_Hold[i].Equip(listTile_Hold[i - 1].tile);
            }
            Debug.Log("K null");
        }
        else
        {
            listTile_Hold[0].Equip(tile_Entity);
            Debug.Log("null");

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            listTile_Hold[0].Unequip();
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
}
