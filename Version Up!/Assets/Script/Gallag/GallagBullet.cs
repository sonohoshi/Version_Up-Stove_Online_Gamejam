using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallagBullet : MonoBehaviour
{
    public float moveSpeed;

    private void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 맵 밖으로 나간 경우.
        if (collision.CompareTag("Map_Top"))
            GallagPlayer.instance.ReturnBullet(gameObject);

        // 적이 맞은 경우.
        if (collision.CompareTag("enemy"))
        {
            // 적에게 데미지.
            GallagPlayer.instance.ReturnBullet(gameObject);
        }
    }
}
