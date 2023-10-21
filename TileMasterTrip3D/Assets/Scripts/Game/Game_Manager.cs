using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    [Header("Instance")]
    public static Game_Manager Instance;

    [Header("Tile Pool")]
    [SerializeField] Tile_Pool tile_Pool;

    [Header("Buy Tile Hold")]
    [SerializeField] Tile_Hold Slot;
    [SerializeField] GameObject BuySlot_Panel;
    [SerializeField] TMP_Text Message_Txt;
    int SlotPrice = 100;

    [Header("Configure")]
    [SerializeField] List<Tile_Hold> listTile_Hold;
    [SerializeField] public List<GameObject> listTile;
    [SerializeField] public List<Level_Scriptable> list_Level;
    int Index_Insert, Index_Remove, countDuplicate, Streak, Streak_Time = 5;
    Coroutine Streak_Coroutine, CheckLose_Coroutine;
    [HideInInspector]
    public bool IsOver, isPause, IsStart;
    public Level_Entity CurrentLevel;

    [Header("Object_Pool")]
    [SerializeField] List<GameObject> Pool_StarEffect;
    GameObject obj;

    [Header("UI")]
    [SerializeField] TMP_Text Star_Txt, Time_Txt, Level_Txt;

    [SerializeField] GameObject Streak_Object;
    [SerializeField] TMP_Text Streak_Txt;
    [SerializeField] Image Streak_Progress;

    [Header("Win/Lose")]
    [SerializeField] GameObject WinPanel, LosePanel;
    [SerializeField] TMP_Text CoinBonus_Txt, StarBonus_Txt, Status_Txt;
    [SerializeField] AudioClip WinMusic, LoseMusic;

    [SerializeField] AudioSource Source_WinLose, Source_EarnTile;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        tile_Pool.SpawnTile();
    }
    public void Add_Score()
    {
        References.CurrentStar += (1 + Streak);
        Star_Txt.text = References.CurrentStar.ToString();

        Streak_Start();

        if (Check_Win())
        {
            Init_End(true, References.CurrentStar.ToString());
        }
    }

    public void Streak_Start()
    {
        if (Streak_Coroutine != null)
        {
            StopCoroutine(Streak_Coroutine);
            Streak_Coroutine = null;
        }

        Streak_Coroutine = StartCoroutine(Streak_Inscrease());
    }

    public void Streak_Toggle(bool value)
    {
        Streak_Object.SetActive(value);
    }

    public void Init_Start()
    {
        StartCoroutine(Start_Coroutine());
    }

    public void Init_End(bool isWin, string status)
    {
        StopAllCoroutines();
        Streak_Toggle(false);
        IsOver = true; IsStart = false;
        Game_ShowEndPanel(isWin, status);
    }

    public void Insert_Tile(Tile tile)
    {
        if (Check_Lose() == false && IsOver == false)
        {
            tile.SetDefaultSize();

            Index_Insert = GetIndex_Insert(tile.tile_Scriptable.Tile_Entity.Id);

            for (int i = listTile_Hold.Count - 1; i >= Index_Insert + 1; i--)
            {
                listTile_Hold[i].Equip(listTile_Hold[i - 1].tile);
            }

            listTile_Hold[Index_Insert].Equip(tile);

            Check_Duplicate(tile);

            if (CheckLose_Coroutine != null)
            {
                StopCoroutine(CheckLose_Coroutine);
                CheckLose_Coroutine = null;
            }

            CheckLose_Coroutine = StartCoroutine(CheckLose_Delay());
        }

    }

    private bool Check_Lose()
    {
        foreach (var tile in listTile_Hold)
        {
            if (!tile.IsFill) { return false; }
        }

        return true;
    }

    private bool Check_Win()
    {
        foreach (GameObject obj in listTile)
        {
            if (obj.activeInHierarchy)
            {
                return false;
            }
        }

        return true;
    }

    private void Check_Duplicate(Tile tile)
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
            StartCoroutine(AddScore_Delay(tile));
        }

    }

    private int GetIndex_Insert(Tiles_ID id)
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

    private int GetIndex_Remove(Tiles_ID id)
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

    IEnumerator AddScore_Delay(Tile tile)
    {
        yield return new WaitForSeconds(0.8f);

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
        Source_EarnTile.Play();
        Add_Score();
    }

    IEnumerator CheckLose_Delay()
    {
        yield return new WaitForSeconds(1f);

        if (Check_Lose())
        {
            Init_End(false, Message.Lose_OutOfSlot);
        }
    }

    private GameObject GetObjectFromPool(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeInHierarchy)
            {
                return list[i];
            }
        }
        return null;
    }

    public void SetObjectFromPoolAtPosition(Transform transform)
    {
        obj = GetObjectFromPool(Pool_StarEffect);
        if (obj != null)
        {
            obj.transform.position = transform.position;
            obj.SetActive(true);
        }
    }

    private IEnumerator Start_Coroutine()
    {
        float currentTime = CurrentLevel.PlayTime;
        int minutes, seconds;
        IsStart = true;
        References.CurrentStar = 0;
        Level_Txt.text = $"Lv. {CurrentLevel.Level}";

        while (currentTime > 0)
        {
            minutes = Mathf.FloorToInt(currentTime / 60);
            seconds = Mathf.FloorToInt(currentTime % 60);

            Time_Txt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);

            currentTime--;
        }

        Time_Txt.text = "00:00";
        Init_End(false, Message.Lose_TimeUp);
    }

    private IEnumerator Streak_Inscrease()
    {
        Streak++;
        Streak_Txt.text = $"Combo x{Streak}";
        Streak_Toggle(true);
        float currentTime = Streak_Time;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            Streak_Progress.fillAmount = (float)currentTime / (float)Streak_Time;
            yield return null;
        }

        Streak = 0;
        Streak_Toggle(false);

    }

    public void Game_ShowEndPanel(bool isWin, string status)
    {
        if (isWin)
        {
            StarBonus_Txt.text = $"+{status}";
            CoinBonus_Txt.text = $"+{CurrentLevel.CoinBonus}";

            References.Add_Reward(CurrentLevel.CoinBonus, References.CurrentStar);
            Load_Sound(WinMusic);
            WinPanel.SetActive(true);
        }
        else
        {
            Status_Txt.text = $"{status}";
            Load_Sound(LoseMusic);
            LosePanel.SetActive(true);
        }

        Set_PauseGame(true);
    }

    public void Load_Level()
    {
        if (References.account != null)
        {        
            CurrentLevel = list_Level[References.account.Level - 1].Level_Entity;
        }
    }

    public void Load_Sound(AudioClip audioClip)
    {
        Source_WinLose.clip = audioClip;
        Source_WinLose.Play();
    }

    #region Buy Slot

    public void BuySlot_Open()
    {
        Set_PauseGame(true);
        BuySlot_Message("");
        Setting_Manager.Instance.PlaySound();
        BuySlot_Panel.SetActive(true);
    }

    public void BuySlot_Close()
    {
        Set_PauseGame(false);
        Setting_Manager.Instance.PlaySound();
        BuySlot_Panel.SetActive(false);
    }

    public void BuySlot()
    {
        if (References.account.Coin >= SlotPrice)
        {
            References.account.Coin -= SlotPrice;
            Slot.Buy();
            listTile_Hold.Add(Slot);
            BuySlot_Close();
        }
        else
        {
            BuySlot_Message(Message.NotEnoughMoney);
        }
    }
    private void BuySlot_Message(string message)
    {
        Message_Txt.text = message;
    }
    #endregion

    public void Set_PauseGame(bool value)
    {
        isPause = value;
        Time.timeScale = value == true ? 0f : 1f;
    }
}
