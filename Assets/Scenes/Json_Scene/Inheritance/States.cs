using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class States:State_Parent_Class
{
    public override State_Parent_Class handleInput()
    {
        if(Input.GetKey(KeyCode.V))
        {
            Debug.Log("State space pressed");
        }
        return this;

    }
}
public class State_2:State_Parent_Class
{
    public override State_Parent_Class handleInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("State_2 space pressed");
        }
    return this;
        
    }
}
