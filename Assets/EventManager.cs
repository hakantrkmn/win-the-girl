using System;
using UnityEngine;


public static class EventManager
{

    public static Action<SolverTypes,Transform> SetSolvers;
    public static Action<SolverTypes,Vector3> SetReferenceGap;
    public static Action SolverDone;
    public static Action<float> UpdateProgress;

    public static Action PlayerCompletedMove;



}