using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallagEnemyBullet : MonoBehaviour
{
    public float moveSpeed;

    private void Update()
    {
        transform.Translate(-1 * transform.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 맵 밖으로 나간 경우.
        if (collision.CompareTag("Map_Bottom"))
            GallagPlayer.instance.ReturnBullet(gameObject);

        // 플레이어가 맞은 경우.
        else if (collision.CompareTag("Player"))
        {
            GallagEnemy.ReturnBullet(gameObject);
        }
    }
}
