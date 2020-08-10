using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -0.07f, 0); //등속 운동

        if(transform.position.y<-5.0f) //바닥에 닿으면 소멸
        {
            Destroy(gameObject);
        }
    }
}
