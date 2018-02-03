using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WhatsMyIp : NetworkBehaviour {
    public Text _text;
    public Button cancel;
    public Button show;
	// Use this for initialization
	void Start () {
        print("what is life");
        print("lol : "+Network.player.ipAddress);
    }

    public void lol() {
        gameObject.SetActive(true);
        _text.gameObject.SetActive(true);

    }

    public void Hide() {
        show.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
        cancel.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
