using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 4f;
    Rigidbody rg;
    Vector3 yAxis;
    private Restart restart;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        yAxis = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemycode();       
    }
    public void enemycode()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rg.MovePosition(pos);
        transform.LookAt(target);
        rg.constraints = RigidbodyConstraints.FreezeRotationZ;
        rg.constraints = RigidbodyConstraints.FreezeRotationX;
        Debug.Log("i am moving towars character");

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            {
            Destroy(target);
            restart.RestartGame();
            }
    }
}
