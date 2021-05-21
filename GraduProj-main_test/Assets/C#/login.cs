using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    public InputField EmailInput, PasswordInput, UsernameInput;
    public GameObject Nameinput;
    public string myID;


    public void GoogleLogin()
    {
        Social.localUser.Authenticate((success) =>
        {
            if (success) { PlayFabLogin(); }
        });
    }
    public void PlayFabLogin() //
    {
        var request = new LoginWithEmailAddressRequest { Email = Social.localUser.id + "@rand.com", Password = Social.localUser.id };
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { myID = result.PlayFabId; Variables.name = result.PlayFabId; SceneManager.LoadScene("MainScene1"); }, (error) => Nameinput.SetActive(true));
    }

    public void PlayFabRegister()
    {
        var request = new RegisterPlayFabUserRequest { Email = Social.localUser.id + "@rand.com", Password = Social.localUser.id, Username = UsernameInput.text, DisplayName = UsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { SetData(); setStatistics1(); PlayFabLogin(); }, OnRegisterFailure);
    }

    public void LoginBtn() 
    {
        var request = new LoginWithEmailAddressRequest { Email = EmailInput.text, Password = PasswordInput.text }; 
        PlayFabClientAPI.LoginWithEmailAddress(request, (result) => { Variables.name = result.PlayFabId; print(Variables.name); SceneManager.LoadScene("MainScene1"); }, OnLoginFailure);

    }

    public void RegisterBtn() 
    {
        var request = new RegisterPlayFabUserRequest { Email = EmailInput.text, Password = PasswordInput.text, Username = UsernameInput.text, DisplayName = UsernameInput.text }; 
        PlayFabClientAPI.RegisterPlayFabUser(request, (result) => { SetData(); setStatistics1(); LoginBtn(); }, OnRegisterFailure); 
    }

    public void SetData()
    {
        var request = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() {{ "NAME", UsernameInput.text } } };
        PlayFabClientAPI.UpdateUserData(request, (result) => print("데이터 저장 성공"), (error) => print("데이터 저장 실패"));
    }



    void OnLoginSuccess(LoginResult result) => print("로그인 성공");

    void OnLoginFailure(PlayFabError error) => print("로그인 실패");

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        SetData();
        print("회원가입 성공");
    }
    void OnRegisterFailure(PlayFabError error) => print("회원가입 실패");

    void setStatistics1()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate {StatisticName = "High", Value = 30},
            }
        },
       (result) => { print("값 저장됨"); },
       (error) => { print("값 저장실패"); });
    }
}
public static class Variables
{
    public static int num;
    public static string name = "";

}
