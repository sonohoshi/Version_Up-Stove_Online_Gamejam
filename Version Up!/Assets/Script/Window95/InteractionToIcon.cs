using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionToIcon : MonoBehaviour
{
    public BlueScreen blueScreen;
    public GameObject error;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = false;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Icon"))
        {
            Debug.Log(col.gameObject.name);
            if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space)) && VerticalMovement.canMove)
            {
                audioSource.Play();
                col.GetComponent<Icons>().ShowMethods(transform.position);
                blueScreen.DoInteract();
                VerticalMovement.canMove = false;
            }
        }

        if (col.CompareTag("StartBTN"))
        {
            if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Space))
            {
                col.GetComponent<StartButton>().ShowMessage();
            }
        }

        if (col.CompareTag("EnemyBullet"))
        {
            VerticalMovement.canMove = false;
            error.SetActive(true);
        }
    }
}
