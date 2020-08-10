using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EndingCreditScroll : MonoBehaviour
{
    public int speed;
    public GameObject[] uiList;

    private void Update()
    {
        for(int i=0; i<uiList.Length; i++)
        {
            uiList[i].transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
    }
}
