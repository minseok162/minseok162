using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RandomSelect : MonoBehaviour
{
    public GameObject open;
    public List<Card> deck = new List<Card>();  // 카드 덱
    public int total = 0;  // 카드들의 가중치 총 합
    public int Count = 0;
    public List<Card> result = new List<Card>();  // 랜덤하게 선택된 카드를 담을 리스트
    Animator animator;
    public Transform parent;
    public GameObject cardprefab;
    public GameObject Skip;

    void Update()
    {
        if (Count == 10 && V.num == 10)
        {
            V.num = 0;
            Var.count = 0;
            Skip.SetActive(true);
        }
        if (Count == 1 && V.num == 1)
        {
            V.num = 0;
            Var.count = 0;
            Skip.SetActive(true);
        }
    }
    void Start()
    {
        Count = Var.count;
        Sss();
    }
    public void Sss()
    {
        if (open.transform.childCount == 10)
        {
            total = 0;
            for (int i = 0; i < 10; i++)
            {
                Destroy(open.transform.GetChild(i).gameObject);
            }
            result.Clear();
        }
        if (open.transform.childCount == 1)
        {
            total = 0;
            for (int i = 0; i < 1; i++)
            {
                Destroy(open.transform.GetChild(i).gameObject);
            }
            result.Clear();
        }
        for (int i = 0; i < deck.Count; i++)
        {
            // 스크립트가 활성화 되면 카드 덱의 모든 카드의 총 가중치를 구해줍니다.
            total += deck[i].weight;
        }
        // 실행
        ResultSelect();
    }

    public void ResultSelect()
    {
        for (int i = 0; i < Count; i++)
        {
            // 가중치 랜덤을 돌리면서 결과 리스트에 넣어줍니다.
            result.Add(RandomCard()); 
            // 비어 있는 카드를 생성하고
            CardUI cardUI = Instantiate(cardprefab, parent).GetComponent<CardUI>();
            // 생성 된 카드에 결과 리스트의 정보를 넣어줍니다.
            cardUI.CardUISet(result[i]);
        }

    }
    // 가중치 랜덤의 설명은 영상을 참고.
    public Card RandomCard()
    {
        int weight = 0;
        string name;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for (int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].weight;
            name = deck[i].cardName;
            if (selectNum <= weight)
            {
                Card temp = new Card(deck[i]);
                var request = new PurchaseItemRequest() { CatalogVersion = "Unit", ItemId = name, VirtualCurrency = "GD", Price = 0 };
                PlayFabClientAPI.PurchaseItem(request, (result) => print("아이템 구입 성공"), (error) => print("아이템 구입 실패"));
                return temp;
            }
        }
        return null;
    }
    public void AAA()
    {
        SceneManager.LoadScene("MainScene1");
    }
}
