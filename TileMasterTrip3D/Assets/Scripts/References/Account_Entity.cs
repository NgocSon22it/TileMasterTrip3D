using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Account_Entity
{
    public string Id;
    public string Name;

    public int Coin;
    public int Star;
    public int Life;

    public int Level;

    public Account_Entity() { }
    public Account_Entity(string id, string name, int coin, int star, int life, int level)
    {
        Id = id;
        Name = name;
        Coin = coin;
        Star = star;
        Life = life;
        Level = level;
    }
}
