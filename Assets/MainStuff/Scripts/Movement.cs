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
        gameObject.tag = "Player";

        if(Legs==null){
            Legs =GameObject.FindGameObjectsWithTag("Legs");
        }
        
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
        Move();
        //Turn();
        if (m_JumpInputValue != 0)
        {
            jump();
        }
    }
    private void Move()
    {
        Vector3 direction = new Vector3(HorizontalInputValue, 0, 0).normalized;
        Vector3 turndir = new Vector3(0, 0, VerticalInputValue).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnspeedtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            currentEulerAngles += Vector3.forward * Time.deltaTime * m_Speed * 10;
            currentRotation.eulerAngles = currentEulerAngles;
            Player_Sphere.rotation = currentRotation;
            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            Vector3 Movement = movedir * m_Speed * Time.deltaTime;
            m_RigidBody.MovePosition(m_RigidBody.position + Movement);

        }
        else if (turndir.magnitude >= 0.1f)
        {
            float targetangle = Mathf.Atan2(turndir.x, turndir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetangle, ref turnsmoothvelocity, turnspeedtime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            currentEulerAngles += Vector3.forward * Time.deltaTime * m_Speed * 10;
            currentRotation.eulerAngles = currentEulerAngles;
            Player_Sphere.rotation = currentRotation;
            Vector3 movedir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            Vector3 Movement = movedir * m_Speed * Time.deltaTime;
            m_RigidBody.MovePosition(m_RigidBody.position + Movement);
        }

        //TODO : Roll the object towards te input directional value
        //For Example if user inputs value "W" the ball should roll towards that point while playing some animation

    }
    /*private void Turn()
    {
        float turn = VerticalInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_RigidBody.MoveRotation(m_RigidBody.rotation * turnRotation);

    }*/
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
