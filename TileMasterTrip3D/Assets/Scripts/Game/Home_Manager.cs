using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Home_Manager : MonoBehaviour
{
    [Header("Loaded")]
    [SerializeField] GameObject Loaded_Panel;
    [SerializeField] TMP_Text Heart_Txt;
    [SerializeField] TMP_Text Star_Txt;
    [SerializeField] TMP_Text Coin_Txt;

    [SerializeField] TMP_Text Level_Txt;

    [Header("Loading")]
    [SerializeField] GameObject Loading_Panel;
    [SerializeField] TMP_Text Progress_Txt;
    [SerializeField] Image Progress_Bar;

    float LoadingTime = 2, CurrentTime, Progress;

    [Header("Instance")]
    public static Home_Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Check_Login();
        AccountInformation_Load();
    }

    public void Check_Login()
    {
        if (References.FirstLogin)
        {
            Loading(true);
            References.LoadAccountData();
            StartCoroutine(Loading_Start());
        }
        else
        {
            Loading(false);
        }
    }

    public void AccountInformation_Load()
    {
        Heart_Txt.text = "Full";
        Star_Txt.text = References.account.Star.ToString();
        Coin_Txt.text = References.account.Coin.ToString();
        Level_Txt.text = References.account.Level.ToString();

    }

    public void Loading(bool IsLoading)
    {
        Loading_Panel.SetActive(IsLoading);
        Loaded_Panel.SetActive(!IsLoading);
    }

    IEnumerator Loading_Start()
    {
        CurrentTime = 0;

        while (CurrentTime < LoadingTime)
        {
            CurrentTime += Time.deltaTime;
            Progress = CurrentTime / LoadingTime;
            Progress_Bar.fillAmount = Progress;

            int percentage = Mathf.RoundToInt(Progress * 100);
            Progress_Txt.text = percentage + "%";
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Progress_Bar.fillAmount = 1.0f;
        References.FirstLogin = false;
        Loading(false);
    }


}
