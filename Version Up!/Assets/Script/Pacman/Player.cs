using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public FadeImage fadeImage;

    private AudioSource audioSource;
    public AudioSource levelCompleted;
    public AudioClip dieSound;
    public AudioClip catchEnemySound;
    private Collider2D coll;

    public int moveSpeed;

    private Vector2 scale;
    private Animator anim;
    private bool isRight = true;
    private bool isRunning = false;
    private bool isVerticalMoving = false;
    public static bool canMove;

    private Vector2 startPos = new Vector2(-6.25f, 0.5f);

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        coll = GetComponent<Collider2D>();

        transform.position = startPos;
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 코인을 획득한 경우.
        if (collision.CompareTag("Coin"))
            Coin.instance.GetCoin(collision.gameObject);

        // Portal에 들어간 경우 다음 스테이지(윈도우95) 씬 로드.
        else if (collision.CompareTag("Portal"))
        {
            canMove = false;
            coll.enabled = false;

            levelCompleted.Play();
            fadeImage.PublicFadeOut();
        }

        // Ghost와 부딪힌 경우.
        else if (collision.CompareTag("Ghost"))
        {
            Ghost ghostScript = collision.GetComponent<Ghost>();

            // Ghost를 잡을 수 없는 경우 게임을 재시작한다.
            if (ghostScript.curState != Ghost.State.CONFUSED)
            {
                audioSource.clip = dieSound;
                audioSource.Play();

                // Player의 위치, Ghost의 위치, 코인 & 아이템을 전부 초기 상태로 복원한다.
                canMove = false;
                coll.enabled = false;
                Invoke("Restart", 0.2f);
            }

            // Ghost를 잡을 수 있는 경우 Ghost는 감옥으로 보내진다.
            else
            {
                audioSource.clip = catchEnemySound;
                audioSource.Play();

                ghostScript.SetState(Ghost.State.GOPRISON);
            }
        }

        // 아이템을 획득한 경우.
        else if (collision.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);

            // Ghost의 속도를 늦추었다가 3초 뒤에 원래대로 되돌린다.
            GhostManager.instance.SetConfused();
            GhostManager.instance.CancelInvoke();
            GhostManager.instance.Invoke("RevertConfused", 3);
        }
    }

    private void Update()
    {
        if (FadeImage.isOuted)
            SceneManager.LoadScene("KimSM");

        Vector3 worldPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worldPos.x < 0f) worldPos.x = 0f;
        if (worldPos.y < 0f) worldPos.y = 0f;
        if (worldPos.x > 1f) worldPos.x = 1f;
        if (worldPos.y > 1f) worldPos.y = 1f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worldPos);

        if (canMove)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal < 0)
            {
                if (isRight)
                {
                    isRight = !isRight;
                    scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
            }
            else if (horizontal > 0)
            {
                if (!isRight)
                {
                    isRight = !isRight;
                    scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
            }
            if (horizontal != 0)
            {
                anim.SetBool("isRunning", true);
                isRunning = true;
            }

            if (vertical != 0)
            {
                isVerticalMoving = true;
                anim.SetBool("isVerticalMoving", true);
            }
            transform.Translate(horizontal * Time.deltaTime * 5f, vertical * Time.deltaTime * 5f, 0);

            if (isRunning && isVerticalMoving)
            {
                anim.SetBool("runningToBack", true);
            }
            else
            {
                anim.SetBool("runningToBack", false);
            }

            if (horizontal == 0)
            {
                anim.SetBool("isRunning", false);
                isRunning = false;
            }

            if (vertical == 0)
            {
                anim.SetBool("isVerticalMoving", false);
                isVerticalMoving = false;
            }

            if (transform.position.x <= -7f)
                transform.position = new Vector2(6.9f, transform.position.y);

            if (transform.position.x >= 7f)
                transform.position = new Vector2(-6.9f, transform.position.y);
        }

        if (!canMove)
        {
            anim.SetBool("isVerticalMoving", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("runningToBack", false);
        }
    }

    private void Restart()
    {
        canMove = true;
        coll.enabled = true;

        transform.position = startPos;
        Coin.instance.ResetCoin();
        GhostManager.instance.ResetAllGhosts();
    }
}
