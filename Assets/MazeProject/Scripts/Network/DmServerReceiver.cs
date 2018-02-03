using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DmServerReceiver : NetworkBehaviour {

    DmPlayerManager dmPlayerManager;

    [ClientRpc]
    public void RpcStartGamePhase() {
        print("StartGamePhase for DM");
        if(dmPlayerManager != null)
            dmPlayerManager.StartGamePhase();
    }
}
