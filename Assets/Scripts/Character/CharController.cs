using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Image;

[RequireComponent(typeof(PlayerMovement))]
public class CharController : MonoBehaviour
{
    [SerializeField] float interactionDistance = 3f;
    GameObject hoveredGameobject;

    PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.GameFlow) return;

        DetectInteractableObject();

        // showing interaction tip according to hovered object availability
        if (hoveredGameobject != null)
            GameManager.Instance.ShowTip();
        else
            GameManager.Instance.HideTip();

        // interaction using E key
        if (Input.GetKeyDown(KeyCode.E) && hoveredGameobject != null)
        {
            hoveredGameobject.GetComponent<IInteractable>().TryInteract(gameObject);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            GameManager.Instance.Pause();
        }
    }

    private void DetectInteractableObject()
    {
        // check is there any interactable object in interaction distance
        if (Physics.Raycast(transform.position, movement.orientation.forward, out RaycastHit hitInfo, interactionDistance))
        {
            if (hitInfo.collider.transform.parent.TryGetComponent<IInteractable>(out IInteractable interactableGO))
                hoveredGameobject = hitInfo.collider.transform.parent.gameObject;
            else
                hoveredGameobject = null;
        }
        else
            hoveredGameobject = null;
    }
}
