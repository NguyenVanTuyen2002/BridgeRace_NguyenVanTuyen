using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : Singleton<Level>
{
    [SerializeField] public List<Platform> platforms;

    public List<Transform> startPoint;
    public Transform finishPoint;

    private void Start()
    {
        
    }
}
