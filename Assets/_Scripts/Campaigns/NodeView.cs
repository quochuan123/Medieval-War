using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    public GameObject armyChoose;
    public Node node;
    public GameObject arrow;

    public GameObject aura;
    public NodeDestination destination;
    private void Start()
    {
        armyChoose = GameObject.Find("ArmyChoice");
        destination = aura.GetComponent<NodeDestination>();
        destination.node = node;
    }

} 
