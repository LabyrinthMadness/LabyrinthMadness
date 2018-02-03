using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Constants
    private const int START_NUM_HEARTS = 3;
    public const int MAX_NUM_ITEMS = 99;

    // Variables
    private static int numHearts;
    private static int numBullets;
    private static int numDash;
    private static int numPushField;
    private static int numBigSword;
    private static int cash;
    private static bool gameOver;

    // Getters and setters
    public static int NumHearts
    {
        get { return numHearts; }
        set
        {
            numHearts = value;
            if (numHearts < 0)
            {
                numHearts = 0;
            }
            else if (numHearts > MAX_NUM_ITEMS)
            {
                numHearts = MAX_NUM_ITEMS;
            }
        }
    }

    public static int NumBullets
    {
        get { return numBullets; }
        set
        {
            numBullets = value;
            if (numBullets < 0)
            {
                numBullets = 0;
            }
            else if (numBullets > MAX_NUM_ITEMS)
            {
                numBullets = MAX_NUM_ITEMS;
            }
        }
    }

    public static int NumDash
    {
        get { return numDash; }
        set
        {
            numDash = value;
            if (numDash < 0)
            {
                numDash = 0;
            }
            else if (numDash > MAX_NUM_ITEMS)
            {
                numDash = MAX_NUM_ITEMS;
            }
        }
    }

    public static int NumPushField
    {
        get { return numPushField; }
        set
        {
            numPushField = value;
            if (numPushField < 0)
            {
                numPushField = 0;
            }
            else if (numPushField > MAX_NUM_ITEMS)
            {
                numPushField = MAX_NUM_ITEMS;
            }
        }
    }

    public static int NumBigSword
    {
        get { return numBigSword; }
        set
        {
            numBigSword = value;
            if(numBigSword < 0)
            {
                numBigSword = 0;
            }
            else if(numBigSword > MAX_NUM_ITEMS)
            {
                numBigSword = MAX_NUM_ITEMS;
            }
        }
    }

    public static int Cash
    {
        get { return cash; }
        set
        {
            cash = value;
            if (cash <= 0)
            {
                cash = 0;
            }
        }
    }

    public static bool GameOver
    {
        get { return gameOver; }
    }

	// Use this for initialization
	void Start ()
    {
        numHearts = START_NUM_HEARTS;
        numBullets = 0;
        numDash = 0;
        numPushField = 0;
        numBigSword = 0;
        cash = 0;
        gameOver = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckGameEnd();
        // Other stuff here.
	}

    private void CheckGameEnd()
    {
        gameOver = numHearts <= 0;
        if(gameOver)
        {
            // Do game over stuff here
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
