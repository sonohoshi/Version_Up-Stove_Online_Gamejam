using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject letterMSG;

    private Image message;
    private bool isShowed;
    private bool isClicked;

    void Start()
    {
        isShowed = false;
        isClicked = false;
        message = (Image) letterMSG.GetComponentInChildren(typeof(Image));
    }
    public void ShowMessage()
    {
        if (!isClicked)
        {
            StartCoroutine(ShowSlowly());
            isClicked = true;
        }
    }

    void Update()
    {
        if (isShowed && Input.anyKey)
        {
            SceneManager.LoadScene("KimSM");
        }
    }

    private IEnumerator ShowSlowly()
    {
        Color color = message.color;
        letterMSG.SetActive(true);
        while (color.a < 1f)
        {
            color.a += 0.25f;
            message.color = color;
            Debug.Log(color.a);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        isShowed = true;
    }
}
