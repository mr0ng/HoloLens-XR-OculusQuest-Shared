using Photon.Pun;
using UnityEngine;
using XRTK.Utilities;

namespace Photon {
    public class NetworkedAvatar : MonoBehaviour {
        [SerializeField] private GameObject _avatarPrefab;

        // Start is called before the first frame update
        void Start() {
            GameObject instantiate = PhotonNetwork.Instantiate(_avatarPrefab.name,
                CameraCache.Main.gameObject.transform.position,
                Quaternion.identity);
            if (!instantiate) {
                Debug.LogError("could not spawn player.");
            } else {
                instantiate.transform.SetParent(CameraCache.Main.transform);
                instantiate.transform.localPosition = Vector3.zero;
                instantiate.transform.localRotation = Quaternion.identity;
            }
        }
    }
}