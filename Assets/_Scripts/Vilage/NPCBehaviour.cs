using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject conversation;
    private SpriteRenderer spriteRenderer;
    private VillagerManager vm;
    void Start()
    {
        vm = FindObjectOfType<VillagerManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = player.transform.position.x > transform.position.x ? false : true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            switch (gameObject.tag)
            {
                case "miner":
                    if (vm.isTakenGold)
                    {
                        conversation.SetActive(true);
                        vm.takeGold = true;
                    }
                    break;
                case "nun":
                    if (vm.isTakenVoluteers)
                    {
                        conversation.SetActive(true);
                        vm.takeVoluteers = true;
                    }
                    break;
                case "merchant":
                    vm.CanOpenTrainingUnit = true;
                    conversation.SetActive(true);

                    break;
                case "elf":
                    conversation.SetActive(true);
                    vm.isTalkWithElf = true;
                    break;
                case "gravekeeper":
                    conversation.SetActive(true);
                    vm.isTalkWithGraveKeeper = true;
                    break;
                case "soldier":
                    conversation.SetActive(true);
                    vm.isTalkWithSoldier = true;

                    break;
                case "hunter":
                    conversation.SetActive(true);
                    vm.isTalkWithHunter = true;

                    break;
                default:
                    break;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            switch (gameObject.tag)
            {
                case "miner":

                    conversation.SetActive(false);
                    vm.takeGold = false;
                    break;
                case "nun":
                    conversation.SetActive(false);
                    vm.takeVoluteers = false;
                    break;
                case "merchant":
                    vm.CanOpenTrainingUnit = false;
                    conversation.SetActive(false);

                    break;
                case "elf":
                    conversation.SetActive(false);
                    vm.isTalkWithElf = false;

                    break;
                case "gravekeeper":
                    conversation.SetActive(false);
                    vm.isTalkWithGraveKeeper = false;
                    break;
                case "soldier":
                    conversation.SetActive(false);
                    vm.isTalkWithSoldier = false;
                    break;
                case "hunter":
                    conversation.SetActive(false);
                    vm.isTalkWithHunter = false;
                    break;
                default:
                    break;
            }
        }
    }
}
