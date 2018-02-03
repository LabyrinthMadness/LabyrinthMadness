using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IEndDragHandler
{
    [Header("mere")]
    public SkillSlotManager sks;
    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    #region IdropHandler implementation
    public void OnDrop(PointerEventData eventData)
    {
        print("OnDrop");
        DragAndDrop x = DragAndDrop.item.GetComponent<DragAndDrop>();
        if(!x.isPassif) {
            x.Buy();
            print(x.isPassif);
        } else
            return;
            
        
    }

    public void OnEndDrag(PointerEventData eventData) {
        print("Lol");
    }
    #endregion
}