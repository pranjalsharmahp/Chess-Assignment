using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTileTags : MonoBehaviour
{
    private PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager=GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        Invoke("Rotate",10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Rotate(){
        if(playerManager.playerId==2){
            transform.rotation=Quaternion.Euler(0,0,180);
        }
        
    }
}
