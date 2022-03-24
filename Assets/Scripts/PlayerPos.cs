using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
   GameController gameController;
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        transform.position = gameController.lastCheckPointPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
