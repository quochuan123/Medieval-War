using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FireSpark : MonoBehaviour
{
    public float phyDmg;
    public float magicDmg;
    public string targetLayer;
    public float dps;
    public List<Unit> enemyList;
    public bool isBurn;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<Unit>();
        Destroy(gameObject, 5f);
    }

    

    private void OnTriggerEnter2D(Collider2D enter)
    {
        if (enter.gameObject.layer == LayerMask.NameToLayer(targetLayer))
        {
            if (enter.gameObject.GetComponent<Unit>().enabled == true)
            {
                enemyList.Add(enter.gameObject.GetComponent<Unit>());
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D exit)
    {
        if (enemyList.Contains(exit.gameObject.GetComponent<Unit>()))
        {
            enemyList.Remove(exit.gameObject.GetComponent<Unit>());
        }
    }

    private void OnTriggerStay2D(Collider2D stay)
    {
        if (!isBurn)
        {
            isBurn = true;
            enemyList.RemoveAll(n => n.enabled == false);
            foreach (var unit in enemyList)
            {
                float advantage = unit.unitClass == "shieldman" ? 2 : 1;
                dps = (magicDmg / 3) * (1 - (unit.resistance / (unit.resistance + 10)));
                unit.Burned(dps * advantage, 3f);
            }
            StartCoroutine(DmgInterval());
        }
    }

    IEnumerator DmgInterval()
    {
        yield return new WaitForSeconds(1f);
        isBurn = false;
    }


}
