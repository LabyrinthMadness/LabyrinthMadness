using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string name;
    public bool active;
    public bool selected;
    public Sprite iconSelected;
    public Sprite iconUnselected;
    public Sprite iconInactive;

    public const string BULLET = "Bullet";
    public const string DASH = "Dash";
    public const string PUSH_FIELD = "Push Field";
    public const string BIG_SWORD = "Big Sword";

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (name.Equals(BULLET))
        {
            active = GameController.NumBullets > 0;
        }
        else if (name.Equals(DASH))
        {
            active = GameController.NumDash > 0;
        }
        else if (name.Equals(PUSH_FIELD))
        {
            active = GameController.NumPushField > 0;
        }
        else if (name.Equals(BIG_SWORD))
        {
            active = GameController.NumBigSword > 0;
        }

        if(!active)
        {
            spriteRenderer.sprite = iconInactive;
        }
        else
        {
            if (selected)
            {
                spriteRenderer.sprite = iconSelected;
            }
            else
            {
                spriteRenderer.sprite = iconUnselected;
            }
        }
    }
}
