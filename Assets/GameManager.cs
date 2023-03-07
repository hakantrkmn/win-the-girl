using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public float progress;
   public int moveAmount;
   public int solverAmount;
   private int tempSolverAmount;
   private float solverProgress;

   private void Start()
   {
      solverProgress = 100 / (float)solverAmount;
      solverProgress = solverProgress / moveAmount;
   }

   private void OnEnable()
   {
      EventManager.SolverDone += SolverDone;
   }

   private void OnDisable()
   {
      EventManager.SolverDone -= SolverDone;

   }

   private void SolverDone()
   {
      progress += solverProgress;
      EventManager.UpdateProgress(progress / 100);
      tempSolverAmount++;
      if (tempSolverAmount==solverAmount)
      {
         moveAmount--;
         if (moveAmount==0)
         {
            
         }
         else
         {
            EventManager.PlayerCompletedMove();
            tempSolverAmount = 0;
         }
         
      }
   }
}
