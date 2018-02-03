using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DmPlayerManager : MonoBehaviour {
    [Header("Scene level Async")]
    public string levelName;
    AsyncOperation async;

    [Header("Prefab")]
    public GameObject dmShopMenu;
    public GameObject dmPlayMenu;

    GameObject currentOpenMenu;
    DmServerCall dmServerCall;
    UntouchRaycast untouchRaycast;
    RaycastHit hit;
    GraphicRaycaster gr;
    GameObject miniMapObj;

    //Variables for The amazing feature of target place on the map in the shop 
    Camera additiveCamera;

    // Use this for initialization
    void Start () {
		
	}

    public void SetDmServerCall(DmServerCall _dmserverCall) {
        dmServerCall = _dmserverCall;
    }

    IEnumerator loadLevelSceneAsync() {
        async = SceneManager.LoadSceneAsync(levelName,LoadSceneMode.Additive);
        async.allowSceneActivation = true;

        yield return async;
        print("Scene is done loading");
        additiveCamera= GameObject.FindGameObjectWithTag("CameraAdditive").GetComponent<Camera>();
        print("additive camera : " + additiveCamera);
    }

    public void CallRaycastOnSecondCamera(Vector3 _positionClicked) {
        Vector3 vect = _positionClicked - miniMapObj.transform.position;
        print("vect du eventhandler : " + _positionClicked + "   poss de la minMap : " + miniMapObj.transform.position);
        Vector3 vectNormalize = vect.normalized;
        print("vector normalizre : " + vectNormalize);
        print("Enter Real call to ray cast to second camera");
        
        Ray ray = new Ray(additiveCamera.transform.position, vectNormalize);
        print("Additive camera position : " + additiveCamera.transform.position);

        Debug.DrawRay(additiveCamera.transform.position, (Vector3.forward + vectNormalize)*10, Color.red, 3f);

        if(Physics.Raycast(ray, out hit)) {
            print("It enter the raycast Bool with the tag : "+hit.collider.tag);
            if(hit.collider.tag == "LevelFloor") {
                GameObject go =  hit.collider.gameObject;
                print("This tiles : " + go + "   isOccupide : " + go.GetComponent<TilesClass>().isOccupied);
                if(!go.GetComponent<TilesClass>().isOccupied) {
                    print("It is free");    
                }
            }
        }
    }

    public void StartShopMenu() {
       Destroy(GameObject.Find("Main Camera"));
        currentOpenMenu = Instantiate(dmShopMenu, transform);
        GameObject.Find("PanelShopMenuGM").GetComponent<MathieuDMShop>().dmPlayerManager = this;
        
        //currentOpenMenu.GetComponent<DmShopDebug>().SetServerCall(dmServerCall);
    }

    public void StopShopMenu() {
        Destroy(currentOpenMenu);
    }

    public void StartPlayMenu() {
        //Function to change vision on the game
        print("START PLAY");
    }

    public void StopPlayMenu() {
        Destroy(currentOpenMenu);
    }

    public void CallServerImREady() {
        dmServerCall.CmdPlayerReady();
    }

    public void StartGamePhase() {
        StopShopMenu();
        StartPlayMenu();
    }
}