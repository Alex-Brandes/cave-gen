using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    public GameObject mapGen;
    Animator anim;
    int[,] map;
    Vector2 walkTo;
    Vector2 turnTo;
    float turnSpeed = 2.0f;
    public float speed = 1.0f;
    bool looksAtTarget;
    bool isAtTarget;
    //private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        map = mapGen.GetComponent<MapGenerator>().map;

        //Test
        //turnTo = new Vector2(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.z) + 2);
        setWalkTo(new Vector2(32, 32));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAtTarget)
        {
            walk();
        }
       
    }

    void walk()
    {
        if(looksAtTarget)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(walkTo.x, 0, walkTo.y), step);
        }
        else
        {
            lookAt();
        }
        if(walkTo == new Vector2(this.transform.position.x, this.transform.position.z))
        {
            isAtTarget = true;
        }

    }

    void lookAt()
    {
        Debug.Log("tries turning");
        // Determine which direction to rotate towards
        Vector3 targetDirection = new Vector3(walkTo.x, 0, walkTo.y) - this.transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = turnSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        if(Vector2.Angle(new Vector2(this.transform.forward.x, this.transform.forward.z), walkTo - new Vector2(this.transform.position.x, this.transform.position.z)) < 1)
        {
            Debug.Log(Vector2.Angle(new Vector2(this.transform.forward.x, this.transform.forward.z), walkTo));
            looksAtTarget = true;
        }

    }
    void setWalkTo(Vector2 walkTo)
    {
        this.walkTo = walkTo;
        looksAtTarget = false;
        isAtTarget = false;
    }


}
