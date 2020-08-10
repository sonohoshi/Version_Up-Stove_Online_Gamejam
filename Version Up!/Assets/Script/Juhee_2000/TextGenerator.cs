using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGenerator : MonoBehaviour
{
    public Sprite[] wordSprite;
    public GameObject wordPrefab;
    GameObject spawnedPrefab;

    float span = 5.0f;
    float delta = 0;

    public TextGenerator instance;
    private bool isEnded = false;

    public void ListenTimeOver()
    {
        isEnded = true;
        StopCoroutine(SpawnLetters());
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnLetters());
    }

    // Update is called once per frame
    public IEnumerator SpawnLetters()
    {
        while (!isEnded)
        {
            float x = Random.Range(-8, 4);
            int what = Random.Range(0, 32); // 0번째부터 단어 끝까지
            spawnedPrefab = Instantiate(wordPrefab, new Vector2(x, 3.3f), Quaternion.identity);
            //기본적인 워드 프리팹을 생성
            spawnedPrefab.GetComponent<SpriteRenderer>().sprite = wordSprite[what];
            //프리팹에 스프라이트를 입힘
            spawnedPrefab.AddComponent<BoxCollider2D>();
            spawnedPrefab.GetComponent<BoxCollider2D>().isTrigger = true;
            //BoxCollider 컴포넌트를 추가, isTrigger를 코드상에서 true로 바꿔줌. 
            yield return new WaitForSeconds(1.0f);
        }
    }

   
}

