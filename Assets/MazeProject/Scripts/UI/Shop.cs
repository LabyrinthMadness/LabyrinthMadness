using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text cashText;
    public Text heartCounterText;
    public Text bulletCounterText;
    public Text dashCounterText;
    public Text pushFieldCounterText;
    public Text bigSwordCounterText;

    public Button heartButton;
    public Button bulletButton;
    public Button dashButton;
    public Button pushFieldButton;
    public Button bigSwordButton;
    public Button goButton;

    public Sprite buttonGoAppuyer;

    public HeroPlayerManager heroPlayerManager;

    // Constants
    private const int PRICE_HEART = 0;
    private const int PRICE_BULLET = 0;
    private const int PRICE_DASH = 0;
    private const int PRICE_PUSH_FIELD = 0;
    private const int PRICE_BIG_SWORD = 0;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Enable buttons
        heartButton.interactable = isNumItemsLessThanMax(GameController.NumHearts) && isEnoughMoneyForItem(PRICE_HEART);
        bulletButton.interactable = isNumItemsLessThanMax(GameController.NumBullets) && isEnoughMoneyForItem(PRICE_BULLET);
        dashButton.interactable = isNumItemsLessThanMax(GameController.NumDash) && isEnoughMoneyForItem(PRICE_DASH);
        pushFieldButton.interactable = isNumItemsLessThanMax(GameController.NumPushField) && isEnoughMoneyForItem(PRICE_PUSH_FIELD);
        bigSwordButton.interactable = isNumItemsLessThanMax(GameController.NumBigSword) && isEnoughMoneyForItem(PRICE_BIG_SWORD);

        // Set text
        cashText.text = "$: " + GameController.Cash.ToString();
        heartCounterText.text = GameController.NumHearts.ToString();
        bulletCounterText.text = GameController.NumBullets.ToString();
        dashCounterText.text = GameController.NumDash.ToString();
        pushFieldCounterText.text = GameController.NumPushField.ToString();
        bigSwordCounterText.text = GameController.NumBigSword.ToString();
    }

    // Button functions
    public void BuyHeart()
    {
        BuyItem(PRICE_HEART);
        ++GameController.NumHearts;
    }

    public void BuyBullet()
    {
        BuyItem(PRICE_BULLET);
        ++GameController.NumBullets;
    }

    public void BuyDash()
    {
        BuyItem(PRICE_DASH);
        ++GameController.NumDash;
    }

    public void BuyPushField()
    {
        BuyItem(PRICE_PUSH_FIELD);
        ++GameController.NumPushField;
    }

    public void BuyBigSword()
    {
        BuyItem(PRICE_BIG_SWORD);
        ++GameController.NumBigSword;
    }

    

    public void Go()
    {
        // Do stuff first

        heroPlayerManager.CallServerImReady();
        goButton.gameObject.GetComponent<Image>().sprite = buttonGoAppuyer;
        goButton.interactable = false;
    }

    private bool isNumItemsLessThanMax(int numItems)
    {
        return numItems < GameController.MAX_NUM_ITEMS;
    }

    private bool isEnoughMoneyForItem(int price)
    {
        return GameController.Cash >= price;
    }

    private void BuyItem(int price)
    {
        if(GameController.Cash >= price)
        {
            GameController.Cash -= price;
        }
    }
}
