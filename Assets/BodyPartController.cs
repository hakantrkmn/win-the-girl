using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour
{
    public SolverTypes bodyType;
    private void Start()
    {
        EventManager.SetSolvers(bodyType, transform);
    }
}
