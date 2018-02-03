using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlayerManager : MonoBehaviour {

    [Header("Prefab")]
    public GameObject heroShopMenu;
    public GameObject heroPlayMenu;
    public GameObject gameControllerPrefab;

    GameObject currentOpenMenu;
    HeroServerCall heroServerCall;
    GameObject gameController;
    // Use this for initialization
    void Start () {
		
	}

    public void SetHeroServerCall(HeroServerCall _heroServerCall) {
        heroServerCall = _heroServerCall;
    }

    public void StartShopMenu() {
        currentOpenMenu = Instantiate(heroShopMenu,transform);

        Shop s = GameObject.Find("ShopManager").GetComponent<Shop>();
        s.heroPlayerManager = this;
        print(s + "  is initialize");

        gameController = Instantiate(gameControllerPrefab, transform);
    }

    public void CallServerImReady() {
        print("CallsERverImReady");
        heroServerCall.CmdPlayerReady(true);
    }

    public void StopShopMenu() {
        Destroy(currentOpenMenu);
    }

    public void StartPlayMenu() {
        currentOpenMenu = Instantiate(heroPlayMenu, transform);
    }

    public void StopPlayMenu() {
        Destroy(currentOpenMenu);
    }


    public void StartGamePhase() {
        StopShopMenu();
        StartPlayMenu();
    }

}
