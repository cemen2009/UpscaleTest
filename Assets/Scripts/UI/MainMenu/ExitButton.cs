using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExitButton : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Exit()
    {
        StartCoroutine(DelayedExecution(.5f));
    }

    private IEnumerator DelayedExecution(float delay)
    {
        audioSource.Play();

        yield return new WaitForSeconds(delay);

        Application.Quit();
    }
}
