using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm;
    void Awake()
    {
        Screen.SetResolution(Screen.width * 16 / 9, Screen.width * 16 / 9, true);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        bgm.GetComponent<AudioSource>();
        bgm.Play();
    }
    /*
    void Update() //쓸곳이 없어서 여기에 치트를 쓴다
    {
        if (Input.GetKeyDown(KeyCode.F1))
            MoveScene.instance.WarpScene0();
                                         
        if (Input.GetKeyDown(KeyCode.F1))
            MoveScene.instance.WarpScene1();
                                         
        if (Input.GetKeyDown(KeyCode.F2))
            MoveScene.instance.WarpScene2();
                                         
        if (Input.GetKeyDown(KeyCode.F3))
            MoveScene.instance.WarpScene3();

        if (Input.GetKeyDown(KeyCode.F4))
            MoveScene.instance.WarpScene4();

        if (Input.GetKeyDown(KeyCode.F5))
            MoveScene.instance.WarpScene5();

        if (Input.GetKeyDown(KeyCode.F6))
            MoveScene.instance.WarpScene6();

        if (Input.GetKeyDown(KeyCode.F7))
            MoveScene.instance.WarpScene7();

        if (Input.GetKeyDown(KeyCode.F8))
            MoveScene.instance.WarpScene8();

        if (Input.GetKeyDown(KeyCode.F9))
            MoveScene.instance.WarpScene9();
    }
    */
}
