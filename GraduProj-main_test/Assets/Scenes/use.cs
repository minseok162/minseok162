using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class use : MonoBehaviour
{
    public bool spawnCheck = true;

    public List<string> name = new List<string>();
    public List<string> Repair = new List<string>();
    public List<string> Repair1 = new List<string>();
    public string ID;
    public Transform parent;
    public GameObject cardprefab;
    public List<Inven> result1 = new List<Inven>();
    public List<Inven> deck = new List<Inven>();
    public List<Inven> deck2 = new List<Inven>();

    private void Start()
    {
        for (int j = 0; j < deck.Count; j++)
        {
            deck2.Add(new Inven((deck[j])));
        }
    }

    public void Update()
    {
        if (spawnCheck == true)
        {

            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (result) =>
            {
                for (int j = 0; j < deck.Count; j++)
                {
                    for (int i = 0; i < result.Inventory.Count; i++)
                    {
                        if (result.Inventory[i].ItemId == deck[j].invenName)
                        {
                            Repair.Add(result.Inventory[i].RemainingUses.ToString());
                            Repair1.Add(result.Inventory[i].ItemInstanceId);
                            name.Add(result.Inventory[i].ItemId);
                        }
                    }
                }
                sss();

            },
            (error) => print("인벤토리 불러오기 실패"));
            spawnCheck = false;
        }

    }
    public void stop()
    {
        for (int j = 0; j < deck2.Count; j++)
        {
            deck.Add(new Inven((deck2[j])));
        }
        if (parent.transform.childCount != 0)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }
        }
        spawnCheck = true;
    }

    void sss()
    {
        var request = new GetUserDataRequest() { PlayFabId = Variables.name };
        PlayFabClientAPI.GetUserData(request, (result) =>
        {
            foreach (var eachData in result.Data)
            {
                for (int i = 0; i < deck.Count; i++)
                {
                    for (int j = 0; j < name.Count; j++)
                        if (eachData.Key == deck[i].invenName)
                        {
                            if (name[j] == deck[i].invenName)
                            {
                                deck[i].invenRepair = Repair[j] + " / " + (int.Parse(eachData.Value.Value) * 2).ToString();
                                deck[i].Unitlevel = eachData.Value.Value + "lv";
                                cardprefab.name = name[j] + " " + Repair1[j] + " ";
                                result1.Add(new Inven(deck[i]));
                                InvenUI InvenUI = Instantiate(cardprefab, parent).GetComponent<InvenUI>();
                                InvenUI.InvenUISet(result1[i]);
                            }
                        }
                }

                for (int i = 0; i < deck.Count; i++)
                {
                        if (eachData.Key == deck[i].invenName)
                        {
                            if (deck[i].invenRepair == "")
                            {
                                deck[i].invenRepair = 0 + " / " + (int.Parse(eachData.Value.Value) * 2).ToString();
                                deck[i].Unitlevel = eachData.Value.Value + "lv";
                                cardprefab.name = deck[i].invenName + " ";
                                result1.Add(new Inven(deck[i]));
                                InvenUI InvenUI = Instantiate(cardprefab, parent).GetComponent<InvenUI>();
                                InvenUI.InvenUISet(result1[i]);
                            }
                        }
                }
            }
            name.RemoveRange(0, name.Count);
            Repair.RemoveRange(0, Repair.Count);
            Repair1.RemoveRange(0, Repair1.Count);
            result1.RemoveRange(0, result1.Count);
            deck.RemoveRange(0, deck.Count);
        },
        (error) => print("데이터 불러오기 실패"));
    }


}
