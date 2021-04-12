using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

[System.Serializable]
public class GoogleData
{
	public string order, result, msg, value;
}

public class database : MonoBehaviour
{

	const string URL = "https://script.google.com/macros/s/AKfycbxeqVQAnDn1Hqqnkev54B1Jiee75hPv9R5Rf4GStCW5p8n6hOUCmGJj8-5pjSNDDjRG/exec";
	public GoogleData GD;
	public Text nametext;
	public GameObject MenuUI;
	public GameObject fristnameUI;
	public GameObject LoginERRORUI;
	public GameObject loginUI;
	public GameObject logoutUI;
	public InputField IDInput, PassInput, ValueInput;
	int logout = 0;
	string id, pass;

	bool SetIDPass()
	{
		id = IDInput.text.Trim();
		pass = PassInput.text.Trim();

		if (id == "" || pass == "") return false;
		else return true;
	}

	public void Register()
	{
		if (!SetIDPass())
		{
			print("아이디 또는 비밀번호가 비어있습니다");
			return;
		}

		WWWForm form = new WWWForm();
		form.AddField("order", "register");
		form.AddField("id", id);
		form.AddField("pass", pass);
		StartCoroutine(Post(form));
	}

	public void Login()
	{
		if (!SetIDPass())
		{
			print("아이디 또는 비밀번호가 비어있습니다");
			return;
		}

		WWWForm form = new WWWForm();
		form.AddField("order", "login");
		form.AddField("id", id);
		form.AddField("pass", pass);
		
		StartCoroutine(Post(form));
		
	}

	public void showUI()
	{
	 if(logout == 0)
     {
			loginUI.SetActive(true);
	 }
	 else if (logout == 1)
	 {
			logoutUI.SetActive(true);
	 }
	}

	public void Logout()
	{
		WWWForm form = new WWWForm();
		form.AddField("order", "logout");
		nametext.text = "";
		StartCoroutine(Post(form));
		logout = 0;
	}

	public void SetValue()
	{
		WWWForm form = new WWWForm();
		form.AddField("order", "setValue");
		form.AddField("value", ValueInput.text);

		StartCoroutine(Post(form));
		System.Threading.Thread.Sleep(1000);
		fristnameUI.SetActive(false);
	}

	public void GetValue()
	{
		WWWForm form = new WWWForm();
		form.AddField("order", "getValue");

		StartCoroutine(Post(form));
	}

	IEnumerator Post(WWWForm form)
	{
		using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
		{
			yield return www.SendWebRequest();

			if (www.isDone) Response(www.downloadHandler.text);
			else print("웹의 응답이 없습니다.");
		}
	}

	void Response(string json)
	{
		if (string.IsNullOrEmpty(json)) return;

		GD = JsonUtility.FromJson<GoogleData>(json);

		if (GD.msg == "로그인 실패")
		{
			print(GD.order + "을 실행할 수 없습니다. 에러 메시지 : " + GD.msg);
			MenuUI.SetActive(true);
			loginUI.SetActive(true);
			LoginERRORUI.SetActive(true);
			return;
		}
		else if (GD.msg == "로그인 완료")
		{
			
			if (GD.value == "")
			{
				MenuUI.SetActive(false);
				fristnameUI.SetActive(true);
			}
			if (GD.value != "")
			{
				logout = 1;
				showUI();
				MenuUI.SetActive(false);
				loginUI.SetActive(false);
				LoginERRORUI.SetActive(false);
				nametext.text = GD.value;
			}

		}
		print(GD.order + "을 실행했습니다. 메시지 : " + GD.msg);
	}
}

