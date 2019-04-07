using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Photon.Pun;
//using Photon.Realtime;
using UnityEngine.UI;

public class CreatePlayers : Photon.PunBehaviour {



    public Transform[] spawnPoints;

    public RPGCamera camera;

    public byte Version = 1;

    private void Start()
    {

        if (!PhotonNetwork.connected)//isconnected
        {
            this.ConnectNow();
        }
        else
        {
            //CratePlayerObjects();
            PhotonNetwork.JoinRandomRoom();
        }

        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;

        //CratePlayerObjects();
    }

    public void ConnectNow()
    {
        Debug.Log("CreatePlayers.ConnectNow() will now call: PhotonNetwork.ConnectUsingSettings().");
        PhotonNetwork.ConnectUsingSettings(Version.ToString());
        //PhotonNetwork.GameVersion = this.Version + "." + SceneManagerHelper.ActiveSceneBuildIndex;
    }

    public override void OnConnectedToMaster()
    {
        //this.SetActivePanel(SelectionPanel.name);
        Debug.Log("CreatePlayer.OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("CreatePlayers.OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("CreatePlayers.OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 2}, null);");

        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions options = new RoomOptions { maxPlayers = 2 };

        PhotonNetwork.CreateRoom(roomName, options, null);
    }


    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)//OnJoinRandomFailed
    {
        Debug.Log("CreatePlayers.OnPhotonJoinRoomFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 2}, null);");

        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions options = new RoomOptions { maxPlayers = 2 };

        PhotonNetwork.CreateRoom(roomName, options, null);
    }

    public override void OnJoinedRoom()
    {
        //base.OnJoinedRoom();
        CratePlayerObjects();

        //if (ImageFade.Instance)
        //{
        //    ImageFade.Instance.fadeaway = !ImageFade.Instance.fadeaway;
        //    ImageFade.Instance.startfade = true;
        //}
        Debug.Log("OnJoinedRoom: " + PhotonNetwork.room.playerCount + ". -> " + PhotonNetwork.player.name + " : " + PhotonNetwork.player.isMasterClient);

    }

    private void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("CreatePlayers.OnDisconnectedFromServer()");
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoginScene");


        // save data to database
    }



    void CratePlayerObjects()
    {
        Transform trans;
        trans = spawnPoints[PhotonNetwork.room.playerCount - 1];

        PlayerInformation playerInformation;

        playerInformation = PlayerInformation.Instance;
        string ShipName;
        if (playerInformation == null)
        {
            ShipName = "Ships/Cartoon/Ship of the line";
        }
        else
        {
            ShipName = playerInformation.listOfShips[playerInformation.currentShipSelected].pathTo;
        }


        GameObject newPLayerObject = PhotonNetwork.Instantiate(ShipName, trans.position, trans.rotation, 0);



        //camera.transform.Rotate(0, 180, 0);

        camera.transform.SetParent(newPLayerObject.transform);

        camera.Target = newPLayerObject.transform;
        newPLayerObject.GetComponent<ShipController>().RPGcamera = camera;

        if (PhotonNetwork.room.playerCount == 2)
            newPLayerObject.transform.Rotate(0, 180, 0);

        if (playerInformation != null)
        {
            //BattleManger.Instance.playersInGame.Add(playerInformation.playerName + ":" + PhotonNetwork.room.playerCount);
            //playerinstance
            BattleManger.Instance.playersInGame.Add(playerInformation);

        }


        if (PhotonNetwork.room.playerCount == 1)
        {
            //wait for players panel
            BattleManger.Instance.waitForPlayers.SetActive(true);

        }
        else if (PhotonNetwork.room.playerCount == 2) //call on player 2 when connect
        {
            PrintPlayersInRoom("OnJoinedRoom");

            //BattleManger.Instance.waitForPlayers.SetActive(false);
            //BattleManger.Instance.playersInGame.Add(playerInformation);
            ////start battle
            //BattleManger.Instance.Init();



            //call battle manageer to update UI on player 2 when connect
            BattleManger.Instance.StartBattleWhenPlayersConnect();
        }


    }

    private void PrintPlayersInRoom(string s)
    {
        foreach (PhotonPlayer p in PhotonNetwork.playerList)
        {
            Debug.Log(s + ": PrintPlayersInRoom: " + " -> " + p.name + " : " + p.isMasterClient);
        }
    }




    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer) //call on player 1 when player2 connect
    {
        //base.OnPhotonPlayerConnected(newPlayer);
        PrintPlayersInRoom("OnPhotonPlayerConnected");


        //Debug.Log("OnPhotonPlayerConnected: " + PhotonNetwork.room.playerCount + ". -> " + newPlayer.name + " : " + newPlayer.isMasterClient);

        //PlayerInformation playerInformation;
        //playerInformation = PlayerInformation.Instance;


        if (PhotonNetwork.room.playerCount == 2)
        {
            //BattleManger.Instance.playersInGame.Add(playerInformation);

            //BattleManger.Instance.waitForPlayers.SetActive(false);


            ////start battle
            //BattleManger.Instance.Init();

            BattleManger.Instance.StartBattleWhenPlayersConnect();
            //call battle manageer to update UI on player 1 when player 2 connect
        }
    }



    public override void OnLeftRoom()
    {
        //base.OnLeftRoom();

        
        PhotonNetwork.LoadLevel("MainMenu01");
    }

    public void LeaveBattle()
    {
        PhotonNetwork.LeaveRoom();        
    }

    
}
