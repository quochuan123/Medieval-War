using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrainningUnit : MonoBehaviour
{
    public List<TrainingUnitButton> trainButtonList;
    private PrinceController player;
    private VillagerResourcesManager vm;
    private SingletonScript sts;
    public List<UnitTrainningCost> unitTrainningCosts;
    public UnitTrainningCost currentUnitTrainning;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PrinceController>();
        vm = FindObjectOfType<VillagerResourcesManager>();
        sts = FindObjectOfType<SingletonScript>();

        vm.currentGold.text = sts.gold.ToString();
        vm.currentVoluteers.text = sts.volunteers.ToString();

        currentUnitTrainning = unitTrainningCosts
            .Where(n => n.unitClass == "knight").First();
        currentUnitTrainning.GoldCostCalculate(sts.knightAmount);

        vm.tip.text = "Knight\r\n" +
                        "   A highly mobile unit type.\r\n" +
                        "   Movement speed increases over time while running (similar to acceleration).\r\n" +
                        "   Starts off slow, but gets faster the longer it keeps moving.\r\n" +
                        "Skill: Charge Attack\r\n" +
                        "   A skill that deals damage proportional to the current movement speed.\r\n" +
                        "   The faster the cavalry is running, the more powerful the attack becomes.\r\n" +
                        "   Ideal as a finisher when the unit has reached high speed.";


        vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
        vm.voluteersCost.text = currentUnitTrainning.volunteersCost.ToString();

        vm.knightAmount.text = sts.knightAmount.ToString();
        vm.knightAmount.color = sts.knightAmount >= sts.knightCap ? Color.red : Color.black;

        vm.archerAmount.text = sts.archerAmount.ToString();
        vm.archerAmount.color = sts.archerAmount >= sts.archerCap ? Color.red : Color.black;

        vm.archmageAmount.text = sts.archmageAmount.ToString();
        vm.archmageAmount.color = sts.archmageAmount >= sts.archmageCap ? Color.red : Color.black;

        vm.halbertAmount.text = sts.halbertAmount.ToString();
        vm.halbertAmount.color = sts.halbertAmount >= sts.halbertCap ? Color.red : Color.black;

        vm.mageAmount.text = sts.mageAmount.ToString();
        vm.mageAmount.color = sts.mageAmount >= sts.mageCap ? Color.red : Color.black;

        vm.crossbowAmount.text = sts.crossbowAmount.ToString();
        vm.crossbowAmount.color = sts.crossbowAmount >= sts.crossbowCap ? Color.red : Color.black;

        vm.shieldmanAmount.text = sts.shieldmanAmount.ToString();
        vm.shieldmanAmount.color = sts.shieldmanAmount >= sts.shieldmanCap ? Color.red : Color.black;


    }



    public void UnitTrainButton()
    {
        GameObject clickedObj = EventSystem.current.currentSelectedGameObject;
        if (clickedObj != null)
        {
            switch (clickedObj.tag)
            {
                case "knight":
                    currentUnitTrainning = unitTrainningCosts
                        .Where(n => n.unitClass == "knight").First();
                    currentUnitTrainning.GoldCostCalculate(sts.knightAmount);
                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    vm.voluteersCost.text = currentUnitTrainning.volunteersCost.ToString();
                    vm.trainningText.text = "Train Knight";
                    vm.tip.text = "Knight\r\n" +
                        "   A highly mobile unit type.\r\n" +
                        "   Movement speed increases over time while running (similar to acceleration).\r\n" +
                        "   Starts off slow, but gets faster the longer it keeps moving.\r\n" +
                        "Skill: Charge Attack\r\n" +
                        "   A skill that deals damage proportional to the current movement speed.\r\n" +
                        "   The faster the cavalry is running, the more powerful the attack becomes.\r\n" +
                        "   Ideal as a finisher when the unit has reached high speed.";
                    break;
                case "halbert":
                    currentUnitTrainning = unitTrainningCosts
                        .Where(n => n.unitClass == "halbert").First();
                    currentUnitTrainning.GoldCostCalculate(sts.halbertAmount);

                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    vm.voluteersCost.text = currentUnitTrainning.volunteersCost.ToString();
                    vm.trainningText.text = "Train Halbert";
                    vm.tip.text = "Halberdman:\r\n" +
"   A powerful infantry unit trained for frontline combat.\r\n" +
"   Strong and reliable against cavalry and direct assaults.\r\n" +

"Skill: Iron Resolve\r\n" +
"   Negates all damage from the next enemy attack once.\r\n" +
"   Especially effective against lethal cavalry strikes.\r\n";

                    break;
                case "crossbow":
                    currentUnitTrainning = unitTrainningCosts
                        .Where(n => n.unitClass == "crossbow").First();
                    currentUnitTrainning.GoldCostCalculate(sts.crossbowAmount);

                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    vm.voluteersCost.text = currentUnitTrainning.volunteersCost.ToString();
                    vm.trainningText.text = "Train Crossbow";
                    vm.tip.text = "Crossbowman:\r\n" +
"   A warrior specialized in using a crossbow.\r\n" +
"   Delivers precise and powerful ranged attacks.\r\n" +

"Skill: Deadly Bolt\r\n" +
"   Fires bolts that deal critical damage and pierce enemy armor.\r\n";

                    break;
                case "archer":
                    currentUnitTrainning = unitTrainningCosts
                        .Where(n => n.unitClass == "archer").First();
                    currentUnitTrainning.GoldCostCalculate(sts.archerAmount);

                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    vm.voluteersCost.text = currentUnitTrainning.volunteersCost.ToString();
                    vm.trainningText.text = "Train Archer";
                    vm.tip.text = "Archer:\r\n" +
"   A ranged unit specializing in wide-area attacks.\r\n" +
"   Effective at dealing consistent damage from a safe distance.\r\n" +

"Skill: Arrow Rain\r\n" +
"   Fires a volley of arrows over a target area.\r\n" +
"   Deals area damage to all enemies within the zone.\r\n";

                    break;
                case "archmage":
                    currentUnitTrainning = unitTrainningCosts
                        .Where(n => n.unitClass == "archmage").First();
                    currentUnitTrainning.GoldCostCalculate(sts.archmageAmount);

                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    vm.voluteersCost.text = currentUnitTrainning.volunteersCost.ToString();
                    vm.trainningText.text = "Train Archmage";
                    vm.tip.text = "Archmage:\r\n" +
"   A wise mage with mastery over many forms of magic.\r\n" +
"   Attacks using his wooden staff.\r\n" +

"Skill: Heal\r\n" +
"   Restores health to a single allied unit.\r\n" +

"Skill: Firestorm\r\n" +
"   Ignites a target area, dealing damage to all enemies within the explosion radius.\r\n";

                    break;
                case "shieldman":
                    currentUnitTrainning = unitTrainningCosts
                        .Where(n => n.unitClass == "shieldman").First();
                    currentUnitTrainning.GoldCostCalculate(sts.shieldmanAmount);

                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    vm.voluteersCost.text = currentUnitTrainning.volunteersCost.ToString();
                    vm.trainningText.text = "Train Shieldman";
                    vm.tip.text = "Shieldman:\r\n" +
"   A sturdy warrior clad in heavy armor.\r\n" +
"   Specializes in defense and frontline protection.\r\n" +

"Skill: Protector\r\n" +
"   Creates a protective zone for allies.\r\n" +
"   Greatly reduces incoming damage from enemies within the area.\r\n";

                    break;
                case "mage":
                    currentUnitTrainning = unitTrainningCosts
                        .Where(n => n.unitClass == "mage").First();
                    currentUnitTrainning.GoldCostCalculate(sts.mageAmount);
                    vm.tip.text = "Mage:\r\n" +
"   A wise spellcaster with deep knowledge of various types of magic.\r\n" +
"   Specializes in ranged attacks, support, and battlefield control.\r\n" +

"Skill: Empower\r\n" +
"   Increases the attack power of one allied unit.\r\n" +


    "Skill: Gravity Well\r\n" +
    "   Creates a magical zone at a target location.\r\n" +
"   Enemies inside the zone have their movement speed reduced for a few seconds.\r\n";


                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    vm.voluteersCost.text = currentUnitTrainning.volunteersCost.ToString();
                    vm.trainningText.text = "Train Mage";

                    break;
                default:
                    break;
            }
        }
    }

    public void TrainningButton()
    {
        if (sts.gold < currentUnitTrainning.goldCost)
        {
            vm.tip.text = "Not enough gold!";
        }
        else if (sts.volunteers < currentUnitTrainning.volunteersCost)
        {
            vm.tip.text = "Not enough mens!";
        }
        else
        {
            switch (currentUnitTrainning.unitClass)
            {
                case "knight":
                    if (sts.knightAmount >= sts.knightCap)
                    {
                        vm.tip.text = "Your Knights amount reach cap!";
                        break;
                    }
                    sts.gold -= currentUnitTrainning.goldCost;
                    sts.volunteers -= currentUnitTrainning.volunteersCost;
                    sts.knightAmount++;
                    vm.knightAmount.text = sts.knightAmount.ToString();

                    vm.knightAmount.color = sts.knightAmount >= sts.knightCap ? Color.red : Color.black;

                    currentUnitTrainning.GoldCostCalculate(sts.knightAmount);
                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();

                    break;
                case "archer":
                    if (sts.archerAmount >= sts.archerCap)
                    {
                        vm.tip.text = "Your Archers amount reach cap!";
                        break;
                    }
                    sts.gold -= currentUnitTrainning.goldCost;
                    sts.volunteers -= currentUnitTrainning.volunteersCost;
                    sts.archerAmount++;
                    vm.archerAmount.text = sts.archerAmount.ToString();
                    vm.archerAmount.color = sts.archerAmount >= sts.archerCap ? Color.red : Color.black;

                    currentUnitTrainning.GoldCostCalculate(sts.archerAmount);
                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();

                    break;
                case "shieldman":
                    if (sts.shieldmanAmount >= sts.shieldmanCap)
                    {
                        vm.tip.text = "Your Shieldmans amount reach cap!";
                        break;
                    }
                    sts.gold -= currentUnitTrainning.goldCost;
                    sts.volunteers -= currentUnitTrainning.volunteersCost;
                    sts.shieldmanAmount++;
                    vm.shieldmanAmount.text = sts.shieldmanAmount.ToString();
                    vm.shieldmanAmount.color = sts.shieldmanAmount >= sts.shieldmanCap ? Color.red : Color.black;

                    currentUnitTrainning.GoldCostCalculate(sts.shieldmanAmount);
                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    break;
                case "archmage":
                    if (sts.archmageAmount >= sts.archmageCap)
                    {
                        vm.tip.text = "Your Archmages amount reach cap!";
                        break;
                    }
                    sts.gold -= currentUnitTrainning.goldCost;
                    sts.volunteers -= currentUnitTrainning.volunteersCost;
                    sts.archmageAmount++;
                    vm.archmageAmount.text = sts.archmageAmount.ToString();
                    vm.archmageAmount.color = sts.archmageAmount >= sts.archmageCap ? Color.red : Color.black;

                    currentUnitTrainning.GoldCostCalculate(sts.archmageAmount);
                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    break;
                case "mage":
                    if (sts.mageAmount >= sts.mageCap)
                    {
                        vm.tip.text = "Your Mages amount reach cap!";
                        break;
                    }
                    sts.gold -= currentUnitTrainning.goldCost;
                    sts.volunteers -= currentUnitTrainning.volunteersCost;
                    sts.mageAmount++;
                    vm.mageAmount.text = sts.mageAmount.ToString();
                    vm.mageAmount.color = sts.mageAmount >= sts.mageCap ? Color.red : Color.black;

                    currentUnitTrainning.GoldCostCalculate(sts.mageAmount);
                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    break;
                case "halbert":
                    if (sts.halbertAmount >= sts.halbertCap)
                    {
                        vm.tip.text = "Your Halbertmans amount reach cap!";
                        break;
                    }
                    sts.gold -= currentUnitTrainning.goldCost;
                    sts.volunteers -= currentUnitTrainning.volunteersCost;
                    sts.halbertAmount++;
                    vm.halbertAmount.text = sts.halbertAmount.ToString();
                    vm.halbertAmount.color = sts.halbertAmount >= sts.halbertCap ? Color.red : Color.black;

                    currentUnitTrainning.GoldCostCalculate(sts.halbertAmount);
                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    break;
                case "crossbow":
                    if (sts.crossbowAmount >= sts.crossbowCap)
                    {
                        vm.tip.text = "Your Crossbowmans amount reach cap!";
                        break;
                    }
                    sts.gold -= currentUnitTrainning.goldCost;
                    sts.volunteers -= currentUnitTrainning.volunteersCost;
                    sts.crossbowAmount++;
                    vm.crossbowAmount.text = sts.crossbowAmount.ToString();
                    vm.crossbowAmount.color = sts.crossbowAmount >= sts.crossbowCap ? Color.red : Color.black;

                    currentUnitTrainning.GoldCostCalculate(sts.crossbowAmount);
                    vm.goldCost.text = currentUnitTrainning.goldCost.ToString();
                    break;
                default:
                    break;

            }

            vm.currentGold.text = sts.gold.ToString();
            vm.currentVoluteers.text = sts.volunteers.ToString();
            vm.gold.text = sts.gold.ToString();
            vm.voluteers.text = sts.volunteers.ToString();

        }
    }

    public void ExitButton()
    {
        player.isTalk = false;
        vm.disableHud.SetActive(false);
        vm.trainningInterface.SetActive(false);

        SoundManager sm = FindObjectOfType<SoundManager>();
        if(sm != null)
        {
            sm.InterfaceMusicOff();
        }
    }
}

[System.Serializable]
public class UnitTrainningCost
{
    public string unitClass;
    public int baseGoldCost;
    public int goldCost;
    public int volunteersCost;

    public void GoldCostCalculate(int amount)
    {
        goldCost = Mathf.RoundToInt(baseGoldCost * (1 + amount * 0.05f));
    }
}
