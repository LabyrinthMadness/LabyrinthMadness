using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HeroServerCall : NetworkBehaviour {

    DmServerReceiver dmServerReceiver;
    HeroPlayerManager heroPlayerManager;

    int playerReadyToFight=0;

    public void SetDmServerReceiver(DmServerReceiver _dmServerReceiver) {
        dmServerReceiver = _dmServerReceiver;
        heroPlayerManager = GetComponentInChildren<HeroPlayerManager>();
        CmdSaveData(_dmServerReceiver.gameObject);
    }

    [Command]
    void CmdSaveData(GameObject go) {
        dmServerReceiver = go.GetComponent<DmServerReceiver>();
        heroPlayerManager = GetComponentInChildren<HeroPlayerManager>();
        print("hero playermanager  : " + heroPlayerManager);
    }

    [Command]
    public void CmdPlayerReady(bool b) {
        
        playerReadyToFight = b==true ? playerReadyToFight+1 : playerReadyToFight - 1;
        print("CmdPlayer Ready : "+playerReadyToFight);
        if(playerReadyToFight == 2) {
            print("hero playermanager  : " + heroPlayerManager);
            heroPlayerManager.StartGamePhase();
            dmServerReceiver.RpcStartGamePhase();
            print("START GAME PHASE ");
        }

    }

}
