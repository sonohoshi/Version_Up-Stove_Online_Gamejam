using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueScreen : MonoBehaviour
{
    public GameObject blueScreen;
    
    private int interactionCount = 0;
    private bool onBlueScreen = false;

    public void DoInteract()
    {
        interactionCount++;
        CheckCount();
    }

    private void CheckCount()
    {
        if (interactionCount > 15)
        {
            blueScreen.SetActive(true);
            onBlueScreen = true;
        }
    }

    void Update()
    {
        if (onBlueScreen)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("KimSM");
            }
        }
    }
}
