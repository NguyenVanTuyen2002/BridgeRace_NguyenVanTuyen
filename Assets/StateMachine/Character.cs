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

    /*public void OnInit()
    {
        var colors = colorDataSO.GetListColor();

        SetColor(colors[0]);
    }*/

    public void SetColor(ColorType colorType)
    {
        this.colorType = colorType;

        //Material newMaterial = colorDataSO.GetMat(color);
        foreach (var renderer in renderers)
        {
            //renderer.material = newMaterial;
            renderer.material = LevelManager.Ins.colorDataSO.GetMat(colorType);
        }
    }

    protected void ClearBrick()
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

    private void ShowUIWin()
    {
        UIManager.Ins.OpenUI<Win>();
        UIManager.Ins.CloseUI<GamePlay>();
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

    protected void ColliderWithFinishPoint(Collider other)
    {
        if (!other.CompareTag(CacheString.Tag_Door)) return;
        Finish finish = Cache.GetFinish(other);

        if (finish != null)
        {
            //Set player và bot 
            SetPlayerAndBotsOnWin(finish);

            Debug.Log("win");

            //Show Ui Win
            Invoke(nameof(ShowUIWin), 2f);
        }
    }

    private void SetPlayerAndBotsOnWin(Finish finish)
    {

        //Set Player
        //isMoving = false;
        ChangeAnim("Idle");
        GameManager.ChangeState(GameState.Win);

        finish.finishColonms[0].ChangeColor(colorType);
        TF.position = finish.finishColonms[0].GetPoint();
        TF.rotation = Quaternion.Euler(0, 180f, 0);
        ClearBrick();


        //Set Bot
        LevelManager.Ins.ChangeStateWinnerState();
        List<Bot> botCtls = LevelManager.Ins.bots;
        for (int i = 0; i < 2; i++)
        {
            finish.finishColonms[i + 1].ChangeColor(botCtls[i].colorType);
            botCtls[i].TF.position = finish.finishColonms[i + 1].GetPoint();
            botCtls[i].TF.rotation = Quaternion.Euler(0, 180f, 0);
            botCtls[i].ClearBrick();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        ColliderWithBrick(other);
        ColliderWithDoor(other);
    }
}
