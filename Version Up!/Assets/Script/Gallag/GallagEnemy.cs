using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallagEnemy : MonoBehaviour
{
    public static GameObject bulletHolder;
    public static List<GameObject> bulletList = new List<GameObject>();

    public enum state { NONE = 0, FIRSTMOVE = 1, ROTATE = 2, SECONDMOVE = 3, ATTACK = 4, FINISHATTACK = 5 };

    public static GameObject explosionHolder;
    public static List<GameObject> explosionList = new List<GameObject>();

    private AudioSource audioSource;

    public float moveSpeed;
    public Vector2 rotatePos;
    public Vector2 firstTargetPos;
    public Vector2 secondTargetPos;
    public float keepRotate;
    public int rotateDir;
    private float rotateAmount = 0;
    private float attackCoolTime = 0.03f;
    private float attackCount = 0;
    private float keepAttackCount = 3;

    private bool chooseAttackPos = true;
    private bool flyNearAttackPos = false;
    private bool moveDownAttackPos = false;
    private bool attack = false;
    private Vector2 attackPos;
    private Vector2 nearAttackPos;

    public state curState = state.FIRSTMOVE;

    private Transform playerTr;
    private Transform squadTr;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        bulletHolder = GameObject.Find("EnemyBulletHolder");
        for (int i = 0; i < bulletHolder.transform.childCount; i++)
            bulletList.Add(bulletHolder.transform.GetChild(i).gameObject);

        explosionHolder = GameObject.Find("ExplosionHolder");
        for (int i = 0; i < explosionHolder.transform.childCount; i++)
            explosionList.Add(explosionHolder.transform.GetChild(i).gameObject);
    }

    private void Start()
    {
        GallagEnemyController.instance.enemyList.Add(gameObject);
        playerTr = GallagPlayer.instance.transform;
        squadTr = GallagEnemySquadMove.instance.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            explosionList[0].SetActive(true);
            explosionList[0].transform.position = transform.position;
            explosionList.RemoveAt(0);

            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Map_Bottom") && curState != state.ATTACK)
        {
            chooseAttackPos = true;
            transform.position = new Vector2(transform.position.x, 11);
            curState = state.SECONDMOVE;
        }

        else if (collision.CompareTag("Player"))
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if (curState == state.FIRSTMOVE)
        {
            transform.up = firstTargetPos - (Vector2)transform.position;
            transform.position = Vector2.MoveTowards(transform.position, firstTargetPos, 0.1f);
            if ((firstTargetPos - (Vector2)transform.position).sqrMagnitude < 0.001f)
                curState = state.ROTATE;
        }
        else if(curState == state.ROTATE && rotateAmount < keepRotate)
        {
            rotateAmount += 3;
            transform.RotateAround(rotatePos, Vector3.forward, rotateDir * 3f);
            if (rotateAmount >= keepRotate)
            {
                curState = state.SECONDMOVE;
                rotateAmount = 0;
            }
        }
        else if(curState == state.SECONDMOVE)
        {
            transform.up = secondTargetPos + (Vector2)squadTr.position - (Vector2)transform.position;
            transform.position = Vector2.MoveTowards(transform.position, secondTargetPos + (Vector2)squadTr.position, 0.1f);
            if ((secondTargetPos + (Vector2)squadTr.position - (Vector2)transform.position).sqrMagnitude < 0.001f)
            {
                transform.up = Vector2.up;
                curState = state.NONE;
            }
        }
        else if(curState == state.NONE)
        {
            transform.parent = squadTr;
        }
        // 이동하면서 공격
        else if(curState == state.ATTACK)
        {
            // 공격 Pos 설정
            if(chooseAttackPos)
            {
                transform.parent = null;

                float random = Random.Range(0, 2f);
                if (transform.position.x < playerTr.position.x)
                {
                    attackPos = (Vector2)playerTr.position + new Vector2(random, 0);
                    nearAttackPos = attackPos + new Vector2(3, 2);
                    rotateDir = -1;
                }
                else
                {
                    attackPos = (Vector2)playerTr.position - new Vector2(random, 0);
                    nearAttackPos = attackPos + new Vector2(-3, 2);
                    rotateDir = +1;
                }

                chooseAttackPos = false;
                flyNearAttackPos = true;
            }

            // 공격 Pos 근처로 이동
            else if(flyNearAttackPos)
            {
                if (attackCount < keepAttackCount)
                {
                    attackCoolTime -= Time.fixedDeltaTime;

                    // 공격
                    if (attackCoolTime <= 0)
                    {
                        GameObject bullet = GetBullet();
                        bullet.transform.position = (Vector2)transform.position + new Vector2(0, -1f);
                        bullet.transform.up = Vector2.down;
                        bullet.SetActive(true);
                        attackCoolTime = 0.03f;

                        audioSource.Play();
                    }

                    attackCount++;
                }

                transform.up = nearAttackPos - (Vector2)transform.position;
                transform.position = Vector2.MoveTowards(transform.position, nearAttackPos, 0.1f);
                if(((Vector2)transform.position - nearAttackPos).sqrMagnitude < 0.01f)
                {
                    flyNearAttackPos = false;
                    moveDownAttackPos = true;
                }
            }

            // 아래로 이동
            else if(moveDownAttackPos)
            {
                transform.up = Vector2.down;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(nearAttackPos.x, playerTr.position.y), 0.1f);
                if(((Vector2)transform.position - new Vector2(nearAttackPos.x, playerTr.position.y)).sqrMagnitude < 0.01f)
                {
                    moveDownAttackPos = false;
                    attack = true;
                    keepRotate = 360;
                }
            }

            // 공격
            else if(attack)
            {
                if (rotateAmount <= keepRotate)
                {
                    rotateAmount += 3;
                    transform.RotateAround(new Vector2((attackPos.x + nearAttackPos.x) / 2, playerTr.position.y), Vector3.forward, rotateDir * 3);
                }
                else
                {
                    attack = false;
                    curState = state.FINISHATTACK;
                    rotateAmount = 0;
                }
            }
        }

        // 공격 종료
        else if (curState == state.FINISHATTACK)
        {
            attackCoolTime = 0.03f;
            attackCount = 0;
            transform.Translate(-transform.up * moveSpeed * Time.deltaTime);
        }
    }

    private static GameObject GetBullet()
    {
        GameObject bullet = bulletList[0];
        bulletList.RemoveAt(0);
        return bullet;
    }

    public static void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = Vector3.zero;
        bulletList.Add(bullet);
    }
}
