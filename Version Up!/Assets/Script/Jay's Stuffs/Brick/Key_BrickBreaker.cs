using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_BrickBreaker : MonoBehaviour
{
    public GameObject hitEffectprefab;
    GameObject keyEffect;

    GameManager_BrickBreaker gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager_BrickBreaker>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            gm.HowmanyKeyhave++;
            keyEffect = Instantiate(hitEffectprefab, transform.position, transform.rotation);
            keyEffect.GetComponent<Animator>().SetBool("isTouched", true);
            Destroy(keyEffect, 0.3f);
            Destroy(gameObject);
        }
    }
}
