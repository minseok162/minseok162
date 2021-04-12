using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading;

[System.Serializable]
public class Data
{
    public string value, rank, combet;
}

public class board : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbxeqVQAnDn1Hqqnkev54B1Jiee75hPv9R5Rf4GStCW5p8n6hOUCmGJj8-5pjSNDDjRG/exec";
    public Data GD;
    public Text Name;
    public Text Rank;
    public Text Combet;
    public GameObject img;
    public GameObject scroll;

    void Start()
    {
        int i;
        j = 0;
        for (i = 1; i < 4; i++)
        {
            Thread.Sleep(200);
            WWWForm form = new WWWForm();
            form.AddField("order", "board");
            form.AddField("val", i);
            StartCoroutine(Post(form));
        }
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

        GD = JsonUtility.FromJson<Data>(json);
        
        Name.text = GD.value;
        Rank.text = GD.rank;
        Combet.text = GD.combet;

        GameObject prt = Instantiate(img);
        prt.transform.SetParent(scroll.transform);

        Text nickname = Instantiate(Name);
        nickname.transform.SetParent(prt.transform);
        Text ranking = Instantiate(Rank);
        ranking.transform.SetParent(prt.transform);
        Text FFF = Instantiate(Combet);
        FFF.transform.SetParent(prt.transform);

    }

}
