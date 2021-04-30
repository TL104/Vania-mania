using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool hasKey = false;

    public void getKey() { hasKey = true; }

    public bool checkForKey() { return hasKey; }
    
}
