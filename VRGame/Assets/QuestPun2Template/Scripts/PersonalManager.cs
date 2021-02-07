using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sigtrap.VrTunnellingPro;
//
//For handling local objects and sending data over the network
//
namespace Networking.Pun2
{
    public class PersonalManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] GameObject headPrefab;
        [SerializeField] GameObject handRPrefab;
        [SerializeField] GameObject handLPrefab;
        [SerializeField] GameObject ovrCameraRig;
        [SerializeField] GameObject playerRigidbody;
        [SerializeField] GameObject playerRigidbodyPrefab;
        [SerializeField] Transform[] spawnPoints;
        [SerializeField] GameObject mainCamera;


        //Tools
        List<GameObject> toolsR;
        List<GameObject> toolsL;
        int currentToolR;
        int currentToolL;

        public GameObject rigidbodyObj;
        public GameObject leftHandObj;
        public GameObject rightHandObj;
        public GameObject leftRacket;
        public GameObject rightRacket;

        private void Awake()
        {
            /// If the game starts in Room scene, and is not connected, sends the player back to Lobby scene to connect first.
            if (!PhotonNetwork.NetworkingClient.IsConnected)
            {
                SceneManager.LoadScene("Photon2Lobby");
                return;
            }
            /////////////////////////////////

            toolsR = new List<GameObject>();
            toolsL = new List<GameObject>();

            if(PhotonNetwork.LocalPlayer.ActorNumber <= spawnPoints.Length)
            {
                playerRigidbody.transform.position = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.position;
                playerRigidbody.transform.rotation = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.rotation;
                //ovrCameraRig.transform.position = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.position;
                //ovrCameraRig.transform.rotation = spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber - 1].transform.rotation;
            }
        }

        private void Start()
        {
            //Instantiate Rigibody
            GameObject objrb = (PhotonNetwork.Instantiate(playerRigidbodyPrefab.name, OculusPlayer.instance.playerRigidbody.transform.position, OculusPlayer.instance.playerRigidbody.transform.rotation, 0));
            //mainCamera.GetComponent<TunnellingMobile>().motionTarget = objrb.transform;
            ovrCameraRig.transform.parent = objrb.transform;
            objrb.GetComponent<HandThrusters>().primaryCamera = ovrCameraRig.transform;
            rigidbodyObj = objrb;

            //Instantiate Head
            GameObject obj = (PhotonNetwork.Instantiate(headPrefab.name, OculusPlayer.instance.head.transform.position, OculusPlayer.instance.head.transform.rotation, 0));
            obj.GetComponent<SetColor>().SetColorRPC(PhotonNetwork.LocalPlayer.ActorNumber);
            
            //Instantiate right hand
            GameObject objR = (PhotonNetwork.Instantiate(handRPrefab.name, OculusPlayer.instance.rightHand.transform.position, OculusPlayer.instance.rightHand.transform.rotation, 0));
            rightHandObj = objR;
            for (int i = 0; i < objR.transform.childCount; i++)
            {
                toolsR.Add(objR.transform.GetChild(i).gameObject);
                objR.transform.GetComponentInChildren<SetColor>().SetColorRPC(PhotonNetwork.LocalPlayer.ActorNumber);
                if(i > 0)
                    toolsR[i].transform.parent.GetComponent<PhotonView>().RPC("DisableTool", RpcTarget.AllBuffered, 1);
            }
            objrb.GetComponent<HandThrusters>().rightHand = objR.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody>();
            GameObject rightRacketParent = objR.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;
            rightRacket = rightRacketParent;
            foreach(Transform child in rightRacketParent.transform)
            {
                child.GetComponent<BatCollider>().player = objrb;
            }

            //Instantiate left hand
            GameObject objL = (PhotonNetwork.Instantiate(handLPrefab.name, OculusPlayer.instance.leftHand.transform.position, OculusPlayer.instance.leftHand.transform.rotation, 0));
            leftHandObj = objL;
            for (int i = 0; i < objL.transform.childCount; i++)
            {
                toolsL.Add(objL.transform.GetChild(i).gameObject);
                objL.transform.GetComponentInChildren<SetColor>().SetColorRPC(PhotonNetwork.LocalPlayer.ActorNumber);
                if (i > 0)
                    toolsL[i].transform.parent.GetComponent<PhotonView>().RPC("DisableTool", RpcTarget.AllBuffered, 1);
            }
            objrb.GetComponent<HandThrusters>().leftHand = objL.gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<Rigidbody>();
            GameObject leftRacketParent = objL.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;
            leftRacket = leftRacketParent;
            foreach (Transform child in leftRacketParent.transform)
            {
                child.GetComponent<BatCollider>().player = objrb;
            }

        }

        //Detects input from Thumbstick to switch "hand tools"
        private void Update()
        {
            if (OVRInput.GetUp(OVRInput.Button.PrimaryThumbstick))
                SwitchToolL();

            if (OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick))
                SwitchToolR();
        }

        //disables current tool and enables next tool
        void SwitchToolR()
        {
            toolsR[currentToolR].transform.parent.GetComponent<PhotonView>().RPC("DisableTool", RpcTarget.AllBuffered, currentToolR);
            currentToolR++;
            if (currentToolR > toolsR.Count - 1)
                currentToolR = 0;
            toolsR[currentToolR].transform.parent.GetComponent<PhotonView>().RPC("EnableTool", RpcTarget.AllBuffered, currentToolR);
        }

        void SwitchToolL()
        {
            toolsL[currentToolL].transform.parent.GetComponent<PhotonView>().RPC("DisableTool", RpcTarget.AllBuffered, currentToolL);
            currentToolL++;
            if (currentToolL > toolsL.Count - 1)
                currentToolL = 0;
            toolsL[currentToolL].transform.parent.GetComponent<PhotonView>().RPC("EnableTool", RpcTarget.AllBuffered, currentToolL);
        }


        //If disconnected from server, returns to Lobby to reconnect
        public override void OnDisconnected(DisconnectCause cause)
        {
            print(cause + "THIS IS THE CAUSE");
            base.OnDisconnected(cause);
            SceneManager.LoadScene(0);
        }

    }
}
