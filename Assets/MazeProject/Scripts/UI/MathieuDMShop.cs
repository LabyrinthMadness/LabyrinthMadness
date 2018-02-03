using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathieuDMShop : MonoBehaviour {
    [Header("Sprite Passif")]
    public Sprite[] passifSprites;
    public string[] passifTitles;
    public string[] passifDescriptifs;
    public int[] passifPrices;

    [Header("Sprite Actif")]
    public Sprite[] actifSprites;
    public string[] actifTitles;
    public string[] actifDescriptifs;
    public int[] actifPricess;

    [Header("Reference")]
    public Image passifImageUI;
    public Text passifTitleUI;
    public Text passifDescriptifUI;
    public Image actifImageUI;
    public Text actifTitleUI;
    public Text actifDescriptifUI;
    public Text overallCash;

    [Header("Settings ")]
    public SkillSlotManager sks;
    public int goldHolding;
    public bool debugRaycast;
    public Button okbuton;

    [Header("Menu To Hide")]
    public GameObject[] alluiItem;
    public GameObject[] showuiItem;

    int passifInventoryIndex = 0;
    int actifInventoryIndex = 0;
    public DmPlayerManager dmPlayerManager;
    //Buying Stats
    int prIndex = 0;
    int[,] passifRegistery = new int[30, 2];
	// Use this for initialization
	void Start () {
        ShowCash();

        //Default affiahcge 
        actifImageUI.sprite = actifSprites[actifInventoryIndex];
        actifTitleUI.text = actifTitles[actifInventoryIndex];
        actifDescriptifUI.text = actifDescriptifs[actifInventoryIndex];
        passifImageUI.sprite = passifSprites[passifInventoryIndex];
        passifTitleUI.text = passifTitles[passifInventoryIndex];
        passifDescriptifUI.text = passifDescriptifs[passifInventoryIndex];
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0) && debugRaycast) 
        TEST(); 
    }

    public void PassifChangeUI(bool b) {
        ChangePassifIndex(b);

        passifImageUI.sprite = passifSprites[passifInventoryIndex];
        passifTitleUI.text = passifTitles[passifInventoryIndex];
        passifDescriptifUI.text = passifDescriptifs[passifInventoryIndex]; 
    }

    void ChangePassifIndex(bool b) {
        if(b) {
            if(passifInventoryIndex == 3)
                passifInventoryIndex = 0;
            else
                passifInventoryIndex++;
        } else {
            if(passifInventoryIndex == 0)
                passifInventoryIndex = 3;
            else
                passifInventoryIndex--;
        }
    }

    public void ActifChangeUI(bool b) {
        ChangeActifIndex(b);

        actifImageUI.sprite = actifSprites[actifInventoryIndex];
        actifTitleUI.text = actifTitles[actifInventoryIndex];
        actifDescriptifUI.text = actifDescriptifs[actifInventoryIndex];
    }

    void ChangeActifIndex(bool b) {
        if(b) {
            if(actifInventoryIndex == 3)
                actifInventoryIndex = 0;
            else
                actifInventoryIndex++;
        } else {
            if(actifInventoryIndex == 0)
                actifInventoryIndex = 3;
            else
                actifInventoryIndex--;
        }
    }

    public bool CanHePay(bool b) {
        if(!b) {
            if(goldHolding >= passifPrices[passifInventoryIndex])
                return true;
            else
                return false;
        } else {
            if(goldHolding >= actifPricess[actifInventoryIndex])
                return true;
            else
                return false;
        }
    }

    public void MakePurchase(bool b) {
        print("Make Purchase");
        print("Gold : "+goldHolding+"   price of rhine 1  : "+ passifPrices[passifInventoryIndex]);
        if(!b) {
            goldHolding = goldHolding-passifPrices[passifInventoryIndex];
        }else
            goldHolding = goldHolding-actifPricess[actifInventoryIndex];

        print("Gold left : " + goldHolding);
        ShowCash();
    }

    public void UpdateSks() {
        sks.AddNewEntry(passifInventoryIndex);
    }

    void ShowCash() {
        overallCash.text = goldHolding.ToString();
    }

    public void ActifObjectIsBeingDrop(Vector3 _pos) {
        print("Prepare for RayCast : "+_pos);

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_pos);

        if(Physics.Raycast(ray, out hit)) {
            if(hit.collider != null) {
                print("Raycast  TRUE _ name  : " + hit.transform.parent.gameObject + "    tag name  : " + hit.transform.parent.tag);
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 3f);
                if(hit.transform.parent.tag.Equals("LevelFloor")) {
                    TilesClass tc = hit.transform.parent.GetComponent<TilesClass>();
                    if(tc != null && tc.isOccupied == false) {
                        print("It works ichh ; " + hit.collider.gameObject);
                        tc.isOccupied = true;
                        passifRegistery[prIndex, 0] = tc.id;
                        passifRegistery[prIndex, 1] = passifInventoryIndex;
                        prIndex++;
                        //Instantiate sprite to see your defence

                        MakePurchase(false);
                    } else{
                        print("Spot is already Taken");
                        }
                }
            }
        }
    }

    void TEST() {
        print("Test : "+Input.mousePosition);
        RaycastHit hit;
        


        Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 vect3d1 = ray.origin;
        Vector2 vect2d1 = new Vector2(vect3d1.x, vect3d1.y);
        Vector3 vect3d2 = ray.direction;
        Vector2 vect2d2 = new Vector2(vect3d2.x, vect3d2.y);
        Ray2D ray2D = new Ray2D(vect2d1, vect2d2); 


        // hit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //hit2D = Physics2D.Raycast(vect2d1, vect2d2, Mathf.Infinity);
        if(Physics.Raycast(ray, out hit)) {
            if(hit.collider != null) {
                print("Raycast  TRUE _ name  : " + hit.transform.parent.gameObject + "    tag name  : " + hit.transform.parent.tag);
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red, 3f);
                if(hit.transform.parent.tag.Equals("LevelFloor")) {
                    print("It works ichh ; " + hit.collider.gameObject);
                }
            }
        }
        
    }

    public void CallServerReady() {
        okbuton.interactable= false;
        dmPlayerManager.CallServerImREady();
    }

    public void SetSecondPhase() {
        foreach(GameObject go in alluiItem) {
            go.SetActive(false);
        }
        foreach(GameObject go in showuiItem) {
            go.SetActive(true);
        }
    }
}