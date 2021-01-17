using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoolBehaviour : MonoBehaviour
{
    [SerializeField] private List<ObstacleBehaviour> obstacles;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform resetMarkPosition;

    [SerializeField, Range(0, 10f)] private float randomizeRange;

    private void Start()
    {
        obstacles.ForEach( obstacle =>
        {
            RandomizeObstacleHeight(obstacle);
        });   
    }

    private void Update()
    {
        obstacles.ForEach( obstacle =>
        {
            if (obstacle.transform.position.x < resetMarkPosition.transform.position.x)
            {
                OnReachRestMark(obstacle);
            }
        });
    }

    private void OnReachRestMark(ObstacleBehaviour obstacleBehaviour)
    {
        obstacleBehaviour.transform.position = startPosition.position;
        RandomizeObstacleHeight(obstacleBehaviour);
    }

    private void RandomizeObstacleHeight(ObstacleBehaviour obstacleBehaviour)
    {
        obstacleBehaviour.transform.position += Vector3.up * Random.Range(-randomizeRange, randomizeRange);
    }
}
