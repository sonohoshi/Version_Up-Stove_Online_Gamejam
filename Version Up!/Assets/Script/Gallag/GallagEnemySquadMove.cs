using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallagEnemySquadMove : MonoBehaviour
{
    public static GallagEnemySquadMove instance;

    private Vector2 moveDir = Vector2.right;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        transform.Translate(moveDir * Time.deltaTime);
        if (transform.position.x >= 2f)
            moveDir = Vector2.left;
        else if (transform.position.x <= -2f)
            moveDir = Vector2.right;
    }
}
