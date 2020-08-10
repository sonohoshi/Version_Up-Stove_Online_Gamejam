using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private GameManager gm;
    private GameObject bumperLeft;
    private GameObject bumperRight;
    public float bumperspeed = 5f;
    private bool isMoving = false;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        bumperLeft = GameObject.Find("bumperLeft");
        bumperRight = GameObject.Find("bumperRight");
    }

    void MoveBumper()
    {
        if (gm.hit.transform != null && isMoving == false)
        {
            if (gm.hit.transform.tag == "LeftBlock")
                StartCoroutine(MoveTo(bumperLeft, gm.hit.transform.position));
            if (gm.hit.transform.tag == "RightBlock")
                StartCoroutine(MoveTo(bumperRight, gm.hit.transform.position));
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
