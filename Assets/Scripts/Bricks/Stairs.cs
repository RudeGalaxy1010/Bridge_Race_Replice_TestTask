using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private StairsTrigger stairsTrigger;
    [SerializeField] private Transform _stairsTransform;
    [SerializeField] private int _bricksToWin;
    private List<Brick> _bricks;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stairsTrigger.OnPlayerEnter += PlayerEnter;
        _bricks = new List<Brick>();
    }

    private void PlayerEnter(PlayerInventory playerInventory)
    {
        var brick = playerInventory.RemoveBrick();

        if (brick == null)
        {
            return;
        }

        brick.GetComponent<Collider>().isTrigger = false;
        brick.transform.localScale = new Vector3(2, brick.transform.localScale.y, 1);
        brick.transform.rotation = Quaternion.identity;
        var offset = (Vector3.forward * brick.transform.localScale.z + Vector3.up * brick.transform.localScale.y) * _bricks.Count;
        brick.transform.position = _stairsTransform.position + offset;
        brick.transform.SetParent(transform);
        _bricks.Add(brick);

        stairsTrigger.gameObject.transform.position = _stairsTransform.position + offset;

        if (_bricks.Count >= _bricksToWin)
        {
            _winPanel.SetActive(true);
            _playerController.BlockMove();
        }
    }
}
