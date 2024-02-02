using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class S_GameManager : MonoBehaviourPunCallbacks
{

    public GameObject Player = null;
    public GameObject spawnPoint;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Vector3 spawnptV3 = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y + 1.0f, spawnPoint.transform.position.z);
        Player = PhotonNetwork.Instantiate("Player", spawnptV3, Quaternion.identity, 0);


    }
}
