using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    // Start is called before the first frame update
    public bool walkable;
    public Vector3 WorldPos;
    public Node(bool _walkable,Vector3 _WorldPos)
    {       
        walkable = _walkable;
        WorldPos = _WorldPos;
    }
}
