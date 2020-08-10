using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockGenerator : MonoBehaviour
{
    public GameObject clockPrefab;
    float span = 5.0f;
    float delta = 0;

    private bool isEnded = false;

    // Update is called once per frame
    void Update()
    {
        if (!isEnded)
        {
            this.delta += Time.deltaTime;
            if(this.delta>this.span)
            {
                this.delta = 0;
                GameObject go = Instantiate(clockPrefab) as GameObject;
                int px = Random.Range(-8, 4);
                go.transform.position = new Vector3(px, 3.3f, 0);
            }
        }
    }

    public void ListenTimeOver()
    {
        isEnded = true;
    }
}
