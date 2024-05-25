using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Canvas inputCanvas;

    private bool onStairs = false;

    void Update()
    {
        if (onStairs)
        {
            Move();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        float v = joystick.Vertical;
        float h = joystick.Horizontal;
        Vector3 translate = (new Vector3(-h, 0, -v) * Time.deltaTime) * movementSpeed;
        transform.Translate(translate);
    }

    void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm với cầu thang
        if (other.CompareTag(CacheString.Tag_Stairs))
        {
            onStairs = true;
            Debug.Log("hit");
        }
        else if (other.CompareTag(CacheString.Tag_Brick))
        {
            Debug.Log("box");
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Kiểm tra khi rời khỏi cầu thang
        if (other.CompareTag(CacheString.Tag_Brick))
        {
            onStairs = false;
        }
    }
}
