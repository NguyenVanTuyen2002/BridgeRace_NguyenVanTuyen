using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public BoxCollider colDoor;

    public IEnumerator DisappearAndReappear()
    {
        yield return new WaitForSeconds(0.1f);
        colDoor.isTrigger = false;
    }
}
