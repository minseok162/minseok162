using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Profile : MonoBehaviour
{
    const string URL = "https://script.google.com/macros/s/AKfycbw5BZ5FRHql1VGNpFy3oAXUpOQUh60uflScoebuV2CFy6w9jE0/exec";
    public Image image1;
    public Image image2;

    [SerializeField]
    Sprite[] sprites;

    int index = 0;

    public void a1()
    {
        index = 0;
        image1.sprite = sprites[index];
    }

    public void a2()
    {
        index = 1;
        image1.sprite = sprites[index];
    }

    public void a3()
    {
        index = 2;
        image1.sprite = sprites[index];
    }

    public void a4()
    {
        index = 3;
        image1.sprite = sprites[index];
    }

    public void a5()
    {
        index = 4;
        image1.sprite = sprites[index];
    }

    public void a6()
    {
        index = 5;
        image1.sprite = sprites[index];
    }

    public void a7()
    {
        index = 6;
        image1.sprite = sprites[index];
    }

    public void a8()
    {
        index = 7;
        image1.sprite = sprites[index];
    }

    public void a9()
    {
        index = 8;
        image1.sprite = sprites[index];
    }

    public void select()
    {
        image2.sprite = sprites[index];
        WWWForm form = new WWWForm();
        form.AddField("order", "profileinput");
        form.AddField("profile", index);
        StartCoroutine(Post(form));
    }

    IEnumerator Post(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.isDone) print(www.downloadHandler.text);
            else print("웹의 응답이 없습니다.");
        }
    }
}
