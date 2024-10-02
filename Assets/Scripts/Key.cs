using TreeEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Key : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.root.CompareTag("Player"))
        {
            GameManager.Instance.KeyCollected();

            Destroy(GetComponent<SphereCollider>());
            Destroy(transform.Find("Object").gameObject);
            audioSource.Play();
            Destroy(gameObject, .8f);
        }
    }
}