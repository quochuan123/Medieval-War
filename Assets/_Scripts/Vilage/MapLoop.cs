using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoop : MonoBehaviour
{
    public GameObject gate1;
    public GameObject gate2;
    public GameObject point1;
    public GameObject point2;

    public GameObject player;

    

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x <= gate1.transform.position.x)
        {
            player.transform.position = point2.transform.position;
        }

        if (player.transform.position.x >= gate2.transform.position.x)
        {
            player.transform.position = point1.transform.position;
        }
    }
}
