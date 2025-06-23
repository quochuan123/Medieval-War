using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerCameraController : MonoBehaviour
{
    [SerializeField] float offset = -10f;
    public GameObject player;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y, offset);
        transform.position = pos;
    }
}
