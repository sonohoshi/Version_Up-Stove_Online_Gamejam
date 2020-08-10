using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    public Transform prefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void complete()
    {
        Instantiate(prefab, new Vector3(-1.5f, -4.23f, 0), Quaternion.identity);
    }


}
