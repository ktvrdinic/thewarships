//using Photon.Pun;
//using ExitGames.Client.Photon;
//using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonMainMenu : Photon.PunBehaviour
{

    public PlayerInformation player;
    public byte Version = 1;

    #region UNITY

    public void Awake()
    {
        PhotonNetwork.automaticallySyncScene = true;


    }

    #endregion

    // Use this for initialization
    void Start () {

        if (player == null) return;

        string playerName = player.playerName;

        if (!playerName.Equals(""))
        {
            PhotonNetwork.playerName = playerName;

            PhotonNetwork.ConnectUsingSettings(Version.ToString());
            //PhotonNetwork.GameVersion = this.Version + "." + SceneManagerHelper.ActiveSceneBuildIndex;
            Debug.Log("PhotonMainMenu.Start() will now call: PhotonNetwork.ConnectUsingSettings().");
        }
        else
        {
            Debug.LogError("Player Name is invalid.");
        }

        Debug.Log("PlaerName: " + PhotonNetwork.playerName);

    }


    #region PUN CALLBACKS

    public override void OnConnectedToMaster()
    {
        //this.SetActivePanel(SelectionPanel.name);
        Debug.Log("PhotonMainMenu.OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: MainMenuManager.Instance.OpenMenuPanelsOnMaster();");
        MainMenuManager.Instance.OpenMenuPanelsOnMaster();

        //if (ImageFade.Instance)
        //{
        //    ImageFade.Instance.fadeaway = true;
        //    ImageFade.Instance.startfade = true;
        //}

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("PhotonMainMenu.OnJoinedLobby(). This client is connected and does get a room-list, which gets stored as PhotonNetwork.GetRoomList(). This script now calls: PhotonNetwork.JoinRandomRoom();");
        
    }

  
    public override void OnLeftLobby()
    {
        
    }





    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {

        Debug.Log("PhotonMainMenu.OnJoinRandomFailed() was called by PUN. No random room available, so we create one. Calling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions options = new RoomOptions { maxPlayers = 2 };

        PhotonNetwork.CreateRoom(roomName, options, null);

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PhotonMainMenu.OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running.");

        Debug.Log("Joined room: " + PhotonNetwork.room.name);

        //PhotonNetwork.LoadLevel("SeaWars_GameScene");

        // PhotonNetwork.LoadLevel("SeaWars_LobbyScene");
    }

    public override void OnLeftRoom()
    {
        
    }



    public override void OnDisconnectedFromPhoton()
    {
        Debug.Log("PhotonMainMenu.OnDisconnectedFromPhoton()");
    
        UnityEngine.SceneManagement.SceneManager.LoadScene("LoginScene");
    }

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    //{

    //}

    #endregion


    public void OnClickToBattleButton()
    {
        ///PhotonNetwork.JoinRandomRoom();
        if (ImageFade.Instance)
        {
            ImageFade.Instance.fadeaway = false;
            ImageFade.Instance.startfade = true;
        }


        StartCoroutine(LoadLevel());
       // PhotonNetwork.LoadLevel("SeaWars_GameScene");  

        //PhotonNetwork.CurrentRoom.IsOpen = false;
        //PhotonNetwork.CurrentRoom.IsVisible = false;
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.LoadLevel("SeaWars_GameScene");
    }

}
