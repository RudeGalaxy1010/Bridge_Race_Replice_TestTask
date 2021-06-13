using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private ColorsInformation _colorsInformation;
    [SerializeField] private Transform _bricksTowerTransform;

    public Color Color => _color;
    private Color _color;
    private List<Brick> _bricks;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _bricks = new List<Brick>();
        _color = _colorsInformation.GetPlayerColor();
        var playerRenderer = GetComponent<Renderer>();
        playerRenderer.material = new Material(playerRenderer.material);
        playerRenderer.material.color = _color;
    }

    public void AddBrick(Brick brick)
    {
        if (brick == null)
        {
            Debug.LogError("Brick is null");
            return;
        }

        brick.transform.SetParent(_bricksTowerTransform);
        brick.transform.position = _bricksTowerTransform.position + Vector3.up * (brick.transform.localScale.y + 0.01f) * _bricks.Count;
        _bricks.Add(brick);
    }

    public Brick RemoveBrick()
    {
        if (_bricks.Count == 0)
        {
            return null;
        }

        var brick = _bricks[_bricks.Count - 1];
        _bricks.Remove(brick);
        return brick;
    }
}
