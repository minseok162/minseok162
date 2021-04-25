using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Chatting : MonoBehaviour
{
    const string URL = "https://docs.google.com/spreadsheets/d/1Kb3388WpLnBcoLXjT6GILrz2nfBYpGtivvQSZau_2Ro/export?format=tsv&range=B:B";
    const string WebURL = "https://script.google.com/macros/s/AKfycbwJLbWjlGv1pQPAj3H7bu_9rEwvuLKfUvWdlyfP1jrStIxkzg57/exec";

    public Text ChatText;
    public Text Nickname;
    public InputField ChatInput;

    void Start()
    {
            StartCoroutine(Get());
    }

    IEnumerator Get()
    {
        while (true)
        {
            UnityWebRequest www = UnityWebRequest.Get(URL);
            yield return www.SendWebRequest();

            string data = www.downloadHandler.text;
            ChatText.text = data;
        }
    }

    public void ChatPost()
    {
        WWWForm form = new WWWForm();
        form.AddField("nickname", Nickname.text);
        form.AddField("chat", ChatInput.text);

        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(WebURL, form)) // 반드시 using을 써야한다
        {
            yield return www.SendWebRequest();
        }
    }

}
