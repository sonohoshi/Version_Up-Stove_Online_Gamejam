using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Internet : Icons
{
    private GameObject html;
    private GameObject properties;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        html = GameObject.Find("Canvas_Internet").transform.GetChild(0).gameObject;
        properties = GameObject.Find("Canvas_Internet").transform.GetChild(1).gameObject;
        
        SetOnClick();
    }

    private void SetOnClick()
    {
        html.GetComponent<Button>().onClick.AddListener(CloseInternet);
        properties.GetComponent<Button>().onClick.AddListener(CloseProperty);
        parentOfButtons.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenInternet);
        parentOfButtons.GetChild(1).GetComponent<Button>().onClick.AddListener(OpenProperty);
    }

    private void OpenInternet()
    {
        html.SetActive(true);
        parentOfButtons.gameObject.SetActive(false);
    }

    private void OpenProperty()
    {
        properties.SetActive(true);
        parentOfButtons.gameObject.SetActive(false);
    }

    private void CloseInternet()
    {
        html.SetActive(false);
        VerticalMovement.canMove = true;
    }

    private void CloseProperty()
    {
        properties.SetActive(false);
        VerticalMovement.canMove = true;
    }
}
