using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_BrickBreaker : MonoBehaviour
{
    Rigidbody2D rb;
    public RaycastHit2D hit;
    //hit은 공의 ray가 가리키는 identical 블럭

    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public GameObject keyPrefab;
    public GameObject brickPrefab;
    public GameObject gatePrefab;

    GameObject spawned_ball;
    GameObject spawned_player;
    GameObject spawned_brick;

    public GameObject belowBumper;
    public Transform Ball_spawnPoint;
    public Transform Player_spawnPoint;
    public Transform[] Key_spawnPoint;
    public Transform Gate_spawnPoint;

    public int HowmanyKeyhave = 0;
    public bool isStageCleared = false;

    public FadeImage fadeImage;

    void Start()
    {
        PlayerSet();
        BallSet();
        KeySet();
        BrickSet();
        GateSet();
    }

    void PlayerSet()
    {
        spawned_player = Instantiate(playerPrefab, Player_spawnPoint.position, Player_spawnPoint.rotation);
    }

    void BallSet()
    {
        spawned_ball = Instantiate(ballPrefab, Ball_spawnPoint.position, Ball_spawnPoint.rotation);
        rb = spawned_ball.GetComponent<Rigidbody2D>();
    }

    void KeySet()
    {
        Instantiate(keyPrefab, Key_spawnPoint[0].position, Key_spawnPoint[0].rotation);
        Instantiate(keyPrefab, Key_spawnPoint[1].position, Key_spawnPoint[1].rotation);
    }

    void BrickSet()
    {
        spawned_brick = Instantiate(brickPrefab, Vector3.zero, Quaternion.identity);
    }

    void GateSet()
    {
        Instantiate(gatePrefab, Gate_spawnPoint.position, Gate_spawnPoint.rotation);
    }

    public GameObject GetCurrentPlayer()
    {
        return spawned_player;
    }

    void removeBall()
    {
        if (spawned_ball.transform.position.y < -30 || Mathf.Abs(spawned_ball.transform.position.x) > 100)
        {
            Destroy(spawned_ball);
            BallSet();
            belowBumper.transform.position = new Vector3(0, -9, 0);
        }
    }

    void restartPlayer()
    {
        if (spawned_player == null)
        {
            PlayerSet();
            Destroy(spawned_ball);
            BallSet();
            belowBumper.transform.position = new Vector3(0, -9, 0);

            HowmanyKeyhave = 0;
            Destroy(GameObject.FindGameObjectWithTag("KeyItem"));
            KeySet();

            Destroy(spawned_brick);
            BrickSet();
        }
    }

    public void clearStage()
    {
        isStageCleared = true;
        Destroy(spawned_ball);
        spawned_player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
        StartCoroutine(MoveTo(spawned_player.gameObject, Gate_spawnPoint.transform.position));
        fadeImage.PublicFadeOut();
    }

    void Update()
    {
        if (isStageCleared)
        {
            if (FadeImage.isOuted)
                MoveScene.instance.WarpScene3();
            return;
        }

        removeBall();
        restartPlayer();

        //layermask로 IdenticalBlock 레이어만 레이캐스팅
        int layerMask = 1 << LayerMask.NameToLayer("IdenticalBlock");
        //Debug.DrawRay(rb.position, new Vector3(rb.velocity.x, rb.velocity.y, 0f));
        hit = Physics2D.Raycast(rb.position, new Vector3(rb.velocity.x, rb.velocity.y, 0f), Mathf.Infinity, layerMask);
    }

    IEnumerator MoveTo(GameObject a, Vector3 toPos)
    {
        float count = 0;
        Vector3 wasPos = a.transform.position;
        while (true)
        {
            count += Time.deltaTime;
            a.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 3)
            {
                a.transform.position = toPos;
                break;
            }
            yield return null;
        }
    }

}
