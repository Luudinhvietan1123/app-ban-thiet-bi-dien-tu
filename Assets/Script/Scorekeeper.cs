using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class Scorekeeper : MonoBehaviourPunCallbacks
{
    public int ScoreLimit = 10;
    public Transform SpawnP1;
    public Transform SpawnP2;
    public GameObject paddlePrefab;
    public TextMesh PingDisplay;
    public TextMesh Player1ScoreDisplay;
    public TextMesh Player2ScoreDisplay;
    private int p1Score = 0;
    private int p2Score = 0;
    PhotonView view;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            view = GetComponent<PhotonView>();
            PhotonNetwork.Instantiate(paddlePrefab.name, SpawnP1.position, Quaternion.identity, 0);
        }
        Player2ScoreDisplay.text = "Waiting...";
    }

    public override void OnPlayerEnteredRoom(Player thisPlayer)
    {
        view.RPC("RPC_DoSpawn", thisPlayer, SpawnP2.position);
        Player2ScoreDisplay.text = "0";
    }

    public override void OnJoinedRoom()
    {
        // when a player joins, tell them to spawn
        /*view.RPC("RPC_DoSpawn", thisPlayer, SpawnP2.position);*/
        // change player 2's score display from "waiting..." to "0"
        Player2ScoreDisplay.text = "0";
    }

    public override void OnPlayerLeftRoom(Player thisPlayer)
    {
        p1Score = 0;
        p2Score = 0;
        Player1ScoreDisplay.text = p1Score.ToString();
        Player2ScoreDisplay.text = "Waiting...";
    }

    public override void OnLeftRoom()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Lobby");
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("OnJoinedLobby");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster");
    }

    [PunRPC]
    void RPC_DoSpawn(Vector3 position)
    {
        PhotonNetwork.Instantiate(paddlePrefab.name, position, Quaternion.identity, 0);
    }
    public void AddScore(int player)
    {

        view.RPC("net_AddScore", RpcTarget.All, player);
    }

    [PunRPC]
    public void net_AddScore(int player)
    {
        if (player == 1)
        {
            p1Score++;
        }
        else if (player == 2)
        {
            p2Score++;
        }

        if (p1Score >= ScoreLimit || p2Score >= ScoreLimit)
        {
            if (p1Score > p2Score)
            {
                Debug.Log("Player 1 wins");
            }
            else if (p2Score > p1Score)
            {
                Debug.Log("Player 2 wins");
            }
            else
            {
                Debug.Log("Players are tied");
            }

            p1Score = 0;
            p2Score = 0;
        }

        Player1ScoreDisplay.text = p1Score.ToString();
        Player2ScoreDisplay.text = p2Score.ToString();
    }

    private void Update()
    {
        PingDisplay.text = PhotonNetwork.GetPing().ToString() + "ms";
    }
}
