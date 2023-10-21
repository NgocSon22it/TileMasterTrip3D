using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class References
{
    public static Account_Entity account =  new Account_Entity();

    public static int CurrentStar;

    public static bool FirstLogin = true;

    public static void Add_Reward(int Coin, int Star)
    {
        account.Star += Star;
        account.Coin += Coin;

        if (account.Level < 3)
        {
            account.Level++;
        }
    }

    public static bool MusicCheck = true, SoundCheck = true;

    public static string dataFilePath = Path.Combine(Application.persistentDataPath, "account_data.json");

    public static void SaveAccountData(Account_Entity account)
    {
        string json = JsonUtility.ToJson(account);

        File.WriteAllText(dataFilePath, json);
    }
    public static void LoadAccountData()
    {
        if (File.Exists(dataFilePath))
        {
            string json = File.ReadAllText(dataFilePath);

            account =  JsonUtility.FromJson<Account_Entity>(json);
        }
        else
        {
            account = GetNewAccount();
        }

        if (account.Level < 1) { account.Level = 1; }
    }
    public static Account_Entity GetNewAccount()
    {
        return new Account_Entity("1", "User", 0, 0, 0, 1);
    }

}

public static class Message
{

    public static string Win_GoodJob = "Xuất sắc";

    public static string Lose_OutOfSlot = "Hết slot rồi!";
    public static string Lose_TimeUp = "Hết thời gian!";

    public static string NotEnoughMoney = "Không đủ tiền!";
}

public enum Scenes
{
    Home, Game
}

public enum Tiles_ID
{
    Tile_Bush_001, Tile_Bush_002, Tile_Bush_003, Tile_Bush_004,
    Tile_Crescent_001, Tile_Crescent_002,
    Tile_Crystals_001, Tile_Crystals_002, Tile_Crystals_003, Tile_Crystals_004,
    Tile_Flower_001,
    Tile_Moon_001, Tile_Moon_002,
    Tile_Mushroom_001, Tile_Mushroom_002
}

