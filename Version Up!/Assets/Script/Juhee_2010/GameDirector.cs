using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public Transform prefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void complete()
    {
        Instantiate(prefab, new Vector3(0, -0.4f, 0), Quaternion.identity);
    }

    
}
