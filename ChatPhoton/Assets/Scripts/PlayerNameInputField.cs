using UnityEngine;
using UnityEngine.UI;


using Photon.Pun;
using Photon.Realtime;
using TMPro;

using System.Collections;

namespace com.nicknamechatapp
{
    public class PlayerNameInputField : MonoBehaviour
    {
        //we store the PlayerPref Key
        const string playerNamePrefKey = "PlayerName";
        TMP_InputField _inputField;
        void Start () {

            string defaultName = string.Empty;
            TMP_InputField _inputField = this.GetComponent<TMP_InputField>();
            if (_inputField!=null)
            {
             
            }

            PhotonNetwork.NickName =  _inputField.text;
        }
        /// Sets the name of the player, and save it in the PlayerPrefs
        public void SetPlayerName(string value)
        {
            
            if (string.IsNullOrEmpty(value))
            {
                int RandGuest = Random.Range(654,49849);
                value = ("Guest"+RandGuest).ToString();
                PhotonNetwork.NickName = value;
            }

            if (value == null)
            {
                int RandGuest = Random.Range(654, 49849);
                value = ("Guest" + RandGuest);
                PhotonNetwork.NickName = value;
            }
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNamePrefKey,value);
        }
    }
}

