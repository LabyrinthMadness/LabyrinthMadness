using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAI : MonoBehaviour {

    Animator anim;
    public GameObject player;

    public GameObject GetPlayer()
    {
        return player;
    }

    public void StopAttacking()
    {
        CancelInvoke("Attack");
    }

    public void StartAttack()
    {
        InvokeRepeating("Attack", 0.5f, 0.5f);
    }

    public void Attack()
    {

    }
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("DistanceToPlayer", Vector2.Distance(transform.position, player.transform.position));
	}
}
