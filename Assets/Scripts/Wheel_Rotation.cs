using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel_Rotation : MonoBehaviour
{
    public float rotation_Speed;
    [Range(1,5)]
    public float Tire_Size;
    public float y_rotation_tyre_time;
    
    //public GameObject wheels;
    // Start is called before the first frame update
    void Start()
    {
        
        //wheels.AddComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.Rotate(rotation_Speed, 0.0f, 0.0f, Space.Self);
        }
        if(Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.Rotate(-rotation_Speed, 0.0f, 0.0f, Space.Self);
        }
        if(Input.GetKey(KeyCode.A))
        {
            Debug.Log("Front wheels");
            if(this.gameObject.tag == "Front_Wheels")
            {
                            Debug.Log("Front wheels_2");

                 this.gameObject.transform.Rotate(0.0f,Mathf.Lerp(0.0f,25.0f,y_rotation_tyre_time), 0.0f, Space.Self);
            }
        }
        if(Input.GetKey(KeyCode.D))
        {
            if(this.gameObject.tag == "Front_Wheels")
            {
                 this.gameObject.transform.Rotate(0.0f,Mathf.Lerp(0.0f,-25.0f,y_rotation_tyre_time), 0.0f, Space.Self);
            }
        }
        this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f,1.0f)*Tire_Size;
    }
}
