using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroll : MonoBehaviour
{
    public float scrollSpeed;
    public Transform[] mapSprite;

    private void Update()
    {
        for(int i=0; i<mapSprite.Length; i++)
        {
            mapSprite[i].Translate(Vector2.down * scrollSpeed * Time.deltaTime);
            if(mapSprite[i].transform.position.y <= -16)
            {
                int postIndex = i - 1;
                if (postIndex < 0)
                    postIndex = mapSprite.Length - 1;
                mapSprite[i].transform.position = new Vector2(mapSprite[postIndex].position.x, mapSprite[postIndex].position.y + 15);
            }
        }
    }
}
