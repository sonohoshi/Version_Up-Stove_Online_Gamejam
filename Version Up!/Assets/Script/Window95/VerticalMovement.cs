using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{
    private Transform transform;
    private Vector2 scale;
    private Animator anim;
    private bool isRight = true;
    private bool isRunning = false;
    private bool isVerticalMoving = false;

    public static bool canMove;
    
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        transform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        }

        if (!canMove)
        {
            anim.SetBool("isVerticalMoving", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("runningToBack", false);
        }
    }
}
