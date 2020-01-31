using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;


public class TileScript : MonoBehaviour
{
    public enum TileType { Normal, Pit, Conveyor }
    public enum Direction { Up, Down, Left, Right }

    public TileType tileType;


    [ConditionalField(nameof(tileType), false, TileType.Conveyor)]
    public Direction direction;


    private void Start()
    {
        GameController.instance.tileList.Add(this);
    }

    public Vector3 ConveyorDir()
    {

        switch (direction)
        {
            case Direction.Up:
                return new Vector3(0, 0,1);
            case Direction.Down:
                return new Vector3(0, 0,-1);
            case Direction.Right:
                return new Vector3(1, 0,0);
            default:
                return new Vector3(-1, 0,0);
        }

    }
}
