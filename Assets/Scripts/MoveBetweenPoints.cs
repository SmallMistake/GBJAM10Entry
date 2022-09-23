using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    public Transform movingObject;
    public Transform destinationHolder;
    public float speed;

    public bool loopBackwards;
    private bool headingForwards;

    private List<Transform> destinations = new List<Transform>();
    private int currentDestination;

    private float waitAtEnd = 1f;

    bool stopped = false;


    private void Awake()
    {
        foreach (Transform destination in destinationHolder)
        {
            destinations.Add(destination);
        }
    }


    private void OnEnable()
    {
        stopped = false;
        currentDestination = 0;
        movingObject.transform.position = destinations[currentDestination].position;
        currentDestination++;
        headingForwards = true;
    }


    private void Update()
    {
        if (!stopped)
        {
            if (movingObject.transform.position == destinations[currentDestination].position)
            {
                if (headingForwards == true)
                {  //Heading forwards
                    currentDestination++;
                    if (currentDestination >= destinations.Count)
                    {
                        if (loopBackwards) //Loop backward;
                        {
                            StartCoroutine(WaitAtEnd());
                            headingForwards = false;
                            currentDestination -= 2;
                        }
                        else //Restart at begining
                        {
                            currentDestination = 0;
                            movingObject.transform.position = destinations[currentDestination].position;
                            currentDestination = 1;
                        }
                    }
                }
                else  //Heading backwards
                {
                    currentDestination--;
                    if (currentDestination < 0)
                    {
                        if (loopBackwards) //Loop backward;
                        {
                            StartCoroutine(WaitAtEnd());
                            headingForwards = true;
                            currentDestination += 2;
                        }
                        else //Restart at begining
                        {
                            currentDestination = destinations.Count;
                            movingObject.transform.position = destinations[currentDestination].position;
                            currentDestination = destinations.Count - 1;
                        }
                    }
                }
            }
            float step = speed * Time.deltaTime;
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, destinations[currentDestination].position, step);
        }
    }

    IEnumerator WaitAtEnd()
    {
        stopped = true;
        yield return new WaitForSeconds(waitAtEnd);
        stopped = false;
    }
}
