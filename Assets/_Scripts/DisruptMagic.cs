using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DisruptMagic : MonoBehaviour
{
    public string targetLayer;
    public List<Unit> enemyList;
    private bool isDisrupt;
    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<Unit>();
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D enter)
    {
        if(enter.gameObject.layer == LayerMask.NameToLayer(targetLayer))
        {
            if(enter.gameObject.GetComponent<Unit>().enabled == true)
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
        if (!isDisrupt)
        {
            isDisrupt = true;
            StartCoroutine(GiveDisruptStatus());
        }
    }

    IEnumerator GiveDisruptStatus()
    {
        enemyList.RemoveAll(n => n.enabled == false);
        foreach (Unit enemy in enemyList)
        {
            enemy.Disrupt();
        }
        yield return new WaitForSeconds(1f);
        isDisrupt = false;
    }
}
