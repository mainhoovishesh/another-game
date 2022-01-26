using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask unwalkableMask;
    public Vector2 gridworldSize;
    public float nodeRadius;
    Node[,] _grid;
    float NodeDiameter;
    int gridSizeX,gridsizeY;
    public Transform player;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(gridworldSize.x,1,gridworldSize.y));

        if(_grid!=null)
        {
            Node playernode = nodeFromWorldPoint(player.position);
            foreach (Node n in _grid)
            {
                Gizmos.color = n.walkable?Color.white:Color.blue;
                if(playernode==n)
                {
                    Gizmos.color = Color.black;
                }
                Gizmos.DrawCube(n.WorldPos,(Vector3.one*(NodeDiameter-0.1f)));
            }
        }
    }
    void Start()
    {
        NodeDiameter = nodeRadius*2;
        gridSizeX = Mathf.RoundToInt(gridworldSize.x/NodeDiameter);
        gridsizeY = Mathf.RoundToInt(gridworldSize.y/NodeDiameter);
        CreateGrid();
    }
    public Node nodeFromWorldPoint(Vector3 _worldPos)
    {
        float PercentageX = ((_worldPos.x+gridworldSize.x/2)/gridworldSize.x);
        float PercentageY = ((_worldPos.z+gridworldSize.y/2)/gridworldSize.y);
        PercentageX = Mathf.Clamp01(PercentageX);
        PercentageY = Mathf.Clamp01(PercentageY);
        int x = Mathf.RoundToInt((gridSizeX-1)*PercentageX);
        int y = Mathf.RoundToInt((gridsizeY-1)*PercentageY);
        return _grid[x,y];
    }
    /* public Node nodefromworldpoint(Vector3 _playworldPos)
    {
        float PercentageX = ((_playworldPos+gridw
    } */
    void CreateGrid()
    {
        _grid = new Node[gridSizeX,gridsizeY];
        Vector3 WorldBottomLeft = transform.position-Vector3.right*gridworldSize.x/2-Vector3.forward*gridworldSize.y/2;;
        for(int x = 0;x<gridSizeX;x++)
        {
            for(int y=0;y<gridsizeY;y++)
            {
                Vector3 WorldPoint = WorldBottomLeft + Vector3.right * (x*NodeDiameter+nodeRadius) + Vector3.forward*(y*NodeDiameter+nodeRadius);
                bool walkable = !(Physics.CheckSphere(WorldPoint,nodeRadius,unwalkableMask));
                _grid[x,y] = new Node(walkable,WorldPoint);
            }
        }

    }
    void createGrid()
    {
        _grid = new Node[gridSizeX,gridsizeY];
        Vector3 WorldBottomLeftPoint = transform.position - Vector3.right*(gridworldSize.x/2) - Vector3.forward*(gridworldSize.y/2);
        for(int x=0;x<gridSizeX;x++)
        {
            for(int y=0;y<gridsizeY;y++)
            {
                Vector3 worldpoint = WorldBottomLeftPoint + Vector3.right*(x*NodeDiameter+nodeRadius) +Vector3.forward*(y*NodeDiameter+nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldpoint,nodeRadius,unwalkableMask));
                _grid[x,y] = new Node(walkable,worldpoint);
            }
        } 
    }
}
