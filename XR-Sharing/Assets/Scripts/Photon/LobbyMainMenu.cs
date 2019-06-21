using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Photon {
    public class LobbyMainMenu : MonoBehaviourPunCallbacks {
        [SerializeField] private Button _loginButton;

        #region UNITY

        public void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
            _loginButton.enabled = false;
            PhotonNetwork.ConnectUsingSettings();
        }

        #endregion

        #region PUN CALLBACKS

        public override void OnConnectedToMaster() {
            _loginButton.enabled = true;
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList) { }

        public override void OnDisconnected(DisconnectCause cause) {
            Debug.LogWarning("Disconnected due to: " + cause);
        }

        public override void OnLeftLobby() { }

        public override void OnCreateRoomFailed(short returnCode, string message) { }

        public override void OnJoinRoomFailed(short returnCode, string message) { }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            string roomName = "Room " + Random.Range(1000, 10000);

            RoomOptions options = new RoomOptions {MaxPlayers = 8};

            PhotonNetwork.CreateRoom(roomName, options, null);
        }

        public override void OnJoinedRoom() {
            PhotonNetwork.LoadLevel("XRSharing");


            foreach (Player p in PhotonNetwork.PlayerList) {
//                GameObject entry = Instantiate(PlayerListEntryPrefab);
//                entry.transform.SetParent(InsideRoomPanel.transform);
//                entry.transform.localScale = Vector3.one;
//                entry.GetComponent<PlayerListEntry>().Initialize(p.ActorNumber, p.NickName);
//
//                object isPlayerReady;
//                if (p.CustomProperties.TryGetValue(AsteroidsGame.PLAYER_READY, out isPlayerReady))
//                {
//                    entry.GetComponent<PlayerListEntry>().SetPlayerReady((bool) isPlayerReady);
//                }
//
//                playerListEntries.Add(p.ActorNumber, entry);
            }

//            StartGameButton.gameObject.SetActive(CheckPlayersReady());
//
//            Hashtable props = new Hashtable
//            {
//                {AsteroidsGame.PLAYER_LOADED_LEVEL, false}
//            };
//            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }

        public override void OnLeftRoom() { }

        public override void OnPlayerEnteredRoom(Player newPlayer) { }

        public override void OnPlayerLeftRoom(Player otherPlayer) { }

        public override void OnMasterClientSwitched(Player newMasterClient) { }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) { }

        #endregion

        #region UI CALLBACKS

        public void OnLeaveGameButtonClicked() {
            PhotonNetwork.LeaveRoom();
        }

        public void OnStartGameButtonClicked() {
            string roomName = "MrDevSummitRoom";
            RoomOptions options = new RoomOptions {MaxPlayers = 8};
            PhotonNetwork.CreateRoom(roomName, options, null);
        }

        #endregion

        private bool CheckPlayersReady() {
            if (!PhotonNetwork.IsMasterClient) {
                return false;
            }

            foreach (Player p in PhotonNetwork.PlayerList) {
                object isPlayerReady;
                if (p.CustomProperties.TryGetValue(AsteroidsGame.PLAYER_READY, out isPlayerReady)) {
                    if (!(bool) isPlayerReady) {
                        return false;
                    }
                } else {
                    return false;
                }
            }

            return true;
        }
    }
}