using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    GameManager gm;
    Animator animator;
    public AudioSource levelCompleted;


    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && gm.isGetkey)
        {
            Debug.Log("클리어!");
            animator.SetBool("isCanOpen", true);
            levelCompleted.Play();
            gm.clearStage();
        }
    }
}
