﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;


public class TileScript : MonoBehaviour
{
    public enum TileType { Normal, Pit, Conveyor, Goal, Switch, Barrier }
    public enum Direction { Up, Down, Left, Right }

    public TileType tileType;


    [ConditionalField(nameof(tileType), false, TileType.Conveyor)]
    public Direction direction;

    [ConditionalField(nameof(tileType), false, TileType.Barrier)]
    public GameObject barrierObj;

    [ConditionalField(nameof(tileType), false, TileType.Switch)]
    public GameObject switchObj;
    [ConditionalField(nameof(tileType), false, TileType.Switch)]
    public TileScript switchedTile;

    private void Start()
    {
        GameController.instance.tileList.Add(this);

        if (tileType == TileType.Barrier)
            barrierObj = Instantiate(barrierObj, transform.position + Vector3.up,Quaternion.identity);
        if (tileType == TileType.Switch)
            switchObj = Instantiate(switchObj, transform.position + Vector3.up/2, Quaternion.identity);


    }


    public void ClearBarrier()
    {
        tileType = TileType.Normal;

        if (barrierObj != null)
        {
            Destroy(barrierObj);
        }
    }

    public void ClearSwitch()
    {
        tileType = TileType.Normal;

        if (switchObj != null)
        {
            Destroy(switchObj);
        }
    }


    public Vector3 ConveyorDir()
    {

        switch (direction)
        {
            case Direction.Up:
                return new Vector3(0, 0, 1);
            case Direction.Down:
                return new Vector3(0, 0, -1);
            case Direction.Right:
                return new Vector3(1, 0, 0);
            default:
                return new Vector3(-1, 0, 0);
        }

    }
}
