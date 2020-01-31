using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] Transform patrolParent;
    [SerializeField] float speed;

    public enum PatrolType { Random, Incremental };

    public PatrolType patrolType;

    Transform[] patrolPoints;

    int index = 0;
    
    private void Start()
    {
        
        patrolPoints = new Transform[patrolParent.childCount];

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = patrolParent.GetChild(i);
        }

        index = Random.Range(0, patrolPoints.Length);
        transform.position = patrolPoints[index].position;
    }

    private void Update()
    {

        if( Vector3.Distance(transform.position, patrolPoints[index].position) < Mathf.Epsilon)
        {
            if(patrolType == PatrolType.Random)
            {
                index = Random.Range(0, patrolPoints.Length);
            }
            else
            {
                index = ( index + 1 ) % patrolPoints.Length ;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[index].position, speed * Time.deltaTime);
    }


}
