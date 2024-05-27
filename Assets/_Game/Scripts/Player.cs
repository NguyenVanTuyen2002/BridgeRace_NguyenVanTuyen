using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Canvas inputCanvas;
    [SerializeField] Animator anim;
    [SerializeField] Renderer renderer0;
    [SerializeField] Renderer renderer1;
    [SerializeField] Renderer renderer2;
    [SerializeField] Renderer renderer3;
    [SerializeField] Renderer renderer4;
    [SerializeField] Renderer renderer5;
    [SerializeField] ColorDataSO colorDataSO;

    //private bool onStairs = false;
    
    protected ColorType colorType;

    private void Start()
    {
         ChangeColor(ColorType.Red);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float v = joystick.Vertical;
        float h = joystick.Horizontal;
        Vector3 translate = (new Vector3(-h, 0, -v) * Time.deltaTime) * movementSpeed;

        if (translate != Vector3.zero)
        {
            anim.SetBool("Running", true);
            Debug.Log("run");
        }
        else
        {
            anim.SetBool("Running", false);
            Debug.Log("idle");

        }

        transform.Translate(translate);
    }

    public void ChangeColor(ColorType color)
    {
        this.colorType = color;
        renderer0.material = colorDataSO.GetMat(color);
        renderer1.material = colorDataSO.GetMat(color);
        renderer2.material = colorDataSO.GetMat(color);
        renderer3.material = colorDataSO.GetMat(color);
        renderer4.material = colorDataSO.GetMat(color);
        renderer5.material = colorDataSO.GetMat(color);
    }

    void OnTriggerEnter(Collider other)
    {
        Brick brick = Cache.GetBrick(other);

        // Kiểm tra va chạm với cầu thang
        if (other.CompareTag(CacheString.Tag_Stairs))
        {
            //onStairs = true;
        }
        else if (other.CompareTag(CacheString.Tag_Brick) && colorType == brick.colorType)
        {
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Kiểm tra khi rời khỏi cầu thang
        if (other.CompareTag(CacheString.Tag_Brick))
        {
            //onStairs = false;
        }
    }
}
