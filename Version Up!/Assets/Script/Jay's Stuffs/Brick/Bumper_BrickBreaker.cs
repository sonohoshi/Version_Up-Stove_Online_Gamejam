using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper_BrickBreaker : MonoBehaviour
{
    private GameManager_BrickBreaker gm;
    private GameObject bumperBelow;

    public GameObject hitEffectPrefab;
    public float bumperspeed = 5f;
    private bool isMoving = false;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager_BrickBreaker>();
        bumperBelow = GameObject.Find("bumperBelow");
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("충돌");
            gm.GetCurrentPlayer().transform.parent = gameObject.transform;
        }

    }

    void MoveBumper()
    {
        if (gm.hit.transform != null && isMoving == false)
        {
            if (gm.hit.transform.tag == "BelowBlock")
                StartCoroutine(MoveTo(bumperBelow, gm.hit.transform.position));
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveBumper();
    }


    IEnumerator MoveTo(GameObject a, Vector3 toPos)
    {
        float count = 0;
        Vector3 wasPos = a.transform.position;
        while (true)
        {
            isMoving = true;
            count += Time.deltaTime * bumperspeed;
            a.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 1)
            {
                a.transform.position = toPos;
                isMoving = false;
                break;
            }
            yield return null;
        }
    }
}
