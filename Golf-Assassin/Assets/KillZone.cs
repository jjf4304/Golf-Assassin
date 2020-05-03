using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZone : MonoBehaviour
{
    GameObject player;
    public float killHeight;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < killHeight){
            
            Application.LoadLevel( SceneManager.GetActiveScene().buildIndex);
        }
    }
}
