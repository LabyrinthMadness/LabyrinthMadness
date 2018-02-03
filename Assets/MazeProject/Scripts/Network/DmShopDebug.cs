using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmShopDebug : MonoBehaviour {

    DmServerCall dmServerCall;

    public void SetServerCall(DmServerCall _dmServerCall) {
        dmServerCall = _dmServerCall;
    }

    public void SendPlayerReady() {
        print("Button pressed");
        dmServerCall.CmdPlayerReady();
    }
}