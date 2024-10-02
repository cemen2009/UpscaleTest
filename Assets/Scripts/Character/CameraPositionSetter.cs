using UnityEngine;

public class CameraPositionSetter : MonoBehaviour
{
    [SerializeField] Transform cameraPosition;

    private void Update()
    {
        if (GameManager.Instance.gameState != GameState.GameFlow) return;

        transform.position = cameraPosition.position;
    }
}
