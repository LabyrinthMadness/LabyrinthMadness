using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMBaseDog : StateMachineBehaviour {

    protected GameObject enemy;
    protected GameObject player;
    public float speed = 2.0f;
    public float rotSpeed = 1.0f;
    public float accuracy = 3.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        enemy = animator.gameObject;
        player = player.GetComponent<DogAI>().GetPlayer();
    }
}
