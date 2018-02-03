using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnDog : MonoBehaviour {

    public DogEnemyStats dog;
    public GameObject objDog;
    
    public float spawnTime = 5.0f;

    private void Start()
    {
        InvokeRepeating("SpawnDog",spawnTime,spawnTime);
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void SpawnDog()
    {       
        Instantiate(objDog, this.transform.position, this.transform.rotation);
    }


}
