using MRTK.Tutorials.AzureCloudServices.Scripts.Managers;
using MRTK.Tutorials.MultiUserCapabilities;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserListUIManager : MonoBehaviourPunCallbacks
{
    public DataManagerCtrl dataManagerCtrl;
    public GameObject userProfilePrefab;
    public Transform userListContent;
    public PhotonView pvUserProfileList;
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        //DatabaseOnJoinedRoom(PhotonLobby.Lobby.input_PIN.text);
        pvUserProfileList.RPC("PunRPC_DatabaseOnJoinedRoom", RpcTarget.AllBuffered, PhotonLobby.Lobby.input_PIN.text);
    }

    public async void DatabaseOnJoinedRoom(string pinNum)
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
