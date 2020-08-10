using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public static GhostManager instance;

    private List<GameObject> ghostList = new List<GameObject>();
    private List<Ghost> ghostScriptList = new List<Ghost>();

    private void Awake()
    {
        if (instance == null)
            instance = this;

        for(int i=0; i<transform.childCount; i++)
        {
            GameObject ghost = transform.GetChild(i).gameObject;
            ghostList.Add(ghost);
            ghostScriptList.Add(ghost.GetComponent<Ghost>());
        }
    }


    // 모든 Ghost들의 위치를 초기 위치로 초기화한다.
    public void ResetAllGhosts()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            ghostList[i].transform.position = ghostScriptList[i].startPos;
            ghostScriptList[i].targetPos = ghostList[i].transform.position;
            ghostScriptList[i].SetState(Ghost.State.NONE);
        }
    }

    public void SetConfused()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            if (ghostScriptList[i].curState == Ghost.State.NONE)
                ghostScriptList[i].SetState(Ghost.State.CONFUSED);
        }
    }

    public void RevertConfused()
    {
        for (int i = 0; i < ghostList.Count; i++)
        {
            if (ghostScriptList[i].curState == Ghost.State.CONFUSED)
                ghostScriptList[i].SetState(Ghost.State.NONE);
        }
    }
}
