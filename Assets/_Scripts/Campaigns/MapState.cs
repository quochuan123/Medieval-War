using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Map State", fileName = "MapStateSO")]
public class MapState : ScriptableObject
{
    public List<Node> allNodes;
    public List<ArmyData> allArmies;
}