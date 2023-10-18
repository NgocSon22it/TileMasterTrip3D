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

    [Header("Configure")]
    [SerializeField] List<Tile_Hold> listTile_Hold;
    [SerializeField] public List<GameObject> listTile;
    [SerializeField] public Level_Scriptable level_Scriptable;
    int Index_Insert, Index_Remove, countDuplicate, Streak, Streak_Time = 5;
    Coroutine Streak_Coroutine, CheckLose_Coroutine;
    public bool IsOver, isPause, IsStart;

    [Header("Object_Pool")]
    [SerializeField] List<GameObject> Pool_StarEffect;
    GameObject obj;

    [Header("UI")]
    [SerializeField] TMP_Text Star_Txt;
    [SerializeField] TMP_Text Time_Txt;
    [SerializeField] TMP_Text Level_Txt;

    [SerializeField] GameObject Streak_Object;
    [SerializeField] TMP_Text Streak_Txt;
    [SerializeField] Image Streak_Progress;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            tile_Pool.gameObject.SetActive(true);
        }
    }
    public void Add_Score()
    {
        References.Star += (1 + Streak);
        Star_Txt.text = References.Star.ToString();

        Streak_Start();

        if (Check_Win())
        {
            Debug.Log("Win r");
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

    public void Insert_Tile(Tile tile)
    {
        if (IsStart)
        {
            if (Check_Lose() == false && IsOver == false)
            {
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
    }

    public bool Check_Lose()
    {
        foreach (var tile in listTile_Hold)
        {
            if (!tile.IsFill) { return false; }
        }

        return true;
    }

    public bool Check_Win()
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
            StartCoroutine(AddScore_Delay(tile));
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

        Add_Score();
    }

    IEnumerator CheckLose_Delay()
    {
        yield return new WaitForSeconds(1f);

        if (Check_Lose())
        {
            IsOver = true;
            Debug.Log("Thua nha");
        }
    }

    public GameObject GetObjectFromPool(List<GameObject> list)
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
        float currentTime = level_Scriptable.Level_Entity.PlayTime;
        int minutes, seconds;
        IsStart = true;

        Level_Txt.text = $"Lv. {level_Scriptable.Level_Entity.Level}";

        while (currentTime > 0)
        {
            minutes = Mathf.FloorToInt(currentTime / 60);
            seconds = Mathf.FloorToInt(currentTime % 60);

            Time_Txt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);

            currentTime--;
        }

        Time_Txt.text = "00:00";

    }

    private IEnumerator Streak_Inscrease()
    {
        Streak++;
        Streak_Txt.text = $"Combo x{Streak}";
        Streak_Toggle(true);
        float currentTime = Streak_Time;

        while (currentTime > 0)
        {
            Streak_Progress.fillAmount = (float)currentTime / (float)Streak_Time;
            yield return new WaitForSeconds(0.01f);
            currentTime -= 0.01f;
        }

        Streak = 0;
        Streak_Toggle(false);

    }
}
