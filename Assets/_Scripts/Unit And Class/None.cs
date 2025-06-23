using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.playerTeam.Contains(gameObject))
        {
            gameManager.playerTeam.Remove(gameObject);
        }
        if (gameManager.enemyTeam.Contains(gameObject))
        {
            gameManager.enemyTeam.Remove(gameObject);
        }
        Destroy(gameObject);
    }

}
