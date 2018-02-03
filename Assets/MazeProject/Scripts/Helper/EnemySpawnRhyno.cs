using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnRhyno : MonoBehaviour {

    public RhynoEnemy dog;
    public GameObject objRhyno;

    public float spawnTime = 5.0f;

    private void Start()
    {
        InvokeRepeating("SpawnRhyno", spawnTime, spawnTime);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnRhyno()
    {
        Instantiate(objRhyno, this.transform.position, this.transform.rotation);
    }
}
