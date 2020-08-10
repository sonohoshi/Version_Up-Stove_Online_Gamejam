using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    SpriteRenderer sp;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        StartCoroutine(colorChange());
    }

    void Update()
    {
        if (Input.anyKeyDown)
            MoveScene.instance.WarpScene0();
    }

    IEnumerator colorChange()
    {
        int index = 1;
        while (true)
        {
            index++;
            switch (index)
            {
                case 1:
                    sp.color = Color.red;
                    break;
                case 2:
                    sp.color = Color.white;
                    break;
                case 3:
                    sp.color = Color.blue;
                    break;
                case 4:
                    sp.color = Color.cyan;
                    break;
                case 5:
                    sp.color = Color.gray;
                    break;
                case 6:
                    sp.color = Color.green;
                    break;
                case 7:
                    sp.color = Color.magenta;
                    break;
                case 8:
                    sp.color = Color.red;
                    break;
                case 9:
                    sp.color = Color.yellow;
                    index = 1;
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
