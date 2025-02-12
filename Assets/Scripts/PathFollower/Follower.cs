using UnityEngine;
using PathCreation;
using UnityEngine.UIElements;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    [SerializeField] public float speed = 5;

    public float distanceTravelled;

    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
    }


}
