using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapIPointer : MonoBehaviour, IPointerClickHandler {

    DmPlayerManager dmPlayerManager;

    public void Initialize(DmPlayerManager _dmPlayerManager) {
        dmPlayerManager = _dmPlayerManager;
    }

    public void OnPointerClick(PointerEventData eventData) {
        print("Event data position : "+eventData.position);
        if(dmPlayerManager != null)
            SendToDmPlayerManager(eventData.position);
    }

    void SendToDmPlayerManager(Vector3 vect) {
        dmPlayerManager.CallRaycastOnSecondCamera(vect);
    }
}
