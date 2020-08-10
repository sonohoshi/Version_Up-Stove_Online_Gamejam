using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyComputer : Icons
{
    private GameObject windowInMyCom;
    private GameObject openTextBTN;
    private GameObject textInMyCom;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // 코드가 좀 더럽지만, 내 컴퓨터에 쓰이는 캔버스를 찾아 그 자식인 오브젝트(이미지)를 가져오는 것.
        windowInMyCom = GameObject.Find("Canvas_MyComputer").transform.GetChild(0).gameObject;
        openTextBTN = windowInMyCom.transform.GetChild(0).gameObject;
        textInMyCom = openTextBTN.transform.GetChild(0).gameObject;
        SetOnClick();
    }

    private void SetOnClick()
    {
        openTextBTN.GetComponent<Button>().onClick.AddListener(OpenTextFile);
        parentOfButtons.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenComputer);
        textInMyCom.GetComponent<Button>().onClick.AddListener(CloseAll);
    }

    private void OpenComputer()
    {
        windowInMyCom.SetActive(true);
        parentOfButtons.gameObject.SetActive(false);
    }

    private void OpenTextFile()
    {
        textInMyCom.SetActive(true);
    }

    private void CloseAll()
    {
        textInMyCom.SetActive(false);
        windowInMyCom.SetActive(false);
        VerticalMovement.canMove = true;
    }
}
