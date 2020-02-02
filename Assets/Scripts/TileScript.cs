using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[ExecuteInEditMode]
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

    
    public Material goalMaterial;
    public GameObject arrowObj;
    public ParticleSystem barrierExplodeFX;
    public ParticleSystem switchFX;
    public GameObject pitObj;

    private void Start()
    {
        if(GameController.instance != null)
        {
            GameController.instance.tileList.Add(this);
        }

        if (tileType == TileType.Normal)
        {
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }

        if (tileType == TileType.Barrier)
        {
            if(transform.childCount > 0)
            {
                barrierObj = transform.GetChild(0).gameObject;
            }
            else
            {
                barrierObj = Instantiate(barrierObj, transform.position + Vector3.up / 2, Quaternion.identity);
                barrierObj.transform.SetParent(transform);
            }
           
        }
            
        else if (tileType == TileType.Switch)
        {
            if (transform.childCount > 0)
            {
                switchObj = transform.GetChild(0).gameObject;

            }
            else
            {
                switchObj = Instantiate(switchObj, transform.position + Vector3.up / 2, Quaternion.identity);
                switchObj.transform.SetParent(transform);
            }
                
        }

      
        if (transform.childCount > 0) return;

        if (tileType == TileType.Pit)
        {
            pitObj = Instantiate(pitObj, transform.position + Vector3.up/2, Quaternion.Euler(new Vector3(0,Random.Range(-180,180),0)));
            pitObj.transform.SetParent(transform);
        }
        else if (tileType == TileType.Conveyor)
        {
            arrowObj = Instantiate(arrowObj, transform.position + Vector3.up, Quaternion.identity);
            arrowObj.transform.SetParent(transform);

            if (direction == Direction.Right)
            {
                arrowObj.transform.forward = Vector3.right;
            }
            else if (direction == Direction.Down)
            {
                arrowObj.transform.forward = -Vector3.forward;
            }
            else if (direction == Direction.Left)
            {
                arrowObj.transform.forward = -Vector3.right;
            }
        }
        else if (tileType == TileType.Goal)
        {
//            Debug.Log(name);
            GetComponent<Renderer>().material = goalMaterial;
            
        }
        
    }


    public void ClearBarrier()
    {
        tileType = TileType.Normal;

        if (barrierObj != null)
        {
            Destroy(barrierObj);
            Instantiate(barrierExplodeFX, transform.position + Vector3.up, Quaternion.identity);
        }
    }

    public void ClearSwitch()
    {
        tileType = TileType.Normal;

        if (switchObj != null)
        {
            Instantiate(switchFX, transform.position + Vector3.up, Quaternion.identity);
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
