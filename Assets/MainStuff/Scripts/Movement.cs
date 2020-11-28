using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private Rigidbody m_RigidBody;
    

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }
    public void Start()
    {
        m_MovementAxisName = "Vertical" ;
        m_TurnAxisName = "Horizontal" ;
    }
    public void Update()
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
    }
    private void OnEnable()
    {
        m_RigidBody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }
    private void OnDisable()
    {
        m_RigidBody.isKinematic = true;
    }
    public void FixedUpdate()
    {
        Move();
        Turn();
    }
    public void Move()
    {
        Vector3 Movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_RigidBody.MovePosition(m_RigidBody.position + Movement);
    }
    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_RigidBody.MoveRotation(m_RigidBody.rotation * turnRotation);
    }
}
