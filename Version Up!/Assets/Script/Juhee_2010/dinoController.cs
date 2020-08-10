using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinoController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    float jumpForce = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float move = -1 * 6 * Time.deltaTime;
        this.transform.Translate(new Vector3(move, 0, 0));

        if (transform.position.x<=7.5f&& transform.position.x >= 7.1f)
        {
            this.rigid2D.velocity = new Vector3(0, 8, 0);
        }

        if (transform.position.x <= -3.5f && transform.position.x >= -3.9f)
        {
            this.rigid2D.velocity = new Vector3(0, 8, 0);
        }

        if(transform.position.x < -12.0f)
        {
            this.transform.localPosition = new Vector3(12, -4.3f, 0);
        }

    }

}
