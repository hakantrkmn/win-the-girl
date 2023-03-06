using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolverController : MonoBehaviour
{
    public HumanTypes humanType;
    public SolverTypes solverType;
    public float speed;
    public Transform targetBodyPart;
    public Transform referencePos;
    
    private Vector3 mouseStartPosition;
    private float timer;
    private Vector2 direction;
    public Vector3 targetSolverPos;

    private void OnEnable()
    {
        EventManager.SetReferenceGap += SetReferenceGap;
        EventManager.SetSolvers += SetSolvers;
    }

    private void Start()
    {
        if (GetComponentInParent<PlayerController>())
        {
            humanType = GetComponentInParent<PlayerController>().humanType;
            referencePos = GetComponentInParent<PlayerController>().referencePosition;

        }
        else if(GetComponentInParent<EnemyController>())
        {
            humanType = GetComponentInParent<EnemyController>().humanType;
        }
    }

    private void Update()
    {
        if (humanType==HumanTypes.Player)
        {
            if ((transform.localPosition- targetSolverPos).sqrMagnitude<.05f)
            {
                Debug.Log(solverType.ToString() + " done");
            }
        }
        
    }

    private void SetReferenceGap(SolverTypes type, Vector3 refGap)
    {
        if (type==solverType)
        {
            targetSolverPos.x = Mathf.Abs(refGap.x);
            targetSolverPos.y = Mathf.Abs(refGap.y);
            targetSolverPos.z = refGap.z;

        }
    }

    private void OnDisable()
    {
        EventManager.SetSolvers -= SetSolvers;
    }

    private void SetSolvers(SolverTypes type, Transform bodyPart)
    {
        if (type==solverType && transform.root==bodyPart.root)
        {
            targetBodyPart = bodyPart;
        }
    }

    private void OnMouseDown()
    {
        mouseStartPosition = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        timer += Time.deltaTime;
        if (timer>.05f)
        {
            mouseStartPosition = Input.mousePosition;
            timer = 0;
        }
        direction = mouseStartPosition - Input.mousePosition;
        direction = -direction.normalized;
        transform.position += new Vector3(direction.x, direction.y, 0)* Time.deltaTime*speed;
    }

    private void OnMouseUp()
    {
        transform.position = targetBodyPart.position;
    }
}
