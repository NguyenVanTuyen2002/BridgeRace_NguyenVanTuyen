using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed = 5;

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
        Vector3 translate = (new Vector3(-h, 0, -v) * Time.deltaTime) * speed;
        transform.Translate(translate);
    }

    void OnTriggerEnter(Collider other)
    {
        // Kiểm tra va chạm với cầu thang
        if (other.CompareTag("Stairs"))
        {
            onStairs = true;
            Debug.Log("hit");
        }
        else if (other.CompareTag("Brick"))
        {
            Debug.Log("box");
            other.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Kiểm tra khi rời khỏi cầu thang
        if (other.CompareTag("Stairs"))
        {
            onStairs = false;
        }
    }
}
