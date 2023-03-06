using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public HumanTypes humanType;
    public Transform referencePoint;

    public List<SolverController> solvers;


    private void Start()
    {
        foreach (var solver in solvers)
        {
            //EventManager.SetReferenceGap(solver.solverType,Mathf.Abs((solver.transform.position - referencePoint.position).magnitude));
            EventManager.SetReferenceGap(solver.solverType,solver.transform.localPosition);

        }
        
    }
}
