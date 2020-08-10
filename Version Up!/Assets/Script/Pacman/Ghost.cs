using Com.LuisPedroFonseca.ProCamera2D.TopDownShooter;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Animator anim;
    private Collider2D coll;

    public enum State { NONE = 0, CONFUSED = 1, GOPRISON = 2, INPRISON = 3 }
    public State curState { get; private set; }
    [HideInInspector]
    public float moveSpeed = 0.05f;

    [HideInInspector]
    public Vector2 startPos, targetPos;
    private Vector2[] dir = { new Vector2(0, 0.25f), new Vector2(0.25f, 0), new Vector2(0, -0.25f), new Vector2(-0.25f, 0) };
    private int curDirIndex = 0;

    private float turnTime = 0;
    private float baseTurnTime = 0.65f;
    private float inPrisonTime = 0;
    public float baseInPrisonTime;

    private float rayDistance = 0.6f;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        anim.SetInteger("dir", curDirIndex);

        startPos = transform.position;
        targetPos = transform.position;
        SetState(State.NONE);
    }

    private void Update()
    {
        if (curState == State.NONE || curState == State.CONFUSED)
        {
            turnTime -= Time.fixedDeltaTime;

            // 왼쪽과 오른쪽으로 벗어나면 반대편에서 나타난다.
            if (transform.position.x <= -6.8f)
            {
                transform.position = new Vector2(6.8f, transform.position.y);
                targetPos = new Vector2(6.75f, transform.position.y);               
            }
            else if (transform.position.x >= 6.8f)
            {
                transform.position = new Vector2(-6.8f, transform.position.y);
                targetPos = new Vector2(-6.75f, transform.position.y);
            }

            // 한 칸 이동
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed);

            // 한 칸 이동했으면 전진할 수 있는지 우선 확인한다.
            if (((Vector2)transform.position - targetPos).sqrMagnitude < 0.01f)
            {
                transform.position = targetPos;

                if (!CheckCanGo(curDirIndex))
                    Turn();
                else
                {
                    if (turnTime <= 0)
                        Turn();
                    else
                        targetPos += dir[curDirIndex];
                }
            }
        }

        // Player에게 잡힌 경우
        else if (curState == State.GOPRISON)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 0.1f);
            if (((Vector2)transform.position - targetPos).sqrMagnitude < 0.01f)
                SetState(State.INPRISON);
        }

        // 감옥에 있는 경우
        else if (curState == State.INPRISON)
        {
            inPrisonTime += Time.fixedDeltaTime;

            // 감옥에 있는 동안은 좌우로만 움직인다.
            if (inPrisonTime < baseInPrisonTime)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed);

                if (((Vector2)transform.position - targetPos).sqrMagnitude < 0.01f)
                {
                    transform.position = targetPos;
                    if (!CheckCanGo(curDirIndex))
                    {
                        curDirIndex = curDirIndex == 1 ? 3 : 1;
                        targetPos += dir[curDirIndex];
                        anim.SetInteger("dir", curDirIndex);
                    }
                    else
                        targetPos += dir[curDirIndex];
                }
            }

            // 감옥에 있어야할 시간을 다 채웠다면 밖으로 다시 나간다.
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed);
                if (((Vector2)transform.position - targetPos).sqrMagnitude < 0.01f)
                {
                    transform.position = new Vector2(0, 0.5f);
                    curDirIndex = 0;
                    targetPos = new Vector2(0, 2);
                    SetState(State.NONE);
                }
            }
        }
    }

    // 상태를 변경한다.
    public void SetState(State newState)
    {
        switch (newState)
        {
            case State.NONE:
                {
                    coll.enabled = true;
                    turnTime = baseTurnTime;
                    inPrisonTime = 0;
                    moveSpeed = 0.05f;
                    anim.SetBool("isConfused", false);
                    anim.SetBool("isCatched", false);
                    anim.SetInteger("dir", curDirIndex);
                    break;
                }
            case State.CONFUSED:
                {
                    moveSpeed = 0.03f;
                    curDirIndex = (curDirIndex + 2) % 4;
                    anim.SetBool("isConfused", true);
                    anim.SetInteger("dir", curDirIndex);
                    break;
                }
            case State.GOPRISON:
                {
                    coll.enabled = false;
                    targetPos = new Vector2(0, 0.5f);
                    anim.SetBool("isConfused", false);
                    anim.SetBool("isCatched", true);
                    break;
                }
            case State.INPRISON:
                {
                    coll.enabled = true;
                    inPrisonTime = 0;
                    curDirIndex = 1;
                    moveSpeed = 0.05f;
                    anim.SetBool("isConfused", false);
                    anim.SetBool("isCatched", false);
                    anim.SetInteger("dir", curDirIndex);
                    break;
                }
        }

        curState = newState;
    }

    // 방향을 변경한다.
    // 왼쪽으로 회전하는 경우 인자로 -1을, 오른쪽인 경우 +1을 넘겨준다.
    private int GetDirection(bool left)
    {
        int newDirIndex;
        if (left)
        {
            newDirIndex = curDirIndex - 1;
            if (newDirIndex < 0)
                newDirIndex = dir.Length - 1;
        }
        else
        {
            newDirIndex = curDirIndex +  1;
            if (newDirIndex >= dir.Length)
                newDirIndex = 0;
        }

        return newDirIndex;
    }

    // 해당 방향으로 Ray를 쏴 벽의 존재 여부를 판단하여 이동 가능한지 확인한다.
    private bool CheckCanGo(int dirIndex)
    {
        bool canGo = true;

        Collider2D[] colls = Physics2D.OverlapBoxAll((Vector2)transform.position + dir[dirIndex], new Vector2(0.55f, 0.55f), 0);
        //Collider2D[] colls = Physics2D.OverlapCircleAll((Vector2)transform.position + dir[dirIndex], 0.37f);
        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].CompareTag("Map") || colls[i].CompareTag("PrisonEnter"))
            {
                canGo = false;
                break;
            }
        }

        return canGo;
    }

    private void Turn()
    {
        // 왼쪽과 오른쪽으로 이동할 수 있는지 확인한다.
        int leftDirIndex = GetDirection(true);
        bool canGoLeft = CheckCanGo(leftDirIndex);

        int rightDirIndex = GetDirection(false);
        bool canGoRight = CheckCanGo(rightDirIndex);

        // 왼쪽과 오른쪽 전부 이동 가능하다면 무작위로 둘 중에 한 곳으로 이동한다.
        if (canGoLeft && canGoRight)
            curDirIndex = 0 == Random.Range(0, 2) ? leftDirIndex : rightDirIndex;
        else if (canGoLeft)
            curDirIndex = leftDirIndex;
        else if (canGoRight)
            curDirIndex = rightDirIndex;

        if (canGoLeft || canGoRight)
        {
            // 애니메이션 변경
            anim.SetInteger("dir", curDirIndex);

            // 방향을 바꾸었으므로 turnTime 초기화
            turnTime = baseTurnTime;
        }

        targetPos = (Vector2)transform.position + dir[curDirIndex];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, dir[0] * rayDistance);
        Gizmos.DrawRay(transform.position, dir[1] * rayDistance);
        Gizmos.DrawRay(transform.position, dir[2] * rayDistance);
        Gizmos.DrawRay(transform.position, dir[3] * rayDistance);
    }
}
