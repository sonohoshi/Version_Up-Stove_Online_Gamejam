using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallagEnemyController : MonoBehaviour
{
    public static GallagEnemyController instance;

    public List<GameObject> enemyList = new List<GameObject>();

    private float attackCoolTime = 3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    private void Update()
    {
        attackCoolTime -= Time.fixedDeltaTime;

        // 공격
        if(attackCoolTime <= 0)
        {
            List<GallagEnemy> enemyScriptList = new List<GallagEnemy>();
            for (int i = 0; i < enemyList.Count; i++)
            {
                GallagEnemy enemyScript = enemyList[i].GetComponent<GallagEnemy>();
                if (enemyScript.curState == GallagEnemy.state.NONE)
                    enemyScriptList.Add(enemyScript);
            }

            if(enemyScriptList.Count > 0)
            {
                int random = Random.Range(0, enemyScriptList.Count);
                enemyScriptList[random].curState = GallagEnemy.state.ATTACK;
                random = Random.Range(0, enemyScriptList.Count);
                enemyScriptList[random].curState = GallagEnemy.state.ATTACK;
                random = Random.Range(0, enemyScriptList.Count);
                enemyScriptList[random].curState = GallagEnemy.state.ATTACK;
                random = Random.Range(0, enemyScriptList.Count);
                enemyScriptList[random].curState = GallagEnemy.state.ATTACK;

                attackCoolTime = 3;
            }
        }
    }
}
