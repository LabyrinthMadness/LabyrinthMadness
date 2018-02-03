using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{

    public float notHitAnythingTime;
	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, notHitAnythingTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag.Equals ("DogEnemy")) {
			collision.gameObject.GetComponent<DogEnemyStats> ().enemyLooseHealth(10);
			Destroy (this.gameObject);
		} else if (collision.gameObject.tag.Equals ("RhynoEnemy")) {
			collision.gameObject.GetComponent<RhynoEnemy> ().enemyLooseHealth(10);
			Destroy (this.gameObject);
		}
    }
}
