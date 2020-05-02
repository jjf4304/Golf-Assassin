using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deactivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Values.HoleIndex == Values.HoleNames.Count - 1)
        {
            GetComponent<Button>().enabled = false;
            GetComponent<Image>().color = Color.gray;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
