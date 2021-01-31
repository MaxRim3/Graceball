using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OwnershipBarrierController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ball")
        {
            //collider.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.);
            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                if (collider.gameObject.GetComponent<PhotonView>().Owner != PhotonNetwork.CurrentRoom.GetPlayer(i))
                {
                    collider.gameObject.GetComponent <PhotonView>().TransferOwnership(PhotonNetwork.CurrentRoom.GetPlayer(i));
                }
            }
        }
    }
}
