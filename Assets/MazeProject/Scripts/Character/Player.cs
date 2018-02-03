using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Public vars
    [Header("Movement")]
    public float moveSpeed;

    [Header("Coroutine Timings")]
    public float invincibilityTime;
    public float attackDelayTime;
    public float shootDelayTime;
    public float dashTime;
	public float slowTime;
    [Header("Coroutine Timings: Animation Delays")]
    public float animationDelayRunning;
    public float animationDelayAttacking;
	public float animationDelayShooting;
    [Header("Weapons")]
    public GameObject basicAttackHitBox;
    public GameObject bullet;
    public float bulletSpeed;
    [Header("Animation Arrays")]
    public Sprite idle;
    public Sprite[] running;
    public Sprite[] attacking;
	public Sprite[] shooting;
    [Header("Item Array")]
    public Item[] itemList;
    [Header("Audio")]
    public AudioClip macheteWhooshSfx;
    public AudioClip macheteStrikeSfx;
	public AudioClip gunShot;

    // Movement Player vars
    Vector3 moveVect;
    private Vector3 directionVector;
	float speedFactorDash;
	float speedFactorSlow;

    // Animation vars
    private enum AnimationState
    {
        IDLE,
        RUNNING
    }
    private AnimationState animationState;
    private AnimationState lastAnimationState;
    private SpriteRenderer spriteRenderer;

    // Constants
    private const string FIRE_1 = "Fire1";
    private const string FIRE_2 = "Fire2";
    private const string FIRE_3 = "Fire3";
    private const string FIRE_4 = "Fire4";
    private const float DASH_SPEED_FACTOR = 2.0f;
	private const float SLOW_SPEED_FACTOR = 0.5f;

    // Items
    
    private int equippedItemIndex; 

    // Bounds
    private float halfWidth;
    private float halfHeight;

    // Flags
    private bool canAttack;
    private bool canShoot;
    private bool invincible;
	private bool dash;
    private bool slow;
    private bool rtPressed;
    private bool ltPressed;

	// Flag getters
	public bool Invincible{get;}

    // Audio vars
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        // <TestCode>
        GameController.NumBullets = 1;
        GameController.NumDash = 1;
        GameController.NumPushField = 1;
        GameController.NumBigSword = 1;
        // </TestCode>

        moveVect = Vector3.right;
        directionVector = Vector3.right;
        speedFactorDash = 1.0f;
		speedFactorSlow = 1.0f;

        animationState = AnimationState.IDLE;
        lastAnimationState = animationState;
        spriteRenderer = GetComponent<SpriteRenderer>();

        equippedItemIndex = 0;

        halfWidth  = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        halfHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        canAttack = true;
        canShoot = true;
        invincible = false;
		dash = false;
        slow = false;
        rtPressed = false;
        ltPressed = false;

        audioSource = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
		print (GameController.NumHearts);
        UpdateItemList();

        if (Input.GetButtonDown(FIRE_3))
        {
            switchItem(FIRE_3);
        }
        else if(Input.GetButtonDown(FIRE_4))
        {
            switchItem(FIRE_4);
        }
        
        if (Input.GetButtonDown(FIRE_1))
        {
            if (canAttack)
            {
                BasicAttack();
                StartCoroutine(DisableAttack(attackDelayTime));
                StartCoroutine(AttackAnimation());
            }
        }
        else if (Input.GetButtonDown(FIRE_2))
        {
            string equippedItem = itemList[equippedItemIndex].name;
            if(equippedItem.Equals(Item.BULLET))
            {
                if(GameController.NumBullets > 0 && canShoot)
                {
                    Shoot();
                    StartCoroutine(DisableShoot(shootDelayTime));
					StartCoroutine(ShootAnimation());
                    //GameController.NumBullets--;
                    // Disable attackingg for some amount of time
                    // Have shoot animation
                }
            }
            else if(equippedItem.Equals(Item.DASH))
            {
                if(GameController.NumDash > 0)
                {
                    StartCoroutine(ExecuteDash(dashTime));
                    //StartCoroutine(DisableAttack(dashTime));
                    StartCoroutine(Invincibility(dashTime));
                    //GameController.NumDash--;
                }
                
            }
            else if (equippedItem.Equals(Item.PUSH_FIELD))
            {
                Debug.Log(Item.PUSH_FIELD);
                if(GameController.NumPushField > 0)
                {
                    //GameController.NumPushField--;
                }
            }
            else if (equippedItem.Equals(Item.BIG_SWORD))
            {
                Debug.Log(Item.BIG_SWORD);
                if(GameController.NumBigSword > 0)
                {
                    //GameController.NumBigSword--;
                }
            }
            //StartCoroutine(DisableAttack(attackDelayTime));
        }
        Animate(lastAnimationState);

        lastAnimationState = animationState;
    }
    
    void FixedUpdate()
    {
        if(!dash)
        {
            moveVect = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
            if(moveVect.magnitude > 0.0f)
            {
                animationState = AnimationState.RUNNING;
            }
            else
            {
                animationState = AnimationState.IDLE;
            }
        }
        FlipSprite();
        Vector3 finalTranslateVector = speedFactorSlow * speedFactorDash * moveSpeed * Time.deltaTime * moveVect;
        this.transform.Translate(finalTranslateVector);
        directionVector = moveVect.normalized;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If object is damaging
        // GameController.NumHearts -= damage of object
        //if(canAttack)
        //{
        //    StartCoroutine(DisableAttack(invincibilityTime));
        //}

//        if (!invincible)
//        {
//            StartCoroutine(Invincibility(invincibilityTime));
//        }
    }

    private void BasicAttack()
    {
        Instantiate(basicAttackHitBox, FrontOfPlayer(), Quaternion.identity);
        audioSource.PlayOneShot(macheteWhooshSfx);
        // if hit -> play macheteStrikeSfx // TODO: conditionally play macheteStrikeSfx instead
    }

    private void Shoot()
    {
		audioSource.PlayOneShot (gunShot);
        GameObject firedBullet = Instantiate(bullet, FrontOfPlayer(), Quaternion.identity) as GameObject;
        Vector2 bulletDir = (spriteRenderer.flipX) ? Vector2.right : Vector2.left;
        firedBullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * bulletDir);
    }

    private void switchItem(string triggerName) 
    {
        if (triggerName.Equals(FIRE_3))
        {
            equippedItemIndex = (equippedItemIndex + 1) % itemList.Length;
            for (int i = equippedItemIndex; i < itemList.Length; i = (i + 1) % itemList.Length)
            {
                if (itemList[i].active || i == equippedItemIndex)
                {
                    equippedItemIndex = i;
                    break;
                }
            }

        } else if (triggerName.Equals(FIRE_4))
        {
            equippedItemIndex = (equippedItemIndex == 0) ? itemList.Length - 1 : equippedItemIndex - 1;
            for (int i = equippedItemIndex; i >= 0; --i)
            {
                if (itemList[i].active || i == equippedItemIndex)
                {
                    equippedItemIndex = i;
                    break;
                }
            }
        }
    }

    private void UpdateItemList()
    {
        for(int i = 0; i < itemList.Length; ++i)
        {
			itemList[i].selected = (equippedItemIndex == i);
        }
    }

    private void FlipSprite()
    {
        if(Mathf.Abs(directionVector.x) > 0.0f)
        {
            spriteRenderer.flipX = directionVector.x > 0.0f;
        }
    }

    private Vector3 FrontOfPlayer()
    {
        return transform.position + ((!spriteRenderer.flipX) ? Vector3.left * halfWidth : Vector3.right * halfWidth);
    }

    private void Animate(AnimationState lastAnimationState)
    {
        if (animationState == lastAnimationState)
        {
            return;
        }
        switch(animationState)
        {
            case AnimationState.RUNNING:
                StopCoroutine(AttackAnimation());
                StartCoroutine(RunAnimation());
                break;

            default: // Idle by default
                StopCoroutine(RunAnimation());
                StopCoroutine(AttackAnimation());
                spriteRenderer.sprite = idle;
                break;
        }
    }

    private IEnumerator DisableAttack(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    private IEnumerator DisableShoot(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

	public void DamagePlayer(int numDamage)
	{
		if (!invincible) {
			if (GameController.NumHearts > 0) {
				GameController.NumHearts -= numDamage;
				StartCoroutine (Invincibility (invincibilityTime));
			} else {
				Destroy (this.gameObject);
			}
		}

	}

    private IEnumerator Invincibility(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    private IEnumerator ExecuteDash(float time)
    {
        speedFactorDash = DASH_SPEED_FACTOR;
        dash = true;
        yield return new WaitForSeconds(time);
        dash = false;
        speedFactorDash = 1.0f;
    }

	public void ExecuteSlowRoutine()
	{
		if (!slow) 
		{
			StartCoroutine (SlowRoutine (slowTime));
		}
	}

	private IEnumerator SlowRoutine(float time)
	{
		speedFactorSlow = SLOW_SPEED_FACTOR;
		slow = true;
		yield return new WaitForSeconds(time);
		slow = false;
		speedFactorSlow = 1.0f;
	}

    private IEnumerator AttackAnimation()
    {
        return Animation(attacking, animationDelayAttacking);
    }

	private IEnumerator ShootAnimation()
	{
		return Animation(shooting, animationDelayShooting);
	}

    private IEnumerator RunAnimation()
    {
        return LoopAnimation(AnimationState.RUNNING, running, animationDelayRunning);
    }

    private IEnumerator LoopAnimation(AnimationState animState, Sprite[] animationFrames, float delay)
    {
        int frame = 0;
        while (animationState == animState)
        {
            spriteRenderer.sprite = animationFrames[frame];
            frame = (frame + 1) % animationFrames.Length;
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator Animation(Sprite[] animationFrames, float delay)
    {
        int frame = 0;
        while (frame < animationFrames.Length)
        {
            spriteRenderer.sprite = animationFrames[frame];
            ++frame;
            yield return new WaitForSeconds(delay);
        }
        spriteRenderer.sprite = idle;
    }


}
