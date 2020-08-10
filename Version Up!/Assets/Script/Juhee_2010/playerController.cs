using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class playerController : MonoBehaviour
{

    public AudioSource jumpSound;
    public AudioSource levelCompleted;
    public AudioSource itemGet;
    public AudioSource hitSound;

    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 550.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 3.5f;



    public float jump = 10.0f;
    public float walkSpeed = 10.0f;
    public bool isGrounded;
    public bool doubleJumpAllowed;
    GameObject v, e, r, s, i, o, n, u, p;
    GameObject dinosaur;
    public int score=0;


    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();

        this.dinosaur = GameObject.Find("dinosaur");
        this.v = GameObject.Find("V");
        this.e = GameObject.Find("E");
        this.r = GameObject.Find("R");
        this.s = GameObject.Find("S");
        this.i = GameObject.Find("I");
        this.o = GameObject.Find("O");
        this.n = GameObject.Find("N");
        this.u = GameObject.Find("U");
        this.p = GameObject.Find("P");

    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space)&&this.rigid2D.velocity.y==0)
        //{
        //    jumpSound.Play();
        //    this.rigid2D.AddForce(transform.up * this.jumpForce);
        //}



        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        Vector3 horizontal = new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
        transform.position = transform.position + horizontal * walkSpeed * Time.deltaTime;

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpSound.Play();
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jump);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJumpAllowed)
        {
            jumpSound.Play();
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jump);
            doubleJumpAllowed = false;
        }

        // 화면 밖으로 못나가게
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        if (pos.x < 0f) pos.x = 0f;
        if (pos.x > 1f) pos.x = 1f;
        if (pos.y < 0f) pos.y = 0f;
        if (pos.y > 1f) pos.y = 1f;

        //transform.position = Camera.main.ViewportToWorldPoint(pos); //

        if (score == 9) //모든 알파벳을 모으면
        {
            GameObject director = GameObject.Find("search");
            director.GetComponent<GameDirector>().complete();
            score++;
        }

        
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isGrounded = true;
            doubleJumpAllowed = true;
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    public void Restart()
    {
        score = 0;
        Destroy(GameObject.FindGameObjectWithTag("search"));

        transform.position = new Vector3(-8.0f, -4.5f, 0); //플레이어 위치 다시

        this.dinosaur.transform.localPosition = new Vector3(11.88f, -4.3f, 0); //공룡 위치 다시

        this.v.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        this.e.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        this.r.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        this.s.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        this.i.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        this.o.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        this.n.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        this.u.transform.localScale = new Vector3(0.7f, 0.7f, 0);
        this.p.transform.localScale = new Vector3(0.7f, 0.7f, 0);//알파벳 크기

        this.v.transform.localPosition = new Vector3(-2.75f, -2.87f, 0);
        this.e.transform.localPosition = new Vector3(4.1f, -1.26f, 0);
        this.r.transform.localPosition = new Vector3(7.77f, 0.057f, 0);
        this.s.transform.localPosition = new Vector3(9.43f, 1.22f, 0);
        this.i.transform.localPosition = new Vector3(-0.43f, 0.11f, 0);
        this.o.transform.localPosition = new Vector3(-2.86f, 2.38f, 0);
        this.n.transform.localPosition = new Vector3(6.90f, 3.83f, 0);
        this.u.transform.localPosition = new Vector3(0.99f, 2.54f, 0);
        this.p.transform.localPosition = new Vector3(10.15f, -1.88f, 0);
        //알파벳 위치 다시
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("enemy"))
        {
            hitSound.Play();
            Debug.Log("게임 오버");
            this.Restart();
        }
        else if(collision.CompareTag("v"))
        {
            itemGet.Play();
            score++;
            Debug.Log(score);
            this.v.transform.localScale = new Vector3(0.4f, 0.4f, 0);
            this.v.transform.position = new Vector3(-2.5f, 0.4f, 0);
            
        }
        else if (collision.CompareTag("e"))
        {
            itemGet.Play();
            score++;
            Debug.Log(score);
            this.e.transform.localScale = new Vector3(0.4f, 0.4f, 0);
            this.e.transform.position = new Vector3(-2.0f, 0.4f, 0);
        }
        else if (collision.CompareTag("r"))
        {
            itemGet.Play();
            score++;
            Debug.Log(score);
            this.r.transform.localScale = new Vector3(0.4f, 0.4f, 0);
            this.r.transform.position = new Vector3(-1.5f, 0.4f, 0);
        }
        else if (collision.CompareTag("s"))
        {
            itemGet.Play();
            score++;
            Debug.Log(score);
            this.s.transform.localScale = new Vector3(0.4f, 0.4f, 0);
            this.s.transform.position = new Vector3(-1.0f, 0.4f, 0);


        }
        else if (collision.CompareTag("i"))
        {
            itemGet.Play();
            score++;
            Debug.Log(score);
            this.i.transform.localScale = new Vector3(0.4f, 0.4f, 0);
            this.i.transform.position = new Vector3(-0.5f, 0.4f, 0);

        }
        else if (collision.CompareTag("o"))
        {
            itemGet.Play();
            score++;
            Debug.Log(score);
            this.o.transform.localScale = new Vector3(0.4f, 0.4f, 0);
            this.o.transform.position = new Vector3(0f, 0.4f, 0);

        }
        else if (collision.CompareTag("n"))
        {
            itemGet.Play();
            score++;
            Debug.Log(score);
            this.n.transform.localScale = new Vector3(0.4f, 0.4f, 0);
            this.n.transform.position = new Vector3(0.5f, 0.4f, 0);

        }
        else if (collision.CompareTag("u"))
        {
            itemGet.Play();
            score++;
            Debug.Log(score);
            this.u.transform.localScale = new Vector3(0.4f, 0.4f, 0);
            this.u.transform.position = new Vector3(1.2f, 0.4f, 0);

        }
        else if (collision.CompareTag("p"))
        {
            itemGet.Play();
            score++;
            Debug.Log(score);
            this.p.transform.localScale = new Vector3(0.4f, 0.4f, 0);
            this.p.transform.position = new Vector3(1.7f, 0.4f, 0);

        }

        else if(collision.CompareTag("search"))
        {
            itemGet.Play();
            levelCompleted.Play();
            Debug.Log("다음 스테이지"); 
            SceneManager.LoadScene("Ending");
        }
    }
}
