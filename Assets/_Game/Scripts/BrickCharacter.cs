using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BrickCharacter : Singleton<BrickCharacter>
{
    [SerializeField] private Renderer renderer0;
    [SerializeField] private ColorDataSO colorDataSO;

    public Transform BrickParent; // Cha của các đối tượng brickCharacter
    public GameObject player;
    public List<GameObject> bricks = new List<GameObject>();
    public ColorType colorType;

    private int touchCount = 0; // Số lần chạm đối tượng có tag brick

    private static BrickCharacter ins;
    public static BrickCharacter Ins => ins;

    public void ChangeColor(Material material)
    {
        renderer0.material = material;
    }

    internal void SetPosition( float height)
    {
        transform.localPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        //ansform.position = new Vector3(parent.position.x, parent.position.y + height, parent.position.z);
    }
}
