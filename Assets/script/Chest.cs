using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator animator;
    private bool isOpened = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpened && other.CompareTag("Player"))
        {
            isOpened = true;
            animator.SetTrigger("Finish");
        }
    }
}
