using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeOfDeath : MonoBehaviour
{
    //private Movement movement;
    private Rigidbody PlayerRigidbody;
    private Vector3 JumpForce;
    public GameObject Player;
    private Rigidbody m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {

        //movement = gameObject.GetComponent<Movement>();
        //movement = AddComponent<Movement>();
        PlayerRigidbody = Player.GetComponent<Rigidbody>();
        JumpForce = new Vector3(0, 100, 0);
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        //rg.constraints = RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player)
        {
            Debug.Log("Player Jumped");
            PlayerRigidbody.AddForce(JumpForce, ForceMode.Impulse);
            this.gameObject.GetComponent<Movement>().Playerhealth();

        }
    }
}
