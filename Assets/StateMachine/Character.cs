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
        /*int lastIndex = brickCharactors.Count - 1;

        // Lấy phần tử cuối cùng
        var lastElement = brickCharactors[lastIndex];

        // Loại bỏ phần tử cuối cùng khỏi danh sách
        brickCharactors.RemoveAt(lastIndex);
        Destroy(lastElement);*/
        // Xử lý phần tử (ví dụ: đưa vào pool)
        //SimplePool.Despawn(lastElement);

        if (brickCharactors.Count == 0)
        {
            Debug.LogWarning("No bricks in the list to remove.");
            return;
        }

        // Lấy phần tử cuối cùng trong danh sách
        BrickCharacter newBrick = brickCharactors[brickCharactors.Count - 1];

        // Xóa viên gạch ra khỏi danh sách
        brickCharactors.RemoveAt(brickCharactors.Count - 1);

        // Hủy đối tượng viên gạch
        Destroy(newBrick.gameObject);
    }

    /*public void CheckStairs()
    {
        Debug.DrawRay(TF.position, Vector3.down, Color.red, 5f);
        RaycastHit hit;
        if (Physics.Raycast(TF.position, Vector3.down, out hit, 5f, brickBridgeLayer))
        {
            BrickBridge brickBride = Cache.GetBrickBridge(hit.collider);
            brickBride.SetActiceStairs();
            brickBride.ChangeColor(this.colorType);
        }
    }*/
    public void SetColor(ColorType color)
    {
        this.colorType = color;

        Material newMaterial = colorDataSO.GetMat(color);
        foreach (var renderer in renderers)
        {
            renderer.material = newMaterial;
        }
    }

    protected void ColliderWithBrick(Collider other)
    {
        Debug.Log("hit");
        if (!other.CompareTag(CacheString.Tag_Brick)) return;
        Brick brick = Cache.GetBrick(other);
        if (brick.colorType != colorType) return;
        brick.gameObject.SetActive(false);
        Invoke("SetActiceBrick", 5f);
        _height = 0.3f;
        BrickCharacter newBrick = Instantiate(brickCharacterPrefab, brickContainer);
        if (newBrick != null)
        {
            Debug.Log("New brick created successfully.");
            brickCharactors.Add(newBrick);
            newBrick.ChangeColor(colorDataSO.GetMat(colorType));
            newBrick.transform.localPosition = brickCharactors.Count * _height * Vector3.up;
            Debug.Log("New brick added to list successfully.");
        }
        else
        {
            Debug.LogError("Failed to create new brick.");
        }
    }

    protected void SetActiceBrick(Brick brick)
    {
        brick.gameObject.SetActive(true);
    }

    /*private void CollideWithStair(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Stairs)) return;
        *//*Debug.Log("Cầu thang được chạm vào.");
        stairRenderer.enabled = true;
        // Lấy Renderer của cầu thang
        //Renderer stairRenderer = other.GetComponent<Renderer>();
        if (stairRenderer.enabled == false)
        {
            stairRenderer.enabled = true;
            // Đổi màu cầu thang thành màu của player
            stairRenderer.material = colorDataSO.GetMat(colorType);
        }

        // Thêm cầu thang vào danh sách brickBridges nếu nó chưa có trong danh sách
        BrickBridge brickBridge =
            other.GetComponent<BrickBridge>();
        if (brickBridge != null && !brickBridges.Contains(brickBridge))
        {
            brickBridges.Add(brickBridge);
            Debug.Log("Cầu thang được thêm vào danh sách brickBridges.");
        }*//*

        // Kiểm tra xem danh sách có phần tử nào không
        if (brickCharactors.Count == 0)
        {
            Debug.LogWarning("No bricks in the list to remove.");
            return;
        }

        // Lấy phần tử cuối cùng trong danh sách
        BrickCharacter newBrick = brickCharactors[brickCharactors.Count - 1];

        // Xóa viên gạch ra khỏi danh sách
        brickCharactors.RemoveAt(brickCharactors.Count - 1);

        // Hủy đối tượng viên gạch
        Destroy(newBrick.gameObject);
    }*/

    protected void OnTriggerEnter(Collider other)
    {
        ColliderWithBrick(other);
        //CollideWithStair(other);
    }
}
