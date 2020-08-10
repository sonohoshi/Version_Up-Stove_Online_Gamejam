using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMove : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 dir;

    GameObject keyEffect;
    public GameObject keyEffectprefab;
    bool isAniPlaying = false;

    void Start()
    {

        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-3f, 3f);
        dir = new Vector2(x, y).normalized * 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Kirby")
        {
            keyEffect = Instantiate(keyEffectprefab, transform.position, transform.rotation);
            Destroy(keyEffect, 0.4f);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Map_Left") || collision.CompareTag("Map_Right"))
            dir = new Vector2(-dir.x, dir.y);
        else if (collision.CompareTag("Map_Top") || collision.CompareTag("Map_Bottom"))
            dir = new Vector2(dir.x, -dir.y);
    }

    private void Update()
    {
        if (isAniPlaying) return;

        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }
}
