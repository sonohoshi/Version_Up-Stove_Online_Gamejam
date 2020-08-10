using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public FadeImage fadeImage;
    void Update()
    {
        if (FadeImage.isOuted)
            MoveScene.instance.WarpScene8(); //씬 0으로 복귀, 수정필요

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue, 3.5f);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);

            if (hit.transform != null)
            {
                if (hit.transform.CompareTag("search"))
                    fadeImage.PublicFadeOut();
            }
        }
    }
}
