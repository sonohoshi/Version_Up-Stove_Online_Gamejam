using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Network : Icons
{
    private GameObject inputField;
    private GameObject enterButton;
    private GameObject cancelButton;
    private GameObject backGround;
    private GameObject errorMessage;
    private AudioSource audioSource;

    private const string rightIP = "20.200.7.24";

    public FadeImage fade;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        backGround = GameObject.Find("Canvas_Network").transform.GetChild(0).gameObject;
        enterButton = backGround.transform.GetChild(0).gameObject;
        cancelButton = backGround.transform.GetChild(1).gameObject;
        inputField = backGround.transform.GetChild(2).gameObject;
        errorMessage = GameObject.Find("Canvas_Network").transform.GetChild(1).gameObject;
        
        audioSource = enterButton.GetComponent<AudioSource>();
        
        SetOnClick();
    }

    void Update()
    {
        if (FadeImage.isOuted)
        {
            MoveScene.instance.WarpScene6();
        }
    }

    private void SetOnClick()
    {
        parentOfButtons.GetChild(0).GetComponent<Button>().onClick.AddListener(OpenNetwork);
        parentOfButtons.GetChild(1).GetComponent<Button>().onClick.AddListener(PlayAlone);
        enterButton.GetComponent<Button>().onClick.AddListener(EnterIP);
        cancelButton.GetComponent<Button>().onClick.AddListener(Cancel);
        
        errorMessage.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(SayYes);
        errorMessage.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(SayYes);
    }

    private void OpenNetwork()
    {
        backGround.SetActive(true);
        parentOfButtons.gameObject.SetActive(false);
    }

    private void EnterIP()
    {
        string input = inputField.GetComponent<InputField>().text;
        if (input.Equals(rightIP))
        {
            audioSource.Play();
            fade.PublicFadeOut();
        }
    }

    private void PlayAlone()
    {
        errorMessage.SetActive(true);
        parentOfButtons.gameObject.SetActive(false);
    }

    private void SayYes()
    {
        errorMessage.SetActive(false);
        VerticalMovement.canMove = true;
    }

    private void Cancel()
    {
        backGround.SetActive(false);
        VerticalMovement.canMove = true;
    }
}
