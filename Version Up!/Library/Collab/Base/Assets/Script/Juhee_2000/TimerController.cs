using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float LimitTime;
    public Text text_Timer;

    
    // Update is called once per frame
    void Update()
    {
        LimitTime -= Time.deltaTime;
        text_Timer.text = ""+Mathf.Round(LimitTime);
        
        if(Mathf.Round(LimitTime)<=0) //타임이 0이 되면
        {
            text_Timer.text = "Clear !";
        }
    }

    public void MinusTime()
    {
        LimitTime -= 10; //10초 감소
    }
}
