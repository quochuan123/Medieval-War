using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Magic : MonoBehaviour
{
    public GameObject frostBullet;
    public float magicDmg;
    public float speed;
    private Rigidbody2D rb;
    public string targetLayer;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 6f);
        speed = 2f;
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = direction.normalized;
        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer(targetLayer))
        {
            Unit targetScript = collider.GetComponent<Unit>();
            Vector3 pos = new Vector3(collider.transform.position.x, collider.transform.position.y, -0.1f);
            Instantiate(frostBullet, pos, Quaternion.identity);
            targetScript._LostHP(magicDmg, 0,false,0);
            Destroy(gameObject);
        }
    }


}
