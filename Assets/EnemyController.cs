using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HumanTypes humanType;

    public List<SolverController> solvers;
    public Animator animator;
    public int moveCount;

    private void OnEnable()
    {
        EventManager.PlayerCompletedMove += NextMove;
    }

    private void OnDisable()
    {
        EventManager.PlayerCompletedMove -= NextMove;
    }

    private void NextMove()
    {
        animator.SetBool(moveCount.ToString(),true);
        moveCount++;
    }

    public void MoveCompleted()
    {
        
        foreach (var solver in solvers)
        {
            EventManager.SetReferenceGap(solver.solverType,solver.transform.localPosition);
            
        }
    }
}
