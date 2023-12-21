using UnityEngine;         
using UnityEngine.SceneManagement;
using Photon.Pun;         
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

namespace com.nicknamechatapp
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        public string username;
        //the maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.
        [SerializeField] private byte maxPlayersPerRoom = 20;
        //this client's version number. Users are separated from each other by gameVersion
        string gameVersion = "1";
        [SerializeField] private GameObject controlPanel;
        private string sceneName;
        public GameObject connectingText;
        void Awake()
        {

            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;

        }

        void Start()
        {
            controlPanel.SetActive(true);
            connectingText.SetActive(false);
        }
        public void ExitTheApp()
        {
            Application.Quit();
            Debug.Log("Quit Application");
        }

        // Start the connection process.If already connected, we attempt joining a random room,- if not yet connected, Connect this application instance to Photon Cloud Network
        public void Connect(string sName)
        {

            sName = "ChatScene";
            sceneName = sName;


            controlPanel.SetActive(false);
            connectingText.SetActive(true);
            connectingText.gameObject.GetComponent<TextMeshProUGUI>().text = "Connecting...";

            if (PhotonNetwork.IsConnected)
            {

                //we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                //we must first and foremost connect to Photon Online Server
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }


            // we check if we are connected or not, we join if we are , else we initiate the connection to the server

        }


        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");

            int playerCnt = PhotonNetwork.CountOfPlayersInRooms + PhotonNetwork.CountOfPlayersOnMaster;
            Debug.Log("Players on server = " + playerCnt);


            PhotonNetwork.CreateRoom("Room");
        }



        public override void OnDisconnected(DisconnectCause cause)
        {

            controlPanel.SetActive(true);
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            //we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(sceneName);
            }
        }

        public void LoadScene()
        {
            Debug.Log("LOADING SCENE.");
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(sceneName);
            }
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            PhotonNetwork.JoinRoom("Room");
        }

    }


}