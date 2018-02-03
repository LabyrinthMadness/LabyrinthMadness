using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInventory : MonoBehaviour {

    private RectTransform rectInventory;

    private float inventoryWidth, inventoryHeight;

    public int slots = 10;

    public int row = 2;

    public float slotPaddingLeft, slotPaddingTop;

    public float slotSize = 25;
    private int columns;
    public GameObject slotPrefabs;
    private List<GameObject> allSlots;


	// Use this for initialization
	void Start () {
        CreateLayout();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CreateLayout()
    {
        allSlots = new List<GameObject>();

        inventoryWidth = (slots / row) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
        inventoryHeight = row * (slotSize + slotPaddingTop) + slotPaddingTop;
        rectInventory = GetComponent<RectTransform>();
        rectInventory.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
        rectInventory.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryWidth);

        for(int x = 0; x < row; x++)
        {
            for(int y=0; y < columns; y++)
            {
                GameObject newSlot = (GameObject)Instantiate(slotPrefabs);

                RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                newSlot.name = "Slot";

                newSlot.transform.SetParent(this.transform.parent);

                slotRect.localPosition = rectInventory.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);
                slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                allSlots.Add(newSlot);
            }

        }
    }
}
