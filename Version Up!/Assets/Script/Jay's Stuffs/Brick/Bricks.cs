using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public GameObject hitEffectPrefab;
    GameObject spawnedEffect;
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            spawnedEffect = Instantiate(hitEffectPrefab, coll.transform.position, coll.transform.rotation);
            Destroy(spawnedEffect, 0.3f);
            Destroy(gameObject);
        }
    }
}
