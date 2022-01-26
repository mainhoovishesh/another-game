using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;
    // Start is called before the first frame update
    public Transform spawnTransform;
    private int startSpawnTime = 5;
    private int spawnTime = 3;
    private int numberofEnemies = 1;
    //private Enemy enemy;

    private Vector3 positionOfTransform;
    private void Start()
    {
        startSpawnTime = 10;
        spawnTime = 5;
        numberofEnemies = 1;
        //enemy = gameObject.GetComponent<Enemy>();
        positionOfTransform = new Vector3(spawnTransform.position.x, spawnTransform.position.y, spawnTransform.position.z);
        InvokeRepeating("Spawn", startSpawnTime, spawnTime);
    }
    // Update is called once per frame
    void Spawn()
    {
        if (numberofEnemies < 2)
        {
            Debug.Log("Enemy Created");
            numberofEnemies = numberofEnemies + 1;
            Instantiate(Enemy, positionOfTransform, this.transform.rotation);
            //this.gameObject.GetComponent<Enemy>().enemycode();

        }
    }
}
