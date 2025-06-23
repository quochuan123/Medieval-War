using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    public string friendlyMask;
    private List<Unit> friendlyList;
    private bool giveShield;

    private void Start()
    {
        friendlyList = new List<Unit>();
        Destroy(gameObject,10f);
    }

    private void OnTriggerEnter2D(Collider2D enter)
    {
        if (enter.gameObject.layer == LayerMask.NameToLayer(friendlyMask))
        {
            if (enter.gameObject.GetComponent<Unit>().enabled == true)
            {
                friendlyList.Add(enter.gameObject.GetComponent<Unit>());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D stay)
    {
        if (!giveShield)
        {
            giveShield = true;
            StartCoroutine(GiveShield());
        }
    }

    IEnumerator GiveShield()
    {

        friendlyList.RemoveAll(n => n.enabled == false);
        foreach (Unit friend in friendlyList)
        {
            friend.ShieldBuff();
        }
        yield return new WaitForSeconds(1f);
        giveShield = false;

    }

    private void OnTriggerExit2D(Collider2D exit)
    {

        if (friendlyList.Contains(exit.gameObject.GetComponent<Unit>()))
        {
            friendlyList.Remove(exit.gameObject.GetComponent<Unit>());
        }

    }
}
