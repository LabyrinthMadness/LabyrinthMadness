using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float timeToDestroyIfNotHitAnything;
    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, timeToDestroyIfNotHitAnything);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag.Equals ("DogEnemy")) {
			collision.gameObject.GetComponent<DogEnemyStats> ().enemyLooseHealth(50);
		} else if (collision.gameObject.tag.Equals ("RhynoEnemy")) {
			collision.gameObject.GetComponent<RhynoEnemy> ().enemyLooseHealth(50);
		}
        Destroy(this.gameObject);
    }
}
