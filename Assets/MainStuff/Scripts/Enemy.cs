using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    //public GameObject target;
    public Transform target;
    
    [SerializeField] private float speed;
    Rigidbody rg;
    Vector3 yAxis;
    //private Restart restart;
    private Vector3 m_NewForce;
    private Rigidbody m_RigidBody;
    //private string Playertag = "Player";
    //public Vector3 power;
    //private Movement movement;
    private Vector3 positionofPlayer;
    private bool Playeralive;


    
    private Vector3 Enemyjumpforce;
    //public GameObject Enemy;
  
    
    
    private Rigidbody Enemyrigidbody;
    //private Vector3 PlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        Enemyjumpforce = new Vector3(0, 200, 0);
        m_NewForce = (this.transform.position - target.position).normalized;
       
        
        yAxis = new Vector3(0, 1, 0);
    }
    private void Awake()
    {
        //Movement movement = gameObject.AddComponent<Movement>() as Movement;
        //Playeralive = this.gameObject.AddComponent<Movement>().isalive;
        //movement = gameObject.GetComponent<Movement>();
        rg = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {

        //m_NewForce = transform.InverseTransformPoint(target.transform.position);
        //angle = Mathf.Atan2(m_NewForce.x, m_NewForce.z) * Mathf.Rad2Deg;
        positionofPlayer = new Vector3(target.position.x,target.position.y,target.position.z);
        enemycode();       
    }
    public void enemycode()
    {
            Vector3 pos = Vector3.MoveTowards(this.transform.position, positionofPlayer, speed * Time.fixedDeltaTime);
            rg.MovePosition(pos);
            transform.LookAt(positionofPlayer);
            rg.constraints = RigidbodyConstraints.FreezeRotationZ;
            rg.constraints = RigidbodyConstraints.FreezeRotationX;
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            rg.AddForce(m_NewForce*200, ForceMode.Impulse);
            Debug.Log("I'm Hitting the Player");
            
        }

        if (other.gameObject.tag == "Jumper")
        {
            Debug.Log("Enemy jumped");
            rg.AddForce(Enemyjumpforce, ForceMode.Impulse);
        }
    }
}
