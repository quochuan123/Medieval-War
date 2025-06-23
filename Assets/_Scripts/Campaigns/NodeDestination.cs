using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeDestination : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public Node node;
    public Animator animator;
    private SingletonScript sts;
    private CampaignManager manager;

    void Start()
    {
        animator = GetComponent<Animator>();
        sts = FindObjectOfType<SingletonScript>();
        manager = FindObjectOfType<CampaignManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!node.controlledByPlayer && !sts.mapState.allArmies[node.occupyingArmyID].isDead)
        {
            manager.startBattleButton.SetActive(true);
            sts.nextMap = node.nodeName;
            sts.nextNode = node;
            sts.prevNode = manager.prevNode;
            sts.enemyData = sts.mapState.allArmies[sts.nextNode.occupyingArmyID];
            sts.playerData = sts.mapState.allArmies[sts.prevNode.occupyingArmyID];
            sts._refArmyInfor = sts.armies
            .FirstOrDefault(a => a.name == sts.playerData.name);
            int connectPlayer = 0;
            int connectEnemy = 0;
            sts.enemyLvl = sts.enemyData.lvl;
            for (int i = 0; i < node.connectedNodeIDs.Count; i++)
            {
                if (sts.mapState.allNodes[node.connectedNodeIDs[i]].controlledByPlayer)
                {
                    connectPlayer++;
                }
                else
                {
                    connectEnemy++;
                }
            }
            sts.enemyEncircleBonus = connectEnemy;
            sts.playerEncircleBonus = connectPlayer;

            manager.MapInformationUpdate();
            manager.OpenEnemyStatus();
        }
        else
        {
            node.occupyingArmyID = manager.prevNode.occupyingArmyID;
            manager.prevNode.occupyingArmyID = -1;
            sts.mapState.allArmies[node.occupyingArmyID].currentNodeID = node.nodeID;
           
            var result = sts.controllersList
                .FirstOrDefault(a => a.node.nodeID == manager.prevNode.nodeID);
            if(result != null)
            {
                result.node = node;
            }
            manager.enemyStatus.SetActive(false);
            manager.Renderer();
            

            manager.controller.SetActive(true);
            manager.controller.GetComponent<ControllerManager>().OpenControlArmyWindow();

            for (int i = 0; i < manager.nodeObjects.Count; i++)
            {
                manager.nodeObjects[i].GetComponent<NodeView>().arrow.SetActive(false);
                manager.nodeObjects[i].GetComponent<NodeView>().aura.SetActive(false);
            }
            manager.startBattleButton.SetActive(false);
            manager.controller.GetComponent<CanvasGroup>().interactable = true;
            manager.back.SetActive(false);

            foreach(var army in manager.armyObjects)
            {
                army.gameObject.GetComponent<Animator>().Play("Army Idle");
            }
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.Play("Circle Anim");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.Play("Emit Arrow");

    }
}
