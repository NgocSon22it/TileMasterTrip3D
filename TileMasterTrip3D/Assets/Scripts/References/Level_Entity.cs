using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level_Entity 
{
    public int Level;
    public string MapName;
    public string DisplayName;
    public int PlayTime;

    public List<Tiles_ID> ListTile = new List<Tiles_ID>();
}
