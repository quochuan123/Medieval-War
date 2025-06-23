using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomeEnemyLineup : MonoBehaviour
{
    public string knight = "knight";
    public string archer = "archer";
    public string halbert = "halbert";
    public string shieldman = "shieldman";
    public string mage = "mage";
    public string archmage = "archmage";
    public string crossbow = "crossbow";
    //Lineup 5
    public List<string> mage2_Archmage_3 = new List<string>()
    {
        "mage","mage","archmage","archmage","archmage",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> crossbow_5 = new List<string>()
    {
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> archer_5 = new List<string>()
    {
        "archer","archer","archer","archer","archer",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> knight_5 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    //Lineup 6
    public List<string> shieldman_6 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> knight_6 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    //Lineup 8
    public List<string> knight_8 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_8 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> archmage_8 = new List<string>()
    {
        "archmage","archmage","archmage","archmage","archmage",
        "archmage","archmage","archmage","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_5_Crossbow_3 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "crossbow","crossbow","crossbow","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_5_Crossbow_2_Archmage_1 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "crossbow","crossbow","crossbow","none","none",
        "none","none","none","none","none",
        "archmage","none","none","none","none",
        "none","none","none","none","none"
    };
    //Lineup 10
    public List<string> halbert_10 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> crossbow_10 = new List<string>()
    {
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> shieldman_10 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> knight_10 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_5_Knight_5 = new List<string>()
    {

        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "knight","knight","knight","knight","knight"
    };
    public List<string> crossbow_5_Knight_5 = new List<string>()
    {

        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "knight","knight","knight","knight","knight"
    };

    public List<string> mage_10 = new List<string>()
    {
        "mage","mage","mage","mage","mage",
        "mage","mage","mage","mage","mage",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> archmage_10 = new List<string>()
    {
        "archmage","archmage","archmage","archmage","archmage",
        "archmage","archmage","archmage","archmage","archmage",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> archmage_5_Mage_5 = new List<string>()
    {
        "archmage","archmage","archmage","archmage","archmage",
        "mage","mage","mage","mage","mage",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> archer_10 = new List<string>()
    {
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    //Lineup 11
    public List<string> knight_11 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_10_Mage_1 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "mage","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_8_Mage_3 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","none","none",
        "mage","mage","mage","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_8_Archmage_3 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","none","none",
        "archmage","archmage","archmage","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> halbert_6_Crossbow_5 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","none","none","none","none",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> halbert_8_mage_2_Archmage_1 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","none","none",
        "mage","mage","archmage","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    //Lineup 12
    public List<string> halbert_4_mage_4_Archmage_4 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","none",
        "none","none","none","none","none",
        "mage","mage","mage","mage","none",
        "none","none","none","none","none",
        "archmage","archmage","archmage","archmage","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_6_mage_3_Archmage_3 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","none","none","none","none",
        "mage","mage","mage","none","none",
        "none","none","none","none","none",
        "archmage","archmage","archmage","none","none",
        "none","none","none","none","none"
    };
    public List<string> knight_6_mage_3_Archmage_3 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","none","none","none","none",
        "mage","mage","mage","none","none",
        "none","none","none","none","none",
        "archmage","archmage","archmage","none","none",
        "none","none","none","none","none"
    };
    public List<string> crossbow_6_mage_3_Archmage_3 = new List<string>()
    {
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","none","none","none","none",
        "mage","mage","mage","none","none",
        "none","none","none","none","none",
        "archmage","archmage","archmage","none","none",
        "none","none","none","none","none"
    };

    public List<string> crossbow_12 = new List<string>()
    {
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> archmage_12 = new List<string>()
    {
        "archmage","archmage","archmage", "archmage","archmage",
        "archmage","archmage","archmage", "archmage","archmage",
         "archmage","archmage","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> mage_12 = new List<string>()
    {
         "mage","mage","mage","mage","mage",
         "mage","mage","mage","mage","mage",
          "mage","mage","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };


    //Lineup 13
    public List<string> knight_13 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> mage_4_Archmage_4_Knight_5 = new List<string>()
    {
        
        "mage","mage","mage","mage","none",
        "none","none","none","none","none",
        "archmage","archmage","archmage","archmage","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "knight","knight","knight","knight","knight"
    };

    public List<string> shieldman_4_Archmage_3_Archer_6 = new List<string>()
    {

        "shieldman","shieldman","shieldman","shieldman","none",
        "none","none","none","none","none",
        "archmage","archmage","archmage","archmage","none",
        "none","none","none","none","none",
        "archer","archer","archer","archer","archer",
        "archer","none","none","none","none"
    };

    public List<string> shieldman_4_Archmage_3_Mage_3 = new List<string>()
    {

        "shieldman","shieldman","shieldman","shieldman","none",
        "none","none","none","none","none",
        "archmage","archmage","archmage","archmage","none",
        "none","none","none","none","none",
        "mage","mage","mage","mage","none",
        "none","none","none","none","none"
    };

    public List<string> shieldman_4_Crossbow_6 = new List<string>()
    {

        "shieldman","shieldman","shieldman","shieldman","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","none","none","none","none"
    };


    //Line up 14
    public List<string> knight_14 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> knight_7_Crossbow_6 = new List<string>()
    {
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "knight","knight","knight","knight","knight",
        "knight","knight","none","none","none"
    };

    public List<string> halbert_7_Mage_3 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "mage","mage","mage","none","none",
        "none","none","none","none","none"
    };

    public List<string> archer_13 = new List<string>()
    {
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    //Line 15
    public List<string> halbert_10_Crossbow_5 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> crossbow_15 = new List<string>()
    {
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_15 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_10_archer_5 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "archer","archer","archer","archer","archer",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> archer_15 = new List<string>()
    {
       "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> shieldman_15 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> halbert_5_crossbow_5_knight_5 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "knight","knight","knight","knight","knight",
        "none","none","none","none","none"
    };

    public List<string> halbert_7_Archmage_4_Mage_4 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","none","none","none",
        "mage","mage","mage","mage","none",
        "none","none","none","none","none",
        "archmage","archmage","archmage","archmage","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_7_Archer_8 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","none","none"
    };
    public List<string> knight_15 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> crossbow_8_knight_7 = new List<string>()
    {
         "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","none","none",
        "none","none","none","none","none",
        "knight","knight","knight","knight","knight",
        "knight","knight","none","none","none",
        "none","none","none","none","none"

    };

    public List<string> halbert_8_knight_7 = new List<string>()
    {
         "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","none","none",
        "none","none","none","none","none",
        "knight","knight","knight","knight","knight",
        "knight","knight","none","none","none",
        "none","none","none","none","none"

    };
    //Lineup 16
    public List<string> knight_16 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> halbert_8_crossbow_8 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","none","none",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_12_Mage_2_Archmage_2 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","none","none","none",
        "none","none","none","none","none",
        "mage","mage","archmage","archmage","none",
        "none","none","none","none","none"
    };
    //Lineup 17
    public List<string> knight_17 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> halbert_10_Mage_7 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "mage","mage","mage","mage","mage",
        "mage","mage","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    //Lineup 18
    public List<string> halbert_5_Mage_6_Archmage_7 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "mage","mage","mage","mage","mage",
        "mage","none","none","none","none",
        "archmage","archmage","archmage","archmage","archmage",
        "archmage","none","none","none","none"
    };

    public List<string> shieldman_4_Archer_14 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
       "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
       "archer","archer","archer","archer","none"
    };


    public List<string> knight_18 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    //Lineup 20
    public List<string> archer_20 = new List<string>()
    {
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> knight_20 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight"
    };
    public List<string> halbert_10_Crossbow_10 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> shieldman_10_Archmage_5_Mage_5 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "mage","mage","mage","mage","mage",
        "archmage","archmage","archmage","archmage","archmage",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> knight_10_Mage_5_Archer_5 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
       "knight","knight","knight","knight","knight",
        "mage","mage","mage","mage","mage",
        "none","none","none","none","none",
        "archer","archer","archer","archer","archer",
        "none","none","none","none","none"
    };
    public List<string> shieldman_10_Archmage_10 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "archmage","archmage","archmage","archmage","archmage",
        "archmage","archmage","archmage","archmage","archmage",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> Archmage_20 = new List<string>()
    {
         "archmage","archmage","archmage","archmage","archmage",
         "archmage","archmage","archmage","archmage","archmage",
         "archmage","archmage","archmage","archmage","archmage",
        "archmage","archmage","archmage","archmage","archmage",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> halbert_10_Mage_2_Archmage_2_Crossbow_6 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "mage","mage","archmage","archmage","none",
        "none","none","none","none","none",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","none","none","none","none"
    };

    public List<string> shieldman_10_Crossbow_10 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> shieldman_5_Archer_15 = new List<string>()
    {
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "none","none","none","none","none",
        "none","none","none","none","none",
         "shieldman","shieldman","shieldman","shieldman","shieldman"
    };

    public List<string> shieldman_20 = new List<string>()
    {
         "shieldman","shieldman","shieldman","shieldman","shieldman",
         "shieldman","shieldman","shieldman","shieldman","shieldman",
         "shieldman","shieldman","shieldman","shieldman","shieldman",
         "shieldman","shieldman","shieldman","shieldman","shieldman",
        "none","none","none","none","none",
         "none","none","none","none","none"
    };


    //Lineup 22

    public List<string> halbert_15_Crossbow_7 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","none","none","none"
    };

    public List<string> knight_15_mage_7 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "mage","mage","mage","mage","mage",
        "mage","mage","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> shieldman_7_Archer_15 = new List<string>()
    {
         "shieldman","shieldman","shieldman","shieldman","shieldman",
         "shieldman","shieldman","none","none","none",
        "none","none","none","none","none",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer"
    };

    public List<string> knight_22 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> halbert_22 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> crossbow_22 = new List<string>()
    {
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","none","none","none",
        "none","none","none","none","none",

    };


    public List<string> halbert_10_Armage_5_Archer_7 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "archmage","archmage","archmage","archmage","archmage",
        "none","none","none","none","none",
        "archer","archer","archer","archer","archer",
        "archer","archer","none","none","none"
    };
    public List<string> shieldman_10_Armage_5_Knight_7 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "archmage","archmage","archmage","archmage","archmage",
        "none","none","none","none","none",
        "knight","knight","knight","knight","knight",
        "knight","knight","none","none","none"
    };

    
    public List<string> Halbert_20 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> Crossbow_20 = new List<string>()
    {
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "none","none","none","none","none",
        "none","none","none","none","none"

    };

    public List<string> Crossbow_12 = new List<string>()
    {
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","none","none","none","none",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };

    public List<string> all_None = new List<string>()
    {
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    public List<string> halbert_1 = new List<string>()
    {
        "halbert","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none",
        "none","none","none","none","none"
    };
    //Lineup 23
    public List<string> shieldman_5_Crossbow_5__Knight_13 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "none","none","none","none","none",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
       "knight","knight","knight","none","none"
    };

    public List<string> halbert_23 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
       "halbert","halbert","halbert","none","none",
        "none","none","none","none","none" 
    };
    //Lineup 25
    public List<string> archer_25 = new List<string>()
    {
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
       "archer","archer","archer","archer","archer",
        "none","none","none","none","none"
    };
    //Lineup 30
    public List<string> halbert_20_Crossbow_10 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
       "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow"
    };
    public List<string> halbert_30 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
       "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert"
    };
    public List<string> archer_30 = new List<string>()
    {
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
       "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer"
    };
    public List<string> shieldman_15_Armage_15 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
       "archmage","archmage","archmage","archmage","archmage",
        "archmage","archmage","archmage","archmage","archmage",
        "archmage","archmage","archmage","archmage","archmage"
    };

    public List<string> shieldman_10_Archer_15_Armage_5 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "archer","archer","archer","archer","archer",
       "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archmage","archmage","archmage","archmage","archmage"
    };

    public List<string> shieldman_10_Archmage_5_Mage_5_Archer_10 = new List<string>()
    {
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "shieldman","shieldman","shieldman","shieldman","shieldman",
        "archmage","archmage","archmage","archmage","archmage",
        "mage","mage","mage","mage","mage",
        "archer","archer","archer","archer","archer",
       "archer","archer","archer","archer","archer"
    };

    public List<string> halbert_10_Crossbow_10_Archer_10 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer"
    };

    public List<string> halbert_10_Crossbow_10_Knight_10 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight"
    };
    public List<string> halbert_10_Crossbow_5_Mage_5_Archmage_5_Archer_5 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "archmage","archmage","archmage","archmage","archmage",
        "mage","mage","mage","mage","mage",
        "archer","archer","archer","archer","archer"
    };

    public List<string> halbert_15_Crossbow_15 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow"
    };
    public List<string> halbert_15_Knight_15 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight"
    };

    public List<string> archer_15_Knight_15 = new List<string>()
    {
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight"
    };

    public List<string> archer_15_knight_15 = new List<string>()
    {
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",

    };
    public List<string> halbert_10_Archmage_10_Archer_10 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "archmage","archmage","archmage","archmage","archmage",
        "archmage","archmage","archmage","archmage","archmage",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer"
    };
    public List<string> halbert_10_Mage_5_Archmage_5_Crossbow_10 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "archmage","archmage","archmage","archmage","archmage",
        "mage","mage","mage","mage","mage",
        "crossbow","crossbow","crossbow","crossbow","crossbow",
        "crossbow","crossbow","crossbow","crossbow","crossbow"
    };
    public List<string> halbert_10_Mage_10_Archer_10 = new List<string>()
    {
        "halbert","halbert","halbert","halbert","halbert",
        "halbert","halbert","halbert","halbert","halbert",
        "mage","mage","mage","mage","mage",
        "mage","mage","mage","mage","mage",
        "archer","archer","archer","archer","archer",
        "archer","archer","archer","archer","archer"
    };

    public List<string> knight_30 = new List<string>()
    {
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight",
        "knight","knight","knight","knight","knight"

    };





}
