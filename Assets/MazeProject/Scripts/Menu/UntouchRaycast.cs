using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UntouchRaycast : MonoBehaviour {
    RaycastHit hit;
    Vector3 minimapPos;
    Vector3 normalizeDirection;
    DmPlayerManager dmPlayerManager;
    
    // Use this for initialization
    void Start() {

    }

    public void Initialize(Vector3 _minimapPos, DmPlayerManager _dmPlayerManager) {
        minimapPos = _minimapPos;
        dmPlayerManager = _dmPlayerManager;
    }


    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0))
            CallRayCastMouse();
    }

    void CallRayCastMouse() {
        print("Callraycast Mouse");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit)) {
            print("The name is : " + hit.collider.gameObject + "    his tag is : " + hit.collider.tag);
            if(hit.collider.tag == "MiniMap") {
                print("Find tag Minimap");
                normalizeDirection = hit.point - minimapPos;
                normalizeDirection.Normalize();
                print("Normalize vector : " + normalizeDirection);
                dmPlayerManager.CallRaycastOnSecondCamera(normalizeDirection);
            }
        }

    }
}