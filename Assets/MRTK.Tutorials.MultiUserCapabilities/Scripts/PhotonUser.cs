using Photon.Pun;
using UnityEngine;

namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class PhotonUser : MonoBehaviour
    {
        private PhotonView pv;
        private string pinNum;

        private void Start()
        {
            pv = GetComponent<PhotonView>();

            if (!pv.IsMine) return;

            Vector3 position = gameObject.transform.position;
            pinNum = PhotonLobby.Lobby.input_PIN.text;
            EyegazeUIManager.main.myPinNum = pinNum;

            pv.RPC("PunRPC_SetNickName", RpcTarget.AllBuffered, pinNum);
            pv.RPC("PunRPC_SetPosition", RpcTarget.AllBuffered, position);
        }

        public string GetPIN()
        {
            return gameObject.name;
        }

        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }

        [PunRPC]
        private void PunRPC_SetNickName(string nName)
        {
            gameObject.name = nName;
        }

        [PunRPC]
        private void PunRPC_SetPosition(Vector3 nPosition)
        {
            gameObject.transform.position = nPosition;
        }

        [PunRPC]
        private void PunRPC_ShareAzureAnchorId(string anchorId)
        {
            GenericNetworkManager.Instance.azureAnchorId = anchorId;

            Debug.Log("\nPhotonUser.PunRPC_ShareAzureAnchorId()");
            Debug.Log("GenericNetworkManager.instance.azureAnchorId: " + GenericNetworkManager.Instance.azureAnchorId);
            Debug.Log("Azure Anchor ID shared by user: " + pv.Controller.UserId);
        }

        public void ShareAzureAnchorId()
        {
            if (pv != null)
                pv.RPC("PunRPC_ShareAzureAnchorId", RpcTarget.AllBuffered,
                    GenericNetworkManager.Instance.azureAnchorId);
            else
                Debug.LogError("PV is null");
        }
    }
}
