using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private Vector3 Playerjumpforce;
    private Vector3 Enemyjumpforce;
    public GameObject Enemy;
    public GameObject Player;

    private Rigidbody Mainrigidbody;
    private Rigidbody PlayerRigidbody;
    private Rigidbody Enemyrigidbody;


    // Start is called before the first frame update
    void Start()
    {
        Mainrigidbody = this.gameObject.GetComponent<Rigidbody>();
        PlayerRigidbody = Player.GetComponent<Rigidbody>();
        Enemyrigidbody = Enemy.GetComponent<Rigidbody>();

        Playerjumpforce = new Vector3(0, 500, 0);
        Enemyjumpforce = new Vector3(0, 100, 0);

    }

    // Update is called once per frame
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player)
        {
            Debug.Log("Player Jumped");
            PlayerRigidbody.AddForce(Playerjumpforce, ForceMode.Impulse);
        }
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy jumped");
            Enemyrigidbody.AddForce(Enemyjumpforce, ForceMode.Impulse);
        }
    }
}
