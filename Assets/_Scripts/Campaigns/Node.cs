using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Node
{
    public string nodeName;
    public int nodeID;
    public int ObjectID;
    public Vector3 worldPosition;     // Vị trí trên bản đồ
    public bool controlledByPlayer;         // ID người chiếm giữ
    public int occupyingArmyID;       // ID quân đội chiếm giữ

    public List<int> connectedNodeIDs = new List<int>();
}
