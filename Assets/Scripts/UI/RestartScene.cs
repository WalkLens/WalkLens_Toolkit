using MRTK.Tutorials.MultiUserCapabilities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    public void RestartCurrentScene()
    {
        if (PhotonLobby.Lobby != null) PhotonLobby.Lobby.DisconnectFromPhoton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}