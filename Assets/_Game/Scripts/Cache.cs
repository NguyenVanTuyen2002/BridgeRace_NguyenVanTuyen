using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }

        return characters[collider];
    }

    private static Dictionary<Collider, Brick> bricks = new Dictionary<Collider, Brick>();

    public static Brick GetBrick(Collider collider)
    {
        if (!bricks.ContainsKey(collider))
        {
            bricks.Add(collider, collider.GetComponent<Brick>());
        }

        return bricks[collider];
    }


    private static Dictionary<Collider, Stair> stairs = new Dictionary<Collider, Stair>();

    public static Stair GetStair(Collider collider)
    {
        if (!stairs.ContainsKey(collider))
        {
            stairs.Add(collider, collider.GetComponent<Stair>());
        }

        return stairs[collider];
    }

    private static Dictionary<Collider, BrickBridge> brickBridges = new Dictionary<Collider, BrickBridge>();

    public static BrickBridge GetBrickBridge(Collider collider) {
        if (!brickBridges.ContainsKey(collider))
        {
            brickBridges.Add(collider , collider.GetComponent<BrickBridge>());
            
        }
        return brickBridges[collider];
    }
    
}
