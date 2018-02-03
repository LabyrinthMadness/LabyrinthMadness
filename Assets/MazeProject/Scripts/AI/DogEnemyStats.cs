using System.Collections;
using UnityEngine;

public class DogEnemyStats : MonoBehaviour
{
    private Rigidbody2D r2d;
    public GameObject enemy;
    private GameObject player;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;
    public float health;
    private float maxHealth = 100.0f;
    private float distance;
    private float distanceToChase = 20.0f;
    private float distanceToAttack = 1.0f;
    private float angleRot;
    private SpriteRenderer sprRenderer;
    public bool canMove = true;

    private int frame = 0;
    private float deltaTime = 0;
    bool loop = true;
    public float frameSeconds = 1;
    
    public Sprite[] running;
    Vector2 vectDir;
    // Use this for initialization
    private void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
		print (player);
        r2d = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        vectDir = player.transform.position - this.transform.position;
        deltaTime += Time.deltaTime;
        distance = Vector2.Distance(player.transform.position, this.transform.position);
        angleRot = Vector2.Angle(player.transform.position, this.transform.position);

        if (distance < distanceToChase)
        {
            if (canMove)
            {
                transform.LookAt(enemy.transform);
                transform.Rotate(this.transform.localRotation.eulerAngles, angleRot);
                //enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(player.transform.position), rotSpeed * Time.deltaTime);

                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }

        //flip enemy
        //if (angleRot < 90)
        //{
        //    sprRenderer.flipX = true;
        //}
        FlipSprite();
        //enemy enter range of attack
        if (distance < distanceToAttack)
        {
			print ("Dog HERE!");
            StartAttack();
        }
        else
        {
            CancelInvoke("Attack");
        }

        //if enemy is dead
        if (health <= 0)
        {
            enemyDead();
        }


        RunningAnimation();
    }

    //kill the enemy
    public void enemyDead()
    {
        Destroy(this.gameObject);
    }

    //attack enemy with timer
    public void StartAttack()
    {
		print ("Dog here!");
        InvokeRepeating("Attack", 0.5f, 0.5f);
    }

    //put loosing health of the player
    public void Attack()
    {
		player.GetComponent<Player> ().DamagePlayer (1);
    }

    //make the enemy bleed
    public void enemyLooseHealth(float value)
    {
        if (health > 0)
        {
            health -= value;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            canMove = false;
            loop = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canMove = true;
            loop = true;
        }
    }

    public void RunningAnimation()
    {
        while (deltaTime >= frameSeconds)
        {
            deltaTime -= frameSeconds;
            frame++;
            if (loop)
                frame %= running.Length;
            //Max limit
            else if (frame >= running.Length)
                frame = running.Length - 1;
        }
        sprRenderer.sprite = running[frame];
    }

    private void FlipSprite()
    {
        if (Mathf.Abs(vectDir.x) > 0.0f)
        {
            sprRenderer.flipX = vectDir.x > 0.0f;
        }
    }
}