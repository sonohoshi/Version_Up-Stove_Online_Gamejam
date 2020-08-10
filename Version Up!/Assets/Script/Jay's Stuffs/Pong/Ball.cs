using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb;
    AudioSource audioSource;
    public float speed = 5f;

    float sx;
    float sy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        BallInit();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        float xF;
        float yF;
        xF = Random.Range(0, 2) == 0 ? -1 : 1;
        yF = Random.Range(1.0f, 1.5f);
        if (Mathf.Abs(rb.velocity.x) < 50.0f && Mathf.Abs(rb.velocity.y) < 50.0f) //최대속도
            rb.AddForce(new Vector2(rb.velocity.x * xF, rb.velocity.y * yF));
        //벽에 부딪힐때마다 x, y축에 랜덤한 힘 부여!
        audioSource.Play();
    }



    void BallInit()
    {
        sx = Random.Range(0, 2) == 0 ? -1 : 1;
        sy = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.velocity = new Vector2(sx * speed, sy * speed);
    }

}
