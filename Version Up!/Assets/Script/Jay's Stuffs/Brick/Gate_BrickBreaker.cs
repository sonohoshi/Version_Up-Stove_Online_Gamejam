using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_BrickBreaker : MonoBehaviour
{
    GameManager_BrickBreaker gm;
    Animator animator;
    public AudioSource levelCompleted;
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager_BrickBreaker>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && gm.HowmanyKeyhave >= 2)
        {
            Debug.Log("클리어!");
            animator.SetBool("isCanOpen", true);
            levelCompleted.Play();
            gm.clearStage();
        }
    }
}
