using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowExplosion : MonoBehaviour
{
    public float magicDmg;
    public string enemyLayer;


    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(enemyLayer))
        {
            Unit enemy = collision.gameObject.GetComponent<Unit>();
            collision.GetComponent<Unit>()._LostHP(magicDmg, 0, false,3);
        }
    }
}
