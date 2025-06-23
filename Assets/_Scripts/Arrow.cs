using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject normalHit;
    public GameObject pierceThroughArmorHit;
    public GameObject arrowExplosion;
    public string source = "";
    public float phyDmg;
    public float speed;
    private Rigidbody2D rb;
    public string targetLayer;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 6f);
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = direction.normalized;
        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer(targetLayer))
        {
            Unit targetScript = collider.gameObject.GetComponent<Unit>();

            float dmg = phyDmg;
            if (source == "crossbow")
            {
                if (ActiveSpecialSkillChance(30))
                {
                    Vector3 pos = new Vector3(collider.transform.position.x, collider.transform.position.y, -0.1f);
                    Instantiate(pierceThroughArmorHit, pos, Quaternion.identity);
                    collider.GetComponent<Unit>()._LostHP(0, dmg, true, 0);
                }
                else
                {
                    Vector3 pos = new Vector3(collider.transform.position.x, collider.transform.position.y, -0.1f);
                    Instantiate(normalHit, pos, Quaternion.identity);
                    collider.GetComponent<Unit>()._LostHP(0, dmg, false, 0);
                }
                Destroy(gameObject);
                return;
            }

            //collider.GetComponent<Unit>()._LostHP(0,dmg,false);

            if (source == "archer")
            {
                GameObject explosion = Instantiate(arrowExplosion, transform.position, Quaternion.identity);
                ArrowExplosion exp = explosion.GetComponent<ArrowExplosion>();
                dmg = collider.GetComponent<Unit>().unitClass == "knight" ? phyDmg /  10 : phyDmg;
                exp.magicDmg = dmg;
                exp.enemyLayer = targetLayer;
                collider.GetComponent<Unit>()._LostHP(0, dmg, false, 0);
            }




            Destroy(gameObject);
        }


    }

    public bool ActiveSpecialSkillChance(float ratio)
    {
        float x = Random.Range(0, 101);
        if (ratio > x)
            return true;
        else
            return false;
    }


}
