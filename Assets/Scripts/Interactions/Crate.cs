using System.Collections;
using UnityEngine;

public class Crate : InteractableBase
{
    [Header("Local Settings")]
    [SerializeField] private Animator animator;

    [Header("Spawnable Items")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private GameObject magnetPrefab;

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

        SpawnItems();

        StartCoroutine(Reactivate());
    }

    private void SpawnItems()
    {
        Rigidbody batteryRB = Instantiate(batteryPrefab, spawnPoints[0]).GetComponent<Rigidbody>();
        Rigidbody magnetRB = Instantiate(magnetPrefab, spawnPoints[1]).GetComponent<Rigidbody>();

        Vector3 initialForce = new(0, 300, -50);

        batteryRB.AddForce(initialForce);
        magnetRB.AddForce(initialForce);

        batteryRB.AddRelativeTorque(Random.insideUnitSphere);
        magnetRB.AddRelativeTorque(Random.insideUnitSphere);
    }

    protected override IEnumerator Reactivate()
    {
        yield return base.Reactivate();

        animator.SetBool(_animOpenID, false);
    }
}
