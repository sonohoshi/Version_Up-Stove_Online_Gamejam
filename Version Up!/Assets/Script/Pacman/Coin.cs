using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin instance;

    private AudioSource audioSource;

    public GameObject portal;
    public List<GameObject> itemList;
    private List<Transform> coinList = new List<Transform>();
    private List<Vector2> cantPutCoinPos = new List<Vector2>() { new Vector2(-6.25f, 0.5f), new Vector2(-5.75f, 0.5f), new Vector2(-5.25f, 0.5f), new Vector2(-4.75f, 0.5f), new Vector2(-4.25f, 0.5f), new Vector2(-3.75f, 0.5f) };
    private int getCoinCount = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SetCoinPos();
        MakeCoins();
    }

    // 코인 또는 아이템을 배치할 수 있는 좌표를 구한다. 처음 게임 시작시 한번만 호출된다.
    private void SetCoinPos()
    {
        int childIndex = 0;
        Vector2 pos = new Vector2(-6.25f, 7);
        while (pos.x < 6.3f)
        {
            while (pos.y > -7.25f)
            {
                bool canPutCoin = true;

                if (-1.25f <= pos.x && pos.x <= 1.25f && 0 <= pos.y && pos.y <= 1)
                    canPutCoin = false;
                else
                {
                    Collider2D[] colls = Physics2D.OverlapBoxAll(pos, new Vector2(0.5f, 0.5f), 0);
                    for (int i = 0; i < colls.Length; i++)
                    {
                        if (colls[i] != null && (colls[i].CompareTag("Map") || colls[i].CompareTag("PrisonEnter")))
                            canPutCoin = false;
                    }
                }

                if (canPutCoin)
                {
                    Transform coin = transform.GetChild(childIndex++);
                    coin.transform.position = pos;
                    coinList.Add(coin);
                }

                pos.y -= 0.5f;
            }
            pos.x += 0.5f;
            pos.y = 7;
        }
    }

    private void MakeCoins()
    {
        getCoinCount = 0;
        List<int> itemPosIndex = new List<int>();
        int itemIndex = 0;

        // 아이템이 생성될 위치를 정한다.
        while (itemPosIndex.Count < 4)
        {
            int index = Random.Range(itemPosIndex.Count * coinList.Count / 4, (itemPosIndex.Count + 1) * coinList.Count / 4);

            if (!itemPosIndex.Contains(index) && !cantPutCoinPos.Contains(coinList[index].transform.position))
                itemPosIndex.Add(index);
        }

        // 위치를 오름차순으로 정렬한다.
        itemPosIndex.Sort();

        // 아이템이 생성될 위치를 제외하고 모든 위치에 코인을 생성한다.
        for (int i = 0; i < coinList.Count; i++)
        {
            if (itemIndex < itemPosIndex.Count && i == itemPosIndex[itemIndex])
            {
                itemList[itemIndex].transform.position = coinList[i].position;
                itemList[itemIndex++].SetActive(true);

                if (coinList[i].gameObject.activeInHierarchy)
                    coinList[i].gameObject.SetActive(false);
            }
            else
                coinList[i].gameObject.SetActive(true);
        }
    }

    public void ResetCoin()
    {
        MakeCoins();
        portal.SetActive(false);
    }

    // 코인을 획득한 경우 호출된다.
    public void GetCoin(GameObject coin)
    {
        audioSource.Play();
        coin.SetActive(false);

        // Player가 모든 코인을 획득했다면 Portal을 활성화하여 다음 스테이지로 넘어갈 수 있도록한다.
        if (++getCoinCount >= coinList.Count - 4)
            portal.SetActive(true);
    }
}
