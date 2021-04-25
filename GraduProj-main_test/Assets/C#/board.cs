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
    const string URL = "https://script.google.com/macros/s/AKfycbw5BZ5FRHql1VGNpFy3oAXUpOQUh60uflScoebuV2CFy6w9jE0/exec";
    public Data GD;
    public Text Name;
    public Text Rank;
    public Text Combet;
    public GameObject img;
    public GameObject scroll;
    int i = 1;
    void Start()
    {
        StartCoroutine(Post());
    }

    IEnumerator Post()
    {
        
            WWWForm form = new WWWForm();
            form.AddField("order", "board");
            form.AddField("val", i);

            using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
            {
                yield return www.SendWebRequest();

                if (www.isDone) Response(www.downloadHandler.text);
                else print("웹의 응답이 없습니다.");
            }
            i++;
        if (i <= 3) StartCoroutine(Post());
        else yield return 0;

    }

    void Response(string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        GD = JsonUtility.FromJson<Data>(json);

        GameObject prt = Instantiate(img);
        prt.transform.SetParent(scroll.transform);

        Name.text = GD.value;
        Text nickname = Instantiate(Name);
        nickname.transform.SetParent(prt.transform);
        Rank.text = GD.rank;
        Text ranking = Instantiate(Rank);
        ranking.transform.SetParent(prt.transform);
        Combet.text = GD.combet;
        Text FFF = Instantiate(Combet);
        FFF.transform.SetParent(prt.transform);
    }

}
