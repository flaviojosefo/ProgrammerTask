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

        IsInteractable = false;

        SpawnItems();

        StartCoroutine(Reactivate());
    }

    private void SpawnItems()
    {
        // Spawn 1 battery and 1 magnet
        Rigidbody batteryRB = Instantiate(batteryPrefab, spawnPoints[0]).GetComponent<Rigidbody>();
        Rigidbody magnetRB = Instantiate(magnetPrefab, spawnPoints[1]).GetComponent<Rigidbody>();

        Vector3 initialForce = new(0, 300, -50);

        // Project items out of the create and towards the player
        batteryRB.AddForce(initialForce);
        magnetRB.AddForce(initialForce);

        // Add small random torque
        batteryRB.AddRelativeTorque(Random.insideUnitSphere);
        magnetRB.AddRelativeTorque(Random.insideUnitSphere);
    }

    protected override IEnumerator Reactivate()
    {
        yield return base.Reactivate();

        animator.SetBool(_animOpenID, false);
    }
}
