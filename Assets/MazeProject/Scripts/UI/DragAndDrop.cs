using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject item;    // i changed itembeigdraged to item.
    public static int type;

    public bool isPassif;
    public MathieuDMShop mathieuDmShop;

    Vector2 initialPosition;
    Transform startParent;
    Vector3 startPosition;
    bool start = true;
    bool dragging = false;
    //Sprite sprite;

    bool isableToDrag;
    bool isInTrigger = false;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!mathieuDmShop.CanHePay(!isPassif)) {
            isableToDrag = false;
            return;
        } else isableToDrag = true;

        initialPosition = gameObject.transform.position;
        dragging = true;
        item = gameObject;
        startPosition = transform.position;
        //startParent = transform.parent;

        //GetComponent<CanvasGroup>().blocksRaycasts = false;
        //item.GetComponent<LayoutElement>().ignoreLayout = true;
       // item.transform.SetParent(item.transform.parent.parent);
        
    }


    public void OnDrag(PointerEventData eventData)
    {
        if(!isableToDrag)
            return;

        if (dragging)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(isPassif) {
            print("END DRAG");
            if(isInTrigger) {
                mathieuDmShop.UpdateSks();
                mathieuDmShop.MakePurchase(true);
            }
        }

        if(!isableToDrag)
            return;

        item = null;

        if (transform.parent == startParent)
        {
            transform.position = startPosition;
            Debug.Log("item in slot");
        }
        //GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.position = initialPosition;
        //item.GetComponent<LayoutElement>().ignoreLayout = false;
        if(!isPassif) {
            //GetTouch placement 
            
            mathieuDmShop.ActifObjectIsBeingDrop(eventData.position);
        }
    }

    public void ResetPosition()
    {
        
    }

    public void PointerExit(BaseEventData data)
    {
        data.Reset();
    }

    public void Buy() {
        bool b;
        if(isPassif)
            b = false;
        else
            b = true;

        mathieuDmShop.MakePurchase(b);
    }


    void OnTriggerEnter(Collider col) {
        isInTrigger = true;
    }
    void OnTriggerExit(Collider col) {
        isInTrigger = false;
    }
}




