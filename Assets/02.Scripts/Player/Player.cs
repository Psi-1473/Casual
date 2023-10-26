using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {
        string name = Managers.Data.HeroDict[0].name;
        Debug.Log(name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
