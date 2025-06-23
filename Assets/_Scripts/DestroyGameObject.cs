using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    public float destroyTime;
    public void Start()
    {
        Destroy(gameObject,destroyTime);
    }

    public void _Destroy()
    {
        Destroy(gameObject);
    }
}
