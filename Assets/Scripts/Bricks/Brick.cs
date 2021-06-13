using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Brick : MonoBehaviour
{
    public UnityAction<Brick> OnPickUp;

    public Color Color => _color;
    private Color _color;

    [SerializeField] private List<TrailRenderer> trailRenderers;

    public void Init(Color color)
    {
        _color = color;
        var brickRenderer = GetComponent<Renderer>();
        brickRenderer.material = new Material(brickRenderer.material);
        brickRenderer.material.color = _color;

        foreach (var trailRenderer in trailRenderers)
        {
            trailRenderer.startColor = _color;
        }
    }

    public bool CheckColor(Color color)
    {
        return color == _color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerInventory playerInventory))
        {
            if (CheckColor(playerInventory.Color))
            {
                OnPickUp?.Invoke(this);
            }
        }
    }
}
