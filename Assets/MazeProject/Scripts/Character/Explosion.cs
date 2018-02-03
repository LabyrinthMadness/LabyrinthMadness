using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour 
{
	public float animationTimeDelay;
	public Sprite[] animationSpriteList;
	private SpriteRenderer spriteRenderer;
	private bool hasInflictedDamage;

	// Use this for initialization
	void Start () 
	{
		hasInflictedDamage = false;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		StartCoroutine (PlayAnimation (animationTimeDelay));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator PlayAnimation(float delayTime)
	{
		int frame = 0;
		while (frame < animationSpriteList.Length) 
		{
			spriteRenderer.sprite = animationSpriteList [frame++];
			yield return new WaitForSeconds(delayTime);
		}
		Destroy (this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!hasInflictedDamage) {
			if (collision.gameObject.tag.Equals ("Player")) {
				collision.gameObject.GetComponent<Player> ().DamagePlayer (3);
				hasInflictedDamage = true;
			}
		}
	}

}
