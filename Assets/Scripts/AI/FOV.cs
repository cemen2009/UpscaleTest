using UnityEngine;

public class FOV : MonoBehaviour
{
    [SerializeField] BaseAIController controller;

    public Transform targetTransform { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            // checking line of sight by perfoming raycast

            Vector3 directionToPlayer = (other.transform.root.position - transform.root.position).normalized;
            float distanceToPlayer = Vector3.Distance(transform.root.position, other.transform.root.position);

            if (!Physics.Raycast(transform.root.position, directionToPlayer, distanceToPlayer, controller.obstructionLayer))
            {
                targetTransform = other.transform;
            }
            else
            {
                targetTransform = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            // it doesn't worth to make raycast for checking if enemy can see player, so we just clear target transform anyway

            targetTransform = null;
        }
    }
}
