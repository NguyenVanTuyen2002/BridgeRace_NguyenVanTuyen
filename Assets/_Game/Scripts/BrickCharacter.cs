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

    //public ColorType colorType;

    private void Start()
    {

    }
    public void ChangeColor(Material material)
    {
        renderer0.material = material;
    }

    /*internal void SetPosition( float height)
    {
        transform.localPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        //transform.position = new Vector3(parent.position.x, parent.position.y + height, parent.position.z);
    }*/
}
