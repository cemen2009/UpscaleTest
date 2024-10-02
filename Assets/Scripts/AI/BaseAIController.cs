using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseAIController : MonoBehaviour
{
    [SerializeField] FOV fov;
    NavMeshAgent agent;

    Vector3 originPoint;

    [SerializeField]
    float shiftDistance = 0.01f, attackDistance = 2f;

    public LayerMask obstructionLayer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        originPoint = transform.position;
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.GameFlow)
        {
            agent.SetDestination(transform.position);
            return;
        }

        if (fov.targetTransform != null)
        {
            agent.SetDestination(fov.targetTransform.position);

            if (Vector3.Distance(transform.position, fov.targetTransform.position) <= attackDistance)
            {
                agent.SetDestination(transform.position);

                MakeAttack(fov.targetTransform.GetComponent<CharController>());
            }
        }
        else 
        {
            Patrol();
        }
    }

    private void MakeAttack(CharController charController)
    {
        // play attack sound

        GameManager.Instance.EndGame();
    }

    private void Patrol()
    {
        if (Vector3.Distance(transform.position, originPoint) <= shiftDistance)
        {
            // TODO: generate patrol point
        }
        else
        {
            agent.SetDestination(originPoint);
        }
    }
}
