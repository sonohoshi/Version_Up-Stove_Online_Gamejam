using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    static public MoveScene instance;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void WarpScene0() //스토리
    {
        SceneManager.LoadScene("Story");
    }
    public void WarpScene1() //퐁게임
    {
        SceneManager.LoadScene("PongGame");
    }
    public void WarpScene2() //벽돌깨기
    {
        SceneManager.LoadScene("BrickBreaker");
    }
    public void WarpScene3() //갤러그
    {
        SceneManager.LoadScene("Gallag");
    }
    public void WarpScene4() //팩맨
    {
        SceneManager.LoadScene("PackMan");
    }
    public void WarpScene5() //윈95
    {
        SceneManager.LoadScene("KimSM");
    }
    public void WarpScene6() //산성비
    {
        SceneManager.LoadScene("Juhee_2000");
    }

    public void WarpScene7() //구글
    {
        SceneManager.LoadScene("JuHee");
    }

    public void WarpScene8() //엔딩
    {
        SceneManager.LoadScene("Ending");
    }

    public void WarpScene9() //크레딧(찐막)
    {
        SceneManager.LoadScene("EndingCredit");
    }
}
