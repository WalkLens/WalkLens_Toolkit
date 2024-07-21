using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonLobby : MonoBehaviourPunCallbacks
    {
        public static PhotonLobby Lobby;

        private int roomNumber = 1;
        public int userIdCount;
        public Text input_Name;
        //Row key
        public Text input_PIN;
        public DataManagerCtrl dataManagerCtrl;
        public GameObject userProfilePrefab;
        public Transform userListContent;
        public PhotonView pvUserProfileList;

        private void Awake()
        {
            if (Lobby == null)
            {
                Lobby = this;
            }
            else
            {
                if (Lobby != this)
                {
                    Destroy(Lobby.gameObject);
                    Lobby = this;
                }
            }

            DontDestroyOnLoad(gameObject);

            GenericNetworkManager.OnReadyToStartNetwork += StartNetwork;
        }

        public override void OnConnectedToMaster()
        {
            var randomUserId = Random.Range(0, 999999);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.AuthValues = new AuthenticationValues();
            PhotonNetwork.AuthValues.UserId = randomUserId.ToString();
            userIdCount++;
            PhotonNetwork.NickName = PhotonNetwork.AuthValues.UserId;
            //PhotonNetwork.JoinRandomRoom();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            Debug.Log("\nPhotonLobby.OnJoinedRoom()");
            Debug.Log("Current room name: " + PhotonNetwork.CurrentRoom.Name);
            Debug.Log("Other players in room: " + PhotonNetwork.CountOfPlayersInRooms);
            Debug.Log("Total players in room: " + (PhotonNetwork.CountOfPlayersInRooms + 1));

            DatabaseOnJoinedRoom(input_PIN.text);
            pvUserProfileList.RPC("PunRPC_DatabaseOnJoinedRoom", RpcTarget.AllBuffered, input_PIN.text);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            CreateRoom();
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("\nPhotonLobby.OnCreateRoomFailed()");
            Debug.LogError("Creating Room Failed");
            CreateRoom();
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            roomNumber++;
        }

        public void OnCancelButtonClicked()
        {
            PhotonNetwork.LeaveRoom();
        }

        private void StartNetwork()
        {
            PhotonNetwork.ConnectUsingSettings();
            Lobby = this;
        }

        private void CreateRoom()
        {
            var roomOptions = new RoomOptions { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
            PhotonNetwork.CreateRoom("Room" + Random.Range(1, 3000), roomOptions);
        }

        public void JoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        private async void DatabaseOnJoinedRoom(string pinNum)
        {
            if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
            {
                var user = await dataManagerCtrl.LoadUser(pinNum);
                GameObject userProfileInstance = Instantiate(userProfilePrefab, userListContent);
                TextMeshProUGUI userProfileText = userProfileInstance.GetComponentInChildren<TextMeshProUGUI>();
                userProfileText.text += "Name : " + user.Name + "\n";
                userProfileText.text += "Job : " + user.Job + "\n";
                userProfileText.text += "Hobby : " + user.Hobby + "\n";
            }
            else
            {
                Debug.LogError("DataManagerCtrl is not ready.");
            }
        }
        [PunRPC]
        private async void PunRPC_DatabaseOnJoinedRoom(string pinNum)
        {
            if (dataManagerCtrl != null && dataManagerCtrl.IsReady)
            {
                var user = await dataManagerCtrl.LoadUser(pinNum);
                GameObject userProfileInstance = Instantiate(userProfilePrefab, userListContent);
                TextMeshProUGUI userProfileText = userProfileInstance.GetComponentInChildren<TextMeshProUGUI>();
                userProfileText.text += "Name : " + user.Name + "\n";
                userProfileText.text += "Job : " + user.Job + "\n";
                userProfileText.text += "Hobby : " + user.Hobby + "\n";
            }
            else
            {
                Debug.LogError("DataManagerCtrl is not ready.");
            }
        }
    }
}
