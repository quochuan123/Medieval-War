using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
{
    private Tilemap thisTilemap;
    // Start is called before the first frame update
    void Start()
    {
        thisTilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(1,0.6f,0) * Time.deltaTime;
        if(transform.position.x <= -20)
        {
            transform.position = new Vector3(0,0,0);
        }
    }
}
