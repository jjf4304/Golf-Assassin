using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
