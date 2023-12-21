using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;

namespace com.nicknamechatapp { 
    public class ChatInputField : MonoBehaviourPun
    {

        public string chatTxt = "";
        private AudioSource aud;
        [SerializeField] private AudioClip messageSoundEffect;
        private void Start()
        {
            if (aud == null) {
                aud = gameObject.AddComponent<AudioSource>();
            }
            aud = GetComponent<AudioSource>();
            aud.playOnAwake = false;
        }
        public void SetChatTxt(string value)
        {
           
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Chat Text is null or empty");
                return;
            }
            
            chatTxt = PhotonNetwork.NickName + ": " + value;
         
            photonView.RPC("ChatMessage", RpcTarget.All, PlayerUI.playerName  + chatTxt + "\n");
            Debug.Log(PlayerUI.playerName + " Is Sending Message!");
             
            GameObject.Find("InputField").GetComponent<InputField>().text = ""; 
        }

        //the RPCs implementation
        [PunRPC]
        void ChatMessage(string a, PhotonMessageInfo info)
        {
            GameObject[] respawns;

            respawns = GameObject.FindGameObjectsWithTag("TMPText");

            foreach (GameObject gObj in respawns)
            {     
                TMP_InputField tmp = gObj.GetComponent<TMP_InputField>();
                Debug.Log(tmp.text);
                tmp.text = a + tmp.text;
                Debug.Log(tmp.text);
                Debug.Log("Sent a message");
                aud.clip = messageSoundEffect;
                aud.Play();
            }


        }
    }
}

