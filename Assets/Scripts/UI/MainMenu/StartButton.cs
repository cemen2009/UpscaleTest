using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StartButton : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] string levelName;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartLevel()
    {
        StartCoroutine(DelayedExecution(.5f));
    }

    private IEnumerator DelayedExecution(float delay)
    {
        audioSource.Play();

        yield return new WaitForSeconds(delay);

        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
