using System.Collections;
using UnityEngine;

public class Crate : InteractableBase
{
    [Header("Local Settings")]
    [SerializeField] private Animator animator;

    private int _animOpenID;

    private void Start()
    {
        FetchAnimationIDs();
    }

    public void FetchAnimationIDs()
    {
        _animOpenID = Animator.StringToHash("Open");
    }

    public override void Interact()
    {
        animator.SetBool(_animOpenID, true);

        _isInteractable = false;

        StartCoroutine(Reactivate());
    }

    protected override IEnumerator Reactivate()
    {
        yield return base.Reactivate();

        animator.SetBool(_animOpenID, false);
    }
}
