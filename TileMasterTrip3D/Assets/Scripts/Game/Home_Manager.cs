using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Home_Manager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text Heart_Txt;
    [SerializeField] TMP_Text Star_Txt;
    [SerializeField] TMP_Text Coin_Txt;

    [SerializeField] TMP_Text Level_Txt;

    [Header("Instance")]
    public static Home_Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AccountInformation_Load();
    }

    public void AccountInformation_Load()
    {
        if (References.account != null)
        {
            Heart_Txt.text = "Full";
            Star_Txt.text = References.account.Star.ToString();
            Coin_Txt.text = References.account.Coin.ToString();
            Level_Txt.text = References.account.Level.ToString();
        }

    }

}
