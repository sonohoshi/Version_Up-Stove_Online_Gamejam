using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public TextGenerator textGenerator;
    public ClockGenerator clockGenerator;
    public float LimitTime;
    public Text text_Timer;
    int num = 0;

    void Update()
    {

        LimitTime -= Time.deltaTime;
        text_Timer.text = "" + Mathf.Round(LimitTime);

        if (Mathf.Round(LimitTime) <= 0) //타임이 0이 되면
        {
            GetComponent<Text>().color = new Color(0.66f, 0.62f, 0.26f);
            text_Timer.text = "Clear !";
            textGenerator.ListenTimeOver();
            clockGenerator.ListenTimeOver();
            num++;
        }

        if(num==1)
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<Director>().complete(); //문 만들기
        }

        if (Mathf.Round(LimitTime) <= 10 && Mathf.Round(LimitTime)>0)
        {
            ChangeColor();
        } 
        
       
    }

    public void MinusTime()
    {
        LimitTime -= 10; //10초 감소
    }

    public void ResetTime()
    {
        LimitTime = 60;
        GetComponent<Text>().color = new Color(0.4056f, 0.4056f, 0.4056f);
    }

    public void ChangeColor()
    {
        GetComponent<Text>().color = new Color(0.63f, 0.19f, 0.19f);
        
    }

    
}
