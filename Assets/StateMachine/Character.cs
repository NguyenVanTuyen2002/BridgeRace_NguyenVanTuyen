using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : GameUnit
{
    [SerializeField] protected ColorDataSO colorDataSO;
    [SerializeField] private List<SkinnedMeshRenderer> renderers;
    [SerializeField] private BrickCharacter brickCharacterPrefab;
    [SerializeField] private Transform brickContainer;
    [SerializeField] protected List<BrickCharacter> brickCharactors = new List<BrickCharacter>();
    [SerializeField] private BrickBridge brickBridgePrefab;

    public LayerMask brickBridgeLayer;
    public Renderer skinCharacter;
    public ColorType colorType;

    private IState<Character> _currentState;
    private float _height;

    private void Start()
    {
        ChangeState(new IdleState());
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Character> state)
    {
        if (_currentState != null)
        {
            _currentState.OnExit(this);
        }

        _currentState = state;

        if (_currentState != null)
        {
            _currentState.OnEnter(this);
        }
    }

    protected void RemoveBrick()
    {
        if (brickCharactors.Count == 0) return;
        // Lấy phần tử cuối cùng trong danh sách
        BrickCharacter newBrick = brickCharactors[brickCharactors.Count - 1];

        // Xóa viên gạch ra khỏi danh sách
        brickCharactors.RemoveAt(brickCharactors.Count - 1);

        // Hủy đối tượng viên gạch
        Destroy(newBrick.gameObject);
    }

    public void SetColor(ColorType color)
    {
        this.colorType = color;

        Material newMaterial = colorDataSO.GetMat(color);
        foreach (var renderer in renderers)
        {
            renderer.material = newMaterial;
        }
    }

    private IEnumerator SetActiceBrickAfterDelay(Brick brick, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetActiceBrick(brick);
    }

    protected void SetActiceBrick(Brick brick)
    {
        brick.gameObject.SetActive(true);
    }

    protected void ColliderWithBrick(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Brick)) return;
        Brick brick = Cache.GetBrick(other);
        if (brick.colorType != colorType) return;
        brick.gameObject.SetActive(false);
        StartCoroutine(SetActiceBrickAfterDelay(brick, 5f));
        _height = 0.3f;
        BrickCharacter newBrick = Instantiate(brickCharacterPrefab, brickContainer);
        if (newBrick != null)
        {
            brickCharactors.Add(newBrick);
            newBrick.ChangeColor(colorDataSO.GetMat(colorType));
            newBrick.transform.localPosition = brickCharactors.Count * _height * Vector3.up;
        }
        else
        {
            Debug.LogError("Failed to create new brick.");
        }
    }

    protected void ColliderWithDoor(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Door)) return;

        Door doors = Cache.GetDoor(other);

        ActivateBricksWithSameColor(colorType);
        SetBricksToSecondGrid(colorType);
    }

    protected void ActivateBricksWithSameColor(ColorType color)
    {
        // Duyệt qua danh sách brickListLv2 để kích hoạt các viên gạch có màu trùng khớp
        Platform platform = FindObjectOfType<Platform>();
        if (platform != null)
        {
            foreach (var brick in platform.brickListLv2)
            {
                if (brick.colorType == color)
                {
                    brick.gameObject.SetActive(true);
                }
            }
        }
    }

    protected void SetBricksToSecondGrid(ColorType color)
    {
        Brick[] allBricks = FindObjectsOfType<Brick>();
        foreach (var brick in allBricks)
        {
            if (brick.colorType == color)
            {
                brick.IsInSecondGrid = true; // Set the brick as part of the second grid
            }
        }
    }


    protected void OnTriggerEnter(Collider other)
    {
        ColliderWithBrick(other);
        ColliderWithDoor(other);
    }
}
