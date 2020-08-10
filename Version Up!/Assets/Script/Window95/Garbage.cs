using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private const float maxX = 45f;
    private const float minX = -45f;
    
    // Start is called before the first frame update
    void Start()
    {
        float randX = Random.Range(minX, maxX);
        Pop(randX);
    }

    void Pop(float x)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(x * 10, 1000f));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("disposal"))
        {
            Destroy(gameObject);
        }
    }
}
