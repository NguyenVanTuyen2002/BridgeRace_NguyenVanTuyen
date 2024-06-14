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
    [SerializeField] public List<BrickCharacter> brickCharactors = new List<BrickCharacter>();
    [SerializeField] private BrickBridge brickBridgePrefab;

    public string currentAnimName;
    public Animator anim;
    public ColorType colorType;

    private float _height;
    private bool isSecondGridActive = false;

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void SetColor(ColorType colorType)
    {
        this.colorType = colorType;

        foreach (var renderer in renderers)
        {
            //renderer.material = newMaterial;
            renderer.material = LevelManager.Ins.colorDataSO.GetMat(colorType);
        }
    }

    public void ClearBrick()
    {
        // Kiểm tra nếu danh sách rỗng, thoát khỏi hàm
        if (brickCharactors.Count == 0) return;

        // Duyệt qua tất cả các phần tử trong danh sách
        for (int i = brickCharactors.Count - 1; i >= 0; i--)
        {
            // Lấy viên gạch từ danh sách
            BrickCharacter newBrick = brickCharactors[i];

            // Xóa viên gạch ra khỏi danh sách
            brickCharactors.RemoveAt(i);

            // Hủy đối tượng viên gạch
            Destroy(newBrick.gameObject);
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

    protected virtual void ColliderWithDoor(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Door)) return;

        Door doors = Cache.GetDoor(other);

        ActivateBricksWithSameColor(colorType);
        isSecondGridActive = true;
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

    protected virtual void OnTriggerEnter(Collider other)
    {
        ColliderWithBrick(other);
        ColliderWithDoor(other);
    }
}
