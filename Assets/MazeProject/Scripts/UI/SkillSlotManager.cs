using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotManager : MonoBehaviour {
    [Header("Ref")]
    public Image[] boughtItemIcons;
    public Sprite[] spriteForImage;
    public Text[] boughtItemNumberText;

   
    public GameObject menuBoxSkill;

    int[,] typeAndNumber = new int[4,2];

    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddNewEntry(int i) {
        i++;
        print("item id for : "+i);
        bool isFound = false;
        for(int ii = 0; ii < 4; ii++) {
            if(typeAndNumber[ii,0] == i) {
                isFound = true;
                typeAndNumber[ii, 1]++;
                print("add : " + typeAndNumber[ii, 1]+"   At : " + typeAndNumber[ii, 0]);
            }
        }

        if(!isFound) {
            for(int ii = 0; ii < 4; ii++) {
                if(typeAndNumber[ii, 0] == 0) {
                    typeAndNumber[ii, 0] = i;
                    typeAndNumber[ii, 1]++;
                    print("add new to list");
                    break;
                }
            }
        }

        ShowInventory();


    }

    void ShowInventory() {
        print("Show inventory");
        int xx = 3;
        for(int i = 0; i < 4; i++) {
            print("Show inventory check : " + typeAndNumber[i, 0]);
            if(typeAndNumber[i, 0] != 0) {
                boughtItemIcons[xx].gameObject.SetActive(true);
                boughtItemNumberText[xx].gameObject.SetActive(true);
                boughtItemIcons[xx].sprite = spriteForImage[typeAndNumber[i, 0] - 1];
                boughtItemNumberText[xx].text = typeAndNumber[i, 1].ToString();

                
            }
            xx--;
        }
    }
    
}