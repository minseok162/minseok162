using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class InvenUI : MonoBehaviour
{
    public string[] text;
    public string text1;
    public string text2;    
    public Image AAA;
    public Text invenName;
    public Text invenRepair;
    public Text Unitlevel;
    public GameObject buttonName;
    public List<string> sss = new List<string>();
    public void InvenUISet(Inven inven)
    {
        AAA.sprite = inven.invenImage;
        invenName.text = inven.invenName;
        invenRepair.text = inven.invenRepair;
        Unitlevel.text = inven.Unitlevel;
    }

    public void button()
    {
        text = transform.parent.gameObject.name.Split(' ');
        text1 = text[0];
        text2 = text[1];

        var request3 = new GetUserDataRequest() { PlayFabId = Variables.name };
        PlayFabClientAPI.GetUserData(request3, (result) =>
        {
            int Gold = int.Parse(result.Data[text1].Value);
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result1) =>
                {
                    for (int i = 0; i < result1.Inventory.Count; i++)
                    {
                        var Inven1 = result1.Inventory[i];
                        if (Inven1.ItemId == text1)
                        {
                            int iii;

                            iii = result1.VirtualCurrency["GD"];
                            if (iii >= Gold * 20 && Inven1.RemainingUses >= Gold * 2)
                            {

                                Startmain go = GameObject.Find("GameObject (1)").GetComponent<Startmain>();
                                use go2 = GameObject.Find("GameObject (2)").GetComponent<use>();

                                go.name2.text = result1.VirtualCurrency["GD"].ToString();

                                var request = new ConsumeItemRequest { ConsumeCount = int.Parse(result.Data[text1].Value)*2 , ItemInstanceId = text2 };
                                PlayFabClientAPI.ConsumeItem(request, (result2) => print("아이템 사용 성공"), (error1) => print("아이템 사용 실패"));

                                var request1 = new SubtractUserVirtualCurrencyRequest() { VirtualCurrency = "GD", Amount = int.Parse(result.Data[text1].Value) * 20 };
                                PlayFabClientAPI.SubtractUserVirtualCurrency(request1, (result3) => go.name2.text = result3.Balance.ToString(), (error2) => print("돈 빼기 실패"));

                                var request2 = new UpdateUserDataRequest() { Data = new Dictionary<string, string>() { { text1, (Gold + 1).ToString() } } };
                                PlayFabClientAPI.UpdateUserData(request2, (result4) => print("데이터 저장 실패"), (error3) => GameObject.Find("GameObject (2)").GetComponent<use>().stop() );
                            }
                        }
                    }
                },
             (error4) => print("인벤토리 불러오기 실패"));

        },
        (error5) => print("데이터 불러오기 실패"));

        Invoke("button2", 0.45f);
    }
    public void button2()
    {
        var request3 = new GetUserDataRequest() { PlayFabId = Variables.name };
        PlayFabClientAPI.GetUserData(request3, (result1) =>
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result2) =>
            {
                for (int i = 0; i < result2.Inventory.Count; i++)
                {
                    var Inven1 = result2.Inventory[i];
                    if (Inven1.ItemId == text1)
                    { 
                        Unitlevel.text = result1.Data[text1].Value + "lv";
                        invenRepair.text = Inven1.RemainingUses + " / " + (int.Parse(result1.Data[text1].Value) * 2).ToString();
                    }
                    else
                    {
                        Unitlevel.text = result1.Data[text1].Value + "lv";
                        invenRepair.text = 0 + " / " + (int.Parse(result1.Data[text1].Value) * 2).ToString();
                    }
                }

            },
            (error4) => print("인벤토리 불러오기 실패"));
        },
        (error5) => print("데이터 불러오기 실패"));
    }
}
