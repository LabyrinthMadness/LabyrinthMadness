using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetGamePlayer : NetworkBehaviour {

    [Header("State")]
    [SyncVar] public bool isServerr = false;
    [SyncVar] public bool hostHasBeenSpawned = false;
    [Header("Players Prefab")]
    public GameObject heroPlayerObj;
    public GameObject dmPlayerObj;

    GameObject clientPlayerChild;

    // Use this for initialization
    void Start () {
        Invoke("InitializeGame", 1);
    }

    void InitializeGame() {
        //Spawns corresponding Player Pref
        if(isLocalPlayer) {
            if(base.isServer) {
                //Change Indice
                isServerr = true;
                //Instantiate Client Player 
                SpawnClientPlayer(heroPlayerObj);
                HeroPlayerManager hpm = clientPlayerChild.GetComponent<HeroPlayerManager>();
                HeroServerCall hsc = GetComponent<HeroServerCall>();
                hpm.SetHeroServerCall(GetComponent<HeroServerCall>());
                hsc.SetDmServerReceiver(GetOtherGamePlayer().GetComponent<DmServerReceiver>());
                hpm.StartShopMenu();
            } else {
                SpawnClientPlayer(dmPlayerObj);
                DmPlayerManager dmpm = clientPlayerChild.GetComponent<DmPlayerManager>();
                DmServerCall dmsc = GetComponent<DmServerCall>();
                dmpm.SetDmServerCall(dmsc);
                dmsc.SetHeroServerCall(GetOtherGamePlayer().GetComponent<HeroServerCall>());
                dmpm.StartShopMenu();
            }
        }
    }

    void SpawnClientPlayer(GameObject _prefab) {
        clientPlayerChild = Instantiate(_prefab, transform);
    }

    GameObject GetOtherGamePlayer() {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("NetworkGamePlayer")) {
            print("Go name : " + go);
            if(go != gameObject) {
                print("HE find one");
                return go;
            }
        }

        return null;
    }
}