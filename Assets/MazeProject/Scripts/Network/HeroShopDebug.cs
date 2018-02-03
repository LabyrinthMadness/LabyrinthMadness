using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShopDebug : MonoBehaviour {

    HeroServerCall heroServerCall;

    public void SetServerCall(HeroServerCall _heroServerCall) {
        heroServerCall = _heroServerCall;
    }

    public void SendPlayerReady() {
        print("Button pressed");
        heroServerCall.CmdPlayerReady(true);
    }
}
