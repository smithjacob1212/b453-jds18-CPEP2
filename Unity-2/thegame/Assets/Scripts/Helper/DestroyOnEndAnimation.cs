using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DestroyOnEndAnimation : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}