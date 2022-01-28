using System.Security.Cryptography;
using System.IO;
using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float m_Speed;
    //public float m_TurnSpeed = 180f;
    private string HorizontalAxisName;
    private string VerticalAxisName;
    private string m_JumpAxis;
    private float HorizontalInputValue;
    private float VerticalInputValue;
    private float m_JumpInputValue;
    private Rigidbody m_RigidBody;
    Vector3 m_NewForce;
    [SerializeField] private float m_ForceY;
    private bool isgrounded = true;
    public int health = 100;
    //private Restart restart;
    public GameObject Player;
    public Transform cam;
    private float turnspeedtime;
    float turnsmoothvelocity;
    //private CharacterController controller;
    public bool isalive = true;
    private Vector3 Playerjumpforce;
    public Slider m_Slider;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public Image m_FillImage;
    private float m_CurrentHealth;
    public Transform Player_Sphere;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;
    public GameObject[] Legs = new GameObject[4];
    private void Awake()
    {
        health = 100;
        m_CurrentHealth = health;
        turnspeedtime = 0.1f;
        m_RigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        m_ForceY = 1000f;
        m_Speed = 30f;

        isalive = true;
        //controller = gameObject.AddComponent<CharacterController>();
        m_NewForce = new Vector3(-5.0f, 1.0f, 0.0f);
        HorizontalAxisName = "Horizontal";
        VerticalAxisName = "Vertical";
        m_JumpAxis = "Jump";
        currentEulerAngles = Player_Sphere.transform.eulerAngles;
        gameObject.tag = "Player";

        
            Legs =GameObject.FindGameObjectsWithTag("Legs");
        
        
        foreach(GameObject ILegs in Legs)
        {
                ILegs.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }
        
    }
    /* IEnumerator LegsClosing(int AnimTime)
    {
        int Time_2_0 = 0;
        Time_2_0 = (int)Time.deltaTime;
        while(Time_2_0!=AnimTime)
        {
            foreach(GameObject ILegs in Legs)
            {
                ILegs.GetComponent<Transform>().localScale = new Vector3(0.1f,0.1f,0.1f);
                yield return WaitWhile;
            }
        }
    } */
    private void Update()
    {
        HorizontalInputValue = Input.GetAxis(HorizontalAxisName);
        VerticalInputValue = Input.GetAxis(VerticalAxisName);
        m_JumpInputValue = Input.GetAxis(m_JumpAxis);
        //Move();
        m_NewForce = new Vector3(0, m_ForceY, 0);

    }
    private void OnEnable()
    {
        //m_RigidBody.isKinematic = false;
        HorizontalInputValue = 0f;
        VerticalInputValue = 0f;
    }

    private void FixedUpdate()
    {
        VerticalAxis_Movement();
        HorizontalAxis_Movement();
        rotating_Towards_Camera();
        if (m_JumpInputValue != 0)
        {
            jump();
        }
    }
    private void rotating_Towards_Camera()
    {
        Player_Sphere.transform.eulerAngles = new Vector3(Player_Sphere.transform.eulerAngles.x,cam.eulerAngles.y,Player_Sphere.transform.eulerAngles.z);
    }
    private void VerticalAxis_Movement()
    {
        Vector3 direction = new Vector3(0, 0, VerticalInputValue).normalized;
        Debug.Log(direction);
        if (direction.magnitude > 0f){
            m_RigidBody.MovePosition(m_RigidBody.position + Movements(direction));
        }
        else if (direction.magnitude < 0f){
            m_RigidBody.MovePosition(m_RigidBody.position + Movements(direction));
        }
        else if (direction.magnitude == 0f){
            m_RigidBody.MovePosition(m_RigidBody.position + Movements(direction));
        }

        //TODO : Roll the object towards te input directional value
        //For Example if user inputs value "W" the ball should roll towards that point while playing some animation

    }
    private void HorizontalAxis_Movement()
    {
        Vector3 turndir = new Vector3(HorizontalInputValue, 0, 0).normalized;
        Debug.Log(turndir);
        
        if (turndir.magnitude > 0f){
            m_RigidBody.MovePosition(m_RigidBody.position + Movements(turndir));
        }
        else if (turndir.magnitude < 0f){
            m_RigidBody.MovePosition(m_RigidBody.position + Movements(turndir));
        }
        else if (turndir.magnitude == 0){
            m_RigidBody.MovePosition(m_RigidBody.position + Movements(turndir));
        }
    }
    public Vector3 Movements(Vector3 InputAxis)
    {
        Vector3 movingdirection = InputAxis;
        if(InputAxis.magnitude==0)
        {
            movingdirection = Vector3.zero;
        }
        else 
        {
            m_RigidBody.AddForce(movingdirection*m_Speed, ForceMode.Acceleration);
        }
        return movingdirection;

    }
    private void jump()
    {
        if (isgrounded == true)
        {

            // LayerMask mask = LayerMask.GetMask("Ground");
            m_RigidBody.AddForce(m_NewForce, ForceMode.Impulse);
            isgrounded = false;
            Debug.Log("I'm am jumping");

        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Ground")
        {
            isgrounded = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LavaCube")
        {
            m_RigidBody.AddForce(new Vector3(0, 1500f, 0), ForceMode.Impulse);
            Playerhealth();
        }
        if (other.gameObject.tag == "Enemy")
        {
            Playerhealth();
        }
    }

    public float Playerhealth()
    {
        health = health - 5;
        Debug.Log("The Enemy is hitting me");
        SetHealthUI();
        if (health < 0)
        {
            ondeath();
            return health;
            //Destroy(Player);

            //Time.timeScale = 0.5f;
            //Destroy(gameObject, 2);
        }
        else
        {
            return health;
        }

    }
    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = health;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / health);
    }
    private void ondeath()
    {
        isalive = false;
        SceneManager.LoadScene("Dead");
    }
}
