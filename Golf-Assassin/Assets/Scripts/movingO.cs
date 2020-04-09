using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingO : MonoBehaviour
{
    public Quaternion dir = new Quaternion(10,0,0,1);
    private int speed = 1;
    static private System.Random rnd = new System.Random();
    public float time = 5.0f;
    public float timer = 0;

    // Start is called before the first frame update
    void Awake()
    {
        
    }
    void Start()
    {
        ChangeDir();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += this.gameObject.transform.forward * speed * Time.deltaTime;
        this.gameObject.transform.up = Vector3.up;
        if(timer >= time)
        {
            timer = 0;
            ChangeDir();
        }
        timer += Time.deltaTime;
    }

    void ChangeDir()
    {
        Debug.Log("changeDir");
        dir = new Quaternion(0, rnd.Next(-180, 180), rnd.Next(-180, 180), 1);
        gameObject.transform.rotation = dir; //this is not setting anything for some reason
    }
}
