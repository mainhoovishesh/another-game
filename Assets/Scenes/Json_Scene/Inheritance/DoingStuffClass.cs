using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoingStuffClass : MonoBehaviour
{
    private State_Parent_Class state;
    // Start is called before the first frame update
    void Start()
    {
        state = new State_Parent_Class();
    }

    // Update is called once per frame
    void Update()
    {
        state = state.handleInput();

    }
}
