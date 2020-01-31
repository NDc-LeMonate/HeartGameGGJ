using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{

    private void Start()
    {
        GameController.instance.tileList.Add(transform);
    }
}
