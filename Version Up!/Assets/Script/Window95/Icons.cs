using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//각 아이콘들에 들어갈 클래스의 조상 클래스가 될 것입니다.
public class Icons : MonoBehaviour
{
    public string[] nameOfMethods;
    public Button methodBox;
    public Canvas canvas;

    protected Transform parentOfButtons;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        parentOfButtons = canvas.transform.GetChild(1);
        MakeMethods(transform.position);
    }

    protected void MakeMethods(Vector2 playerPos)
    {
        Vector2 instantiatePosition = playerPos;
        foreach(var method in nameOfMethods)
        {
            var thisBox = Instantiate(methodBox, instantiatePosition, Quaternion.identity);
            instantiatePosition.y -= 1.5f;
            thisBox.transform.SetParent(parentOfButtons);
            thisBox.transform.localScale = new Vector3(0.01f,0.01f,1);
            thisBox.GetComponentInChildren<Text>().text = method;
        }
        parentOfButtons.gameObject.SetActive(false);
    }

    public void ShowMethods(Vector2 playerPos)
    {
        parentOfButtons.position = playerPos;
        parentOfButtons.gameObject.SetActive(true);
    }
}
