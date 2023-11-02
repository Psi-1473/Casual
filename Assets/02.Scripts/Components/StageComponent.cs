using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageComponent : MonoBehaviour
{
    int openedChapter = 1;
    int openedStage = 1;
    
    public int OpenedChapter { get { return openedChapter; } set { openedChapter = value; } }
    public int OpenedStage { get { return openedStage; } set { openedStage = value; } }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
