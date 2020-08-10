using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongKirby : MonoBehaviour
{
    public Animator animator;

    public AudioSource hitSound;
    public AudioSource jumpSound;

    public Rigidbody2D rb;
    public float jump = 10.0f;
    public float walkSpeed = 10.0f;
    public bool isGrounded;
    public bool doubleJumpAllowed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D (Collision2D coll)
    {
        //공이랑 닿으면 자기자신 삭제
        if (coll.gameObject.tag == "Ball")
        {
            hitSound.Play();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D (Collider2D coll)
    {
        //Ground 태그 오브젝트와 접촉했을때, 땅보다 플레이어 y값이 높아야 땅을 딛는걸로 판정
        if (coll.gameObject.tag == "Ground") 
        {
            isGrounded = true;
            doubleJumpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void CheckPlayerOut()
    {
        //플레이어 낙사 체크
        if (gameObject.transform.position.y < -30.0f)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        CheckPlayerOut();

        float h = Input.GetAxis("Horizontal");
        animator.SetFloat("Horizontal", h);
        Vector3 horizontal = new Vector3(h, 0.0f, 0.0f);
        transform.position = transform.position + horizontal * walkSpeed * Time.deltaTime;

        //점프
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && doubleJumpAllowed)
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jump);
            doubleJumpAllowed = false;
        }
    }
}
