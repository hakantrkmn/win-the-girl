using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SolverController : MonoBehaviour
{
    public HumanTypes humanType;
    public SolverTypes solverType;
    public SolverTypes targetSolverType;

    public float speed;
    public Transform targetBodyPart;
    public Image dot;
    private Vector3 mouseStartPosition;
    private float timer;
    private Vector2 direction;
    public Vector3 targetSolverPos;

    private bool solverDone;
    private void OnEnable()
    {
        EventManager.PlayerCompletedMove += PlayerCompletedMove;
        EventManager.SetReferenceGap += SetReferenceGap;
        EventManager.SetSolvers += SetSolvers;
    }

    private void PlayerCompletedMove()
    {
    }

    private void Start()
    {
        if (GetComponentInParent<PlayerController>())
        {
            humanType = GetComponentInParent<PlayerController>().humanType;

        }
        else if(GetComponentInParent<EnemyController>())
        {
            humanType = GetComponentInParent<EnemyController>().humanType;
        }
        
      
    }

    public void FixSolverPositions()
    {
        transform.position = new Vector3(targetBodyPart.position.x,targetBodyPart.position.y,transform.position.z);

    }

    private Vector3 tempPos;
    private void Update()
    {
        if (humanType==HumanTypes.Player&&!solverDone)
        {
            tempPos = new Vector2(Mathf.Abs(transform.localPosition.x), Mathf.Abs(transform.localPosition.y));
            var magn = ((Vector2)tempPos - (Vector2)targetSolverPos).sqrMagnitude;
            if (magn<.005f)
            {
                solverDone = true;
                SolverDone();
                EventManager.SolverDone();
            }
            
            dot.color = Color.Lerp(Color.green, Color.red, magn);

        }
        
    }

    public void SolverDone()
    {
        GetComponent<Collider>().enabled = false;
    }

    private void SetReferenceGap(SolverTypes type, Vector3 refGap)
    {
        if (type==targetSolverType)
        {
            GetComponent<Collider>().enabled = true;
            solverDone = false;
            targetSolverPos.x = Mathf.Abs(refGap.x);
            targetSolverPos.y = Mathf.Abs(refGap.y);
            targetSolverPos.z = refGap.z;

        }
    }

    private void OnDisable()
    {
        EventManager.SetReferenceGap -= SetReferenceGap;
        EventManager.SetSolvers -= SetSolvers;
        EventManager.PlayerCompletedMove -= PlayerCompletedMove;
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
        if (!solverDone)
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
       
    }

    private void OnMouseUp()
    {
        //transform.position = new Vector3(targetBodyPart.position.x,targetBodyPart.position.y,transform.position.z);
    }
}
