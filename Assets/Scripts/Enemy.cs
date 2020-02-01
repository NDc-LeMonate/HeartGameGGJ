using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform patrolParent;

    public enum PatrolType { Random, Incremental, PingPong };

    public PatrolType patrolType;

    Transform[] patrolPoints;

    public int startingIndex;

    int index = 0;

    public bool isMoving = false;

    Vector3 dist;

    public bool isAscending;

    private void Start()
    {
        GameController.instance.enemies.Add(this);

        patrolPoints = new Transform[patrolParent.childCount];

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = patrolParent.GetChild(i);
        }

        index = startingIndex;
        transform.position = patrolPoints[index].position;
    }

    private void Update()
    {
        if (isMoving)
        {
            Move();
        }

    }

    public void SetDestination()
    {
        if (patrolType == PatrolType.PingPong)
        {
            if (isAscending)
                index++;
            else
                index--;


            if (index == patrolPoints.Length - 1)
            {
                isAscending = false;
            }
            else if (index == 0)
            {
                isAscending = true;

            }

        }
        else if (patrolType == PatrolType.Incremental)
        {
            index = (index + 1) % patrolPoints.Length;

        }

        isMoving = true;
        dist = patrolPoints[index].position;

    }

    void Move()
    {
        transform.position = Vector3.Lerp(transform.position, dist, 5 * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolPoints[index].position) < 0.01f)
        {
            transform.position = Utility.NearestVector(transform.position);
            isMoving = false;
            GameController.instance.isPlayerTurn = true;
        }
    }


}
