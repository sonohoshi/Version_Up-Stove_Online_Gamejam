using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGate : MonoBehaviour
{
    public AudioSource reject;
    public AudioSource enter;
    public FadeImage fadeImage;
    public GameObject box;
    public TypeWriterEffect twe;
    int count;

    void Start()
    {
        fadeImage = GameObject.Find("FadeSpawner").GetComponent<FadeImage>();
        box = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        twe = box.GetComponent<TypeWriterEffect>();
    }

    void Update()
    {
        if (FadeImage.isOuted)
            MoveScene.instance.WarpScene1();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player")
        {
            if (twe.readyToNextstage)
            {
                enter.Play();
                StartCoroutine(MoveTo(coll.gameObject, twe.gateSpawnpoint.transform.position));
                fadeImage.PublicFadeOut();
                return;
            }


            if (twe.isTried3times == false)
            {
                reject.Play();
                coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2500.0f, 1000f));
                count++;
            }
            if (count >= 2)
                twe.isTried3times = true;
        }
    }
    IEnumerator MoveTo(GameObject a, Vector3 toPos)
    {
        float count = 0;
        Vector3 wasPos = a.transform.position;
        while (true)
        {
            count += Time.deltaTime;
            a.transform.position = Vector3.Lerp(wasPos, toPos, count);

            if (count >= 3)
            {
                a.transform.position = toPos;
                break;
            }
            yield return null;
        }
    }
}
