using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player2000 : MonoBehaviour
{
    public AudioSource jumpSound;
    public AudioSource levelCompleted;
    public AudioSource itemGet;
    public AudioSource hitSound;


    Rigidbody2D rigid2D;
    Animator animator;
    GameObject clockPrefab;
    TimerController timerController;

    public FadeImage fade;

    private bool isEnded = false;
    float jumpForce = 550.0f;
    float walkForce = 4.0f;
    float maxWalkSpeed = 4.0f;
    float fTime;
    int flag = 0;


    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        timerController = GameObject.Find("timer").GetComponent<TimerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FadeImage.isOuted)
        {
            MoveScene.instance.WarpScene7();
        }

        if (!isEnded) // 게임이 끝나면 못움직이도록
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && rigid2D.velocity.y == 0)
            {
                jumpSound.Play();
                this.rigid2D.AddForce(transform.up * this.jumpForce);
            }

            float key = 0;
            //if (Input.GetKey(KeyCode.RightArrow)) key = 1;
            //if (Input.GetKey(KeyCode.LeftArrow)) key = -1;
            key = Input.GetAxis("Horizontal");
            if (key != 0) key = key > 0 ? 1 : -1;

            Vector3 moveVelocity = Vector3.zero;

            if (key < 0)
            {
                transform.localScale = new Vector3(3 * key, 3, 3);
                moveVelocity = Vector3.left;
                animator.SetBool("walk", true);
            }
            else if (key > 0)
            {
                transform.localScale = new Vector3(3 * key, 3, 3);
                moveVelocity = Vector3.right;
                animator.SetBool("walk", true);
            }
            else if(key == 0)
            {
                animator.SetBool("walk", false);
            }

            transform.Translate(moveVelocity * (walkForce * Time.deltaTime));

            // 화면 밖으로 못나가게
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (pos.x < 0.02f) pos.x = 0.02f;
            if (pos.x > 0.78f) pos.x = 0.78f;
            if (pos.y < 0f) pos.y = 0f;
            if (pos.y > 1f) pos.y = 1f;

            transform.position = Camera.main.ViewportToWorldPoint(pos);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("clock")) //시계가 닿으면
        {
            itemGet.Play();
            Debug.Log("시계");
            timerController.MinusTime();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("word")) //단어가 닿으면
        {
            hitSound.Play();
            Debug.Log("게임 오버");
            timerController.ResetTime();//시간 다시
            transform.position = new Vector3(-8.4f, -4.7f, 0);//플레이어 위치 다시

            GameObject[] destroyWord = GameObject.FindGameObjectsWithTag("word"); //단어 없애기
            foreach(GameObject destroy1 in destroyWord)
            {
                Destroy(destroy1);
            }

            GameObject[] destroyClock = GameObject.FindGameObjectsWithTag("clock"); //시계 없애기
            foreach(GameObject destroy2 in destroyClock)
            {
                Destroy(destroy2);
            }

            GameObject door = GameObject.FindWithTag("door");
            if (door != null)
            {
                Destroy(door);
            }
        }

        if(collision.CompareTag("door")) //문에 닿으면
        {
            if(flag <= 0)
            {
                levelCompleted.Play();
                isEnded = true;
                Debug.Log("다음 스테이지");
                GameObject[] words = GameObject.FindGameObjectsWithTag("word");
                foreach (var word in words)
                {
                    word.GetComponent<TextController>().isEnded = true;
                }
                fade.PublicFadeOut();
                flag++;
            }
        }
    }
}
