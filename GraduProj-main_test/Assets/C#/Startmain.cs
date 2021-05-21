using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Startmain : MonoBehaviour
{
    public int Unit1;
    public int Unit2;
    public int Unit3;
    public Text name1;
    public Text name2;
    public Text DisPlayName;
    public Text Rank;
    public Text Combet;
    public GameObject img;
    public GameObject scroll;
    private bool spawnCheck = true;


    void Start()
    {
        var request = new GetUserDataRequest() { PlayFabId = Variables.name }; // 
        PlayFabClientAPI.GetUserData(request, (result) => name1.text = result.Data["NAME"].Value, (error) => print("데이터 불러오기 실패"));

        var request1 = new AddUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = 0 };
        PlayFabClientAPI.AddUserVirtualCurrency(request1, (result) => name2.text = result.Balance.ToString(), (error) => print("돈 얻기 실패"));
    }


    void Update()
    {
        if (spawnCheck == true)
        {
            spawnCheck = false;
            leaderboard();
        }
    }
    
    public void leaderboard()
    {
        var request2 = new GetLeaderboardRequest { StartPosition = 0, StatisticName = "High", MaxResultsCount = 100, ProfileConstraints = new PlayerProfileViewConstraints() { ShowLocations = true, ShowDisplayName = true } };
        PlayFabClientAPI.GetLeaderboard(request2, (result) =>
        {
            for (int i = 0; i < result.Leaderboard.Count; i++)
            {
                var curBoard = result.Leaderboard[i];
                GameObject prt = Instantiate(img);
                prt.transform.SetParent(scroll.transform);
                DisPlayName.text = curBoard.DisplayName;
                Text nickname = Instantiate(DisPlayName);
                nickname.transform.SetParent(prt.transform);
                Rank.text = (i + 1).ToString();
                Text ranking = Instantiate(Rank);
                ranking.transform.SetParent(prt.transform);
                Combet.text = curBoard.StatValue.ToString();
                Text FFF = Instantiate(Combet);
                FFF.transform.SetParent(prt.transform);
            }

        },
            (error) => print("리더보드 불러오기 실패"));
    }

    public void Count()
    {
        if (scroll.transform.childCount != 0)
        {
            for (int i = 0; i < scroll.transform.childCount; i++)
            {
                Destroy(scroll.transform.GetChild(i).gameObject);
            }
        }
        spawnCheck = true;
    }

    public void loginScene()
    {
        SceneManager.LoadScene("loginScene");
    }

    public void testScene()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void oneGacha()
    {
        Var.count = 1;
        SceneManager.LoadScene("Random");
    }
    public void tenGacha()
    {
        Var.count = 10;
        SceneManager.LoadScene("Random");
    }




}

public static class Var
{
    public static int count;
}