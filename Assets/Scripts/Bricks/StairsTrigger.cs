using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class StairsTrigger : MonoBehaviour
{
    public UnityAction<PlayerInventory> OnPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInventory playerInventory))
        {
            OnPlayerEnter?.Invoke(playerInventory);
        }
    }
}
