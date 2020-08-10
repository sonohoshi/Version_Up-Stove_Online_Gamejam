using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class FadeImage
{
    /*
    코루틴의 특성 상 다 끝나지 않더라도 다음 코드가 실행될 수 있다는 점 때문에
    isOuted라는 bool 변수를 추가했습니다.
    스테이지를 넘어가도록 조작하는 스크립트에서 
    FadeImage fadeImage; 같은 식으로 변수를 추가해주시고
    void Update() 안에 이 코드를 추가해주세요.
    
    if (fadeImage.isOuted)
    {
        SceneManager.LoadScene("씬 이름");
    }
    이러면 페이드아웃이 끝난걸 확인하고 다음 스테이지를 로딩할거에요!
    */
    public static bool isOuted;
    private static Image img;

    void Awake()
    {
        isOuted = false;
        img = GetComponent<Image>();
    }

    // 씬에 넣어두면 자동으로 시작되고 페이드인이 되고 비활성화 될 것.
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // 다음 씬을 로딩하기 전에 이 메소드를 불러주고난 뒤... 설명은 위쪽에 더 있음.
    public void PublicFadeOut()
    {
        StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeIn()
    {
        Color fadeColor = img.color;
        yield return new WaitForSeconds(0.5f);

        Debug.Log("anjwl");
        
        while (fadeColor.a > 0f)
        {
            fadeColor.a -= 0.25f;
            img.color = fadeColor;
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.SetActive(false);
    }

    private IEnumerator FadeOut()
    {
        Color fadeColor = img.color;
        yield return new WaitForSeconds(0.5f);
        while (fadeColor.a < 1f)
        {
            fadeColor.a += 0.25f;
            img.color = fadeColor;
            yield return new WaitForSeconds(0.5f);
        }

        isOuted = true;
    }
}
