using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCode : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject power;
    private Vector3 playercurrentposition;
    private Vector3 enemycurrentposition;
    private Movement moving;
    // Start is called before the first frame update
    public enum timeobjectstate{
        stopingplayer,
        stopingenemy,
    }

    public timeobjectstate currentstate;
    //private void Start()
    //{
    //    player.transform.position = playercurrentposition;
    //    enemy.transform.position = enemycurrentposition;
    //}
   
   
    public void stopingplayer()
    {
        if(player.transform.position == playercurrentposition)
        {
            Debug.Log(playercurrentposition);
        }


    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other == player)
        {
            Destroy(this);
            
        }
    }
   
    public void callingstate()
    {
        switch (currentstate)
        {
            case timeobjectstate.stopingplayer:
                stopingplayer();
                break;
            case timeobjectstate.stopingenemy:
                
                break;
        }
    }
}
