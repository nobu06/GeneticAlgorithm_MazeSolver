using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    private int dnaLength = 1;
    public DNA dna;

    public GameObject eye;
    private bool seeWall = true;


    public float rayLength;
    private bool alive = true;
    public float distanceTravelled = 0f;
    private int layerMaskWall;

    public GameObject startingPoint;
    public float maxDistanceFromOrigin;
    //public float maxDistanceToDestination;
    //public GameObject destinationPoint;


    private int numActions = 7;

    private float translateModifier = 0.1f;

    private void OnDestroy()
    {
        Destroy(gameObject);        // c?
    }

    private void OnCollisionEnter(Collision col)
    {
        // get the amount it should reflect by
        if (col.gameObject.tag == "Wall")
        {
            // use raycast




            //int actionNum = dna.GetGene(0);

            //float reflectionNormal = CalculateRotationDirection(actionNum);

            ////// flip by chance
            //if (Random.Range(0, 2) < 1)
            //{
            //    reflectionNormal *= -1;
            //}

            //Vector3 newDirection = Vector3.Reflect(transform.forward, col.contacts[0].normal);

            //// may be buggy
            ////            newDirection = Quaternion.Euler(0f, reflectionNormal, 0f) * newDirection;       // https://answers.unity.com/questions/46770/rotate-a-vector3-direction.html


            //// rotate bot to a new direction
            //transform.rotation = Quaternion.LookRotation(newDirection);

        }
        else if (col.gameObject.tag == "Dead")
        {
            alive = false;

            maxDistanceFromOrigin = 0;      // to not consider those that died as being fit
        }
    }


    private float CalculateRotationDirection(int actionNum)
    {
        float rotationAngle = 0f;

        switch (actionNum)
        {
            case 0:
                rotationAngle = 10f;
                break;

            case 1:
                rotationAngle = 30f;
                break;

            case 2:
                rotationAngle = 60f;
                break;

            case 3:
                rotationAngle = 90f;
                break;

            case 4:
                rotationAngle = 120f;
                break;

            case 5:
                rotationAngle = 150f;
                break;

            case 6:
                rotationAngle = 180f;
                break;
        }

        //newRotation = new Vector3(0f, rotationAngle, 0f);

        return rotationAngle;
    }


    public void Init()
    {

        dna = new DNA(dnaLength, numActions);

        alive = true;

        maxDistanceFromOrigin = 0f;

        SetStartingPoint();

    }

    private void SetStartingPoint()
    {
        //destinationPoint = GameObject.FindWithTag("Destination");
        startingPoint = GameObject.FindWithTag("StartingPoint");
    }

	private void Start()
	{
        layerMaskWall = LayerMask.NameToLayer("Wall");
	}


	private void Update()
    {
        if (!alive)
            return;

        // make it move forward
        transform.Translate(Vector3.forward * translateModifier);

        Ray ray = new Ray(eye.transform.position, eye.transform.forward);
        Debug.DrawRay(eye.transform.position, eye.transform.forward * rayLength);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))//, layerMaskWall))
        {
            if (hit.collider.tag == "Wall")
            {
                Debug.Log("Hit a wall");


                int actionNum = dna.GetGene(0);

                float dnaReflectionOffset = CalculateRotationDirection(actionNum);

                //// flip by chance
                if (Random.Range(0, 2) < 1)
                {
                    dnaReflectionOffset *= -1;
                }

                Vector3 newDirection = Vector3.Reflect(transform.forward, hit.normal);  // c?

                // may be buggy
                newDirection = Quaternion.Euler(0f, dnaReflectionOffset, 0f) * newDirection;       // https://answers.unity.com/questions/46770/rotate-a-vector3-direction.html


                // rotate bot to a new direction
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }


        // calculate the max distance       // not a performant solution though?
        float distanceFromOrigin = Vector3.Distance(transform.position, startingPoint.transform.position);
        if (distanceFromOrigin > maxDistanceFromOrigin)
        {
            maxDistanceFromOrigin = distanceFromOrigin;
        }
    }
}


/*


            int actionNum = dna.GetGene(0);

            float reflectionNormal = CalculateRotationDirection(actionNum);

            //// flip by chance
            if (Random.Range(0, 2) < 1)
            {
                reflectionNormal *= -1;
            }

            Vector3 newDirection = Vector3.Reflect(transform.forward, col.contacts[0].normal);

            // may be buggy
            //            newDirection = Quaternion.Euler(0f, reflectionNormal, 0f) * newDirection;       // https://answers.unity.com/questions/46770/rotate-a-vector3-direction.html


            // rotate bot to a new direction
            transform.rotation = Quaternion.LookRotation(newDirection);

*/

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Brain : MonoBehaviour
//{


//    public DNA dna;
//    private int dnaLength = 1;

//    private bool alive = true;

//    public GameObject startingPoint;
//    public float maxDistanceFromOrigin;
//    //public float maxDistanceToDestination;
//    //public GameObject destinationPoint;

//    private int numActions = 7;

//    private float translateModifier = 0.1f;

//    private void OnDestroy()
//    {
//        Destroy(gameObject);        // c?
//    }

//    private void OnCollisionEnter(Collision col)
//    {
//        // get the amount it should reflect by
//        if (col.gameObject.tag == "Wall")
//        {
//            int actionNum = dna.GetGene(0);

//            float reflectionNormal = CalculateRotationDirection(actionNum);

//            //// flip by chance
//            if (Random.Range(0, 2) < 1)
//            {
//                reflectionNormal *= -1;
//            }

//            Vector3 newDirection = Vector3.Reflect(transform.forward, col.contacts[0].normal);

//            // may be buggy
////            newDirection = Quaternion.Euler(0f, reflectionNormal, 0f) * newDirection;       // https://answers.unity.com/questions/46770/rotate-a-vector3-direction.html


//            // rotate bot to a new direction
//            transform.rotation = Quaternion.LookRotation(newDirection);


//        }
//        else if (col.gameObject.tag == "Dead")
//        {
//            alive = false;

//            maxDistanceFromOrigin = 0;      // to not consider those that died as being fit
//        }
//    }


//    private float CalculateRotationDirection(int actionNum)
//    {
//        float rotationAngle = 0f;

//        switch(actionNum)
//        {
//            case 0:
//                rotationAngle = 10f;
//                break;
            
//            case 1:
//                rotationAngle = 30f;
//                break;

//            case 2:
//                rotationAngle = 60f;
//                break;

//            case 3:
//                rotationAngle = 90f;
//                break;

//            case 4:
//                rotationAngle = 120f;
//                break;

//            case 5:
//                rotationAngle = 150f;
//                break;

//            case 6:
//                rotationAngle = 180f;
//                break;
//        }

//        //newRotation = new Vector3(0f, rotationAngle, 0f);

//        return rotationAngle;
//    }


//    public void Init()
//    {

//        dna = new DNA(dnaLength, numActions);

//        alive = true;

//        maxDistanceFromOrigin = 0f;

//        SetDestinationPoint();

//    }

//    private void SetDestinationPoint()
//    {
//        //destinationPoint = GameObject.FindWithTag("Destination");
//        startingPoint = GameObject.FindWithTag("StartingPoint");
//    }


//    private void Update()
//    {
//        if (!alive)
//            return;

//        // make it move forward
//        transform.Translate(Vector3.forward * translateModifier);

//        // calculate the max distance       // not a performant solution though?
//        float distanceFromOrigin = Vector3.Distance(transform.position, startingPoint.transform.position);
//        if (distanceFromOrigin > maxDistanceFromOrigin)
//        {
//            maxDistanceFromOrigin = distanceFromOrigin;
//        }
//    }


//    // will have dif num for each bot. Sth like the following. Perhaps have two.
//    // they will all keep moving forward by default. These num will control how
//    // they will react when they hit a wall.
//    // the DNA / Brain will store how much the bot rotates
//    // 0 - turn left by 30 degrees
//    // 1 - turn left by 60 degrees
//    // 2 - turn left by 90 degrees
//    // 3 - turn left by 120 degrees 
//    // 4
//    // 5
//    // 6



//    // you use NavMesh;
//    // dif genes represent how fast they move - I think



//}



//Log:
/*
 Did:
 - added Navmehs

 - figure out how to make the capsule move around on the plane

 - think about what to use as the gene traits.
   (angle it rotates when it obunces the wall, the speed it moves, etc..)



*/