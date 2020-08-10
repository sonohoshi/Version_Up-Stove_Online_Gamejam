using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    GameObject keyEffect;
    public GameObject keyEffectprefab;
    GameManager gm;
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            gm.isGetkey = true;
            keyEffect = Instantiate(keyEffectprefab, transform.position, transform.rotation);
            Destroy(keyEffect, 0.3f);
            Destroy(gameObject);
        }
    }
}
