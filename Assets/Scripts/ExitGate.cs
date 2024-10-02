using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGate : MonoBehaviour
{
    [SerializeField] Material exitGateMaterial;
    bool locked = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.CompareTag("Player") && !locked)
            EndRun();
    }

    public void Lock()
    {
        exitGateMaterial.color = Color.red;
        locked = true;
    }

    public void Unlock()
    {
        exitGateMaterial.color = Color.green;
        locked = false;
    }

    private void EndRun()
    {
        GameManager.Instance.EndGame();
    }
}
