using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public bool isEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TextDropper());
    }

    IEnumerator TextDropper()
    {
        while (!isEnded)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 1.0f);

            if (transform.position.y < -5.0f) //바닥에 닿으면 소멸
            {
                Destroy(gameObject);
                break;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

}