using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class Gate : MonoBehaviour, IInteractable
{
    Animator animator;
    AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TryInteract(GameObject initiator)
    {
        // if animator is transitioning or an animation is still playing -- don't trigger animation
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f || animator.IsInTransition(0))
        {
            return;
        }

        animator.SetTrigger("OpenCloseTrigger");
        audioSource.Play();
    }

    public void RebakeSurface()
    {
        GameManager.Instance.BakeNavmesh();
    }
}
