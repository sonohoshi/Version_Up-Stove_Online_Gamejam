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
            TextGenerator.ListenTimeOver(); // 타이머 다 됐다고 텍스트 그만 만들라고 전달하는 코드에요!
            ClockGenerator.ListenTimeOver(); // 얘도 똑같이 만들어줬어요!
        }
    }

    public void MinusTime()
    {
        LimitTime -= 10; //10초 감소
    }
}
