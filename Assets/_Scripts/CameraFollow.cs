using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject obj;
    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = -10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y,offset); 
    }
}
