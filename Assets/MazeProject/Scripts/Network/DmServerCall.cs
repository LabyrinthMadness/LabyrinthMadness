using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DmServerCall : NetworkBehaviour {

    HeroServerCall heroServerCall;

    public void SetHeroServerCall(HeroServerCall _heroServerCall) {
        heroServerCall = _heroServerCall;
        CmdSaveServerCall(_heroServerCall.gameObject);
    }

    [Command]
    public void CmdSaveServerCall(GameObject go) {
        heroServerCall = go.GetComponent<HeroServerCall>();
    }

    [Command]
    public void CmdPlayerReady() {
        print("Call cmd on heroservercall");
        heroServerCall.CmdPlayerReady(true);
    }
}
