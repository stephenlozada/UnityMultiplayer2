﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerSpawn : NetworkBehaviour {
    public GameObject playerPrefab;
    [SerializeField] GameObject playerInstance;
    float respawnTimer = 1f;
	public int numLives = 4;

    // Use this for initialization\
    public override void OnStartServer()
    {
       // Spawn();

	}
	
	// Update is called once per frame
	void Update () {
        if(playerInstance == null && isLocalPlayer)
        {
            respawnTimer -= Time.deltaTime;
            //if(respawnTimer<=0 )
            Spawn();
        }
	
	}
    public void Spawn()
    {
        if (isLocalPlayer)
        {
            //numLives--;
            ScoreHandler.lives--;
            respawnTimer = 1f;
            if (ScoreHandler.lives >= 0)
                playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
            NetworkServer.Spawn(playerInstance);
            if (ScoreHandler.lives == -1)
            {
                Application.LoadLevel("EndScreen");

            }
        }
    }
	//void OnGUI()
	//{
 //       if (ScoreHandler.lives > 0 || playerInstance != null)
 //           GUI.Label(new Rect(0, 15, 100, 50), "Lives Left: " + numLives);
 //       else
 //           //GUI.Label (new Rect (Screen.width /2 - 50, Screen.height/2-25, 100, 50), "GameOVer!!!!!! " );
 //           Application.LoadLevel("EndScreen");
	//}
}
