using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecycleBin : Icons
{
    public GameObject garbage;
    public Sprite emptyBin;
    
    private List<GameObject> garbages;
    private bool isEmpty;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        garbages = new List<GameObject>();
        isEmpty = false;
        for (int i = 0; i < 25; i++)
        {
            garbages.Add(Instantiate(garbage,transform.position,Quaternion.identity));
        }
        parentOfButtons.GetChild(0).GetComponent<Button>().onClick.AddListener(Empty);
    }

    private IEnumerator PopGarbage()
    {
        foreach (var trash in garbages)
        {
            trash.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }

        isEmpty = true;
        GetComponent<SpriteRenderer>().sprite = emptyBin;
    }

    private void Empty()
    {
        if (!isEmpty)
        {
            parentOfButtons.gameObject.SetActive(false);
            VerticalMovement.canMove = true;
            StartCoroutine((PopGarbage()));
        }
        else
        {
            parentOfButtons.gameObject.SetActive(false);
            VerticalMovement.canMove = true;
        }
    }
}
