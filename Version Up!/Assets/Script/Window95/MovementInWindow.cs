using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInWindow : MonoBehaviour
{
    private Transform transform;
    
    public static bool canMove;
    public BlueScreen blueScreen;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.up);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.down);
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right);
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Icon"))
        {
            Debug.Log(col.gameObject.name);
            if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space)) && canMove)
            {
                Debug.Log("Key");
                col.gameObject.GetComponent<Icons>().ShowMethods(transform.position);
                blueScreen.DoInteract();
                canMove = false;
            }
        }
    }
}
