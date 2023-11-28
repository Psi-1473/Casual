using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageComponent : MonoBehaviour
{
    int openedChapter = 3;
    int openedStage = 2;
    
    public int OpenedChapter { get { return openedChapter; } set { openedChapter = value; } }
    public int OpenedStage { get { return openedStage; } set { openedStage = value; } }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OpenStageOrChapter(int _clearedChpater, int _clearedStage)
    {
        if (OpenedChapter == _clearedChpater && OpenedStage == _clearedStage)
        {
            OpenedStage++;

            if (_clearedStage == Managers.Data.StageDicts[openedChapter].Count)
            {
                OpenedChapter++;
                OpenedStage = 1;
            }
        }

        Debug.Log($"chpater, stage : {OpenedChapter}, {openedStage}");
    }
}
