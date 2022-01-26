using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Start is called before the first frame update
    //public int m_PlayerNumber = 1;              // Used to identify the different players.
    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;
    //public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
    //public float m_MaxLaunchForce = 30f;
    //public float m_MaxChargeTime = 0.75f;
    private string m_FireButton;                // The input axis that is used for launching shells.
    //private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    //private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool m_Fired;
    private float nextFire = 0.5F;
    private float myTime = 0.5F;
    public float fireDelta = 0.5F;
    public Transform tpscam;
    public Vector3 offset;
    
    private void Start()
    {
        // The fire axis is based on the player number.
        m_FireButton = "Fire1";
        m_Fired = false;
        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        //m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    private void Update()
    {
        // The slider should have a default value of the minimum launch force.
        

        // If the max force has been exceeded and the shell hasn't yet been launched...
        /*if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {
            // ... use the max force and launch the shell.
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
        // Otherwise, if the fire button has just started being pressed...
        else if (Input.GetButtonDown(m_FireButton))
        {
            // ... reset the fired flag and reset the launch force.
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

           
        }
        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        else if (Input.GetButton(m_FireButton) && !m_Fired)
        {
            // Increment the launch force and update the slider.
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

          
        }
        */
        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        if (Input.GetButtonUp(m_FireButton) && !m_Fired)
        {
            // ... launch the shell.
            Fire();
            m_Fired = false;
        }
    }
    private void Fire()
    {
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;
        nextFire = myTime + fireDelta;
        // Create an instance of the shell and store a reference to it's awarigidbody.
        RaycastHit hit;
        if (Physics.Raycast(tpscam.position, tpscam.forward, out hit))
        {
            Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation,tpscam) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = tpscam.forward * 100;
            Destroy(shellInstance, 3);
        }
        // Change the clip to the firing clip and play it.
        nextFire = nextFire - myTime;
        myTime = 0.0F;
       
        // Reset the launch force.  This is a precaution in case of missing button events.
        //m_CurrentLaunchForce = m_MinLaunchForce;
    }
}

