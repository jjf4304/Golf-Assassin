using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumper : MonoBehaviour
{
    public float force = 5.0f;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ASDFASDFASDF");
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce( (collision.gameObject.transform.position - transform.position).normalized * force, ForceMode.Impulse);
          
        }
    }
}
