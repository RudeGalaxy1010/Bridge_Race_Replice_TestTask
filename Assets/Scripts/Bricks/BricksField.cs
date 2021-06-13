using UnityEngine;

public class BricksField : MonoBehaviour
{
    [SerializeField] private ColorsInformation _colorsInformation;
    [SerializeField] private Vector2 _size;
    [SerializeField] private Brick _brickPrefab;
    [SerializeField] private PlayerInventory _playerInventory;
    private Brick[,] _bricks;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (_size.x % 2 != 0)
        {
            _size.x--;
        }
        if (_size.y % 2 != 0)
        {
            _size.y--;
        }

        ClearField();
        CreateField();
    }

    private void CreateField()
    {
        for (int i = (int)(-_size.x / 2f); i < (int)(_size.x / 2f); i++)
        {
            for (int j = (int)(-_size.y / 2f); j < (int)(_size.y / 2f); j++)
            {
                var brick = Instantiate(_brickPrefab, new Vector3(i, transform.position.y, j), Quaternion.identity);
                brick.transform.SetParent(transform);
                brick.Init(_colorsInformation.GetRandomColor());
                _bricks[i + (int)(_size.x / 2f), j + (int)(_size.y / 2f)] = brick;

                brick.OnPickUp += PickUpBrick;
            }
        }
    }

    private void ClearField()
    {
        if (_bricks != null && _bricks.Length > 0)
        {
            for (int i = 0; i < _size.x; i++)
            {
                for (int j = 0; j < _size.y; j++)
                {
                    Destroy(_bricks[i, j].gameObject);
                }
            }
        }

        _bricks = new Brick[(int)_size.x, (int)_size.y];
    }

    private void PickUpBrick(Brick brick)
    {
        brick.OnPickUp -= PickUpBrick;

        _playerInventory.AddBrick(brick);
        RemoveBrick(brick);
        
        // Spawn same brick in a while
    }

    private void RemoveBrick(Brick brick)
    {
        var index = (-1, -1);

        for (int i = 0; i < _bricks.GetLength(0); i++)
        {
            for (int j = 0; j < _bricks.GetLength(1); j++)
            {
                if (_bricks[i, j] == brick)
                {
                    index = (i, j);
                }
            }
        }

        _bricks[index.Item1, index.Item2] = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(_size.x, 0.5f, _size.y));
    }
}
