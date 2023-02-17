using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedFlocking : Kinematic
{
    public GameObject myCohereTarget;
    public BlendedSteering mySteering;
    public Kinematic[] boids;

    //Vector3 centerOfMass;
    //Vector3 flockVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Separation separate = new Separation();
        separate.character = this;
        
        GameObject[] gBoids = GameObject.FindGameObjectsWithTag("boid");
        boids = new Kinematic[gBoids.Length];
        int j = 0;
        for (int i = 0; i < gBoids.Length; i++)
        {
            if (gBoids[i] == this)
            {
                continue;
            }
            boids[j++] = gBoids[i].GetComponent<Kinematic>();
        }
        separate.targets = boids;
        Arrive cohere = new Arrive();
        cohere.character = this;
        cohere.target = myCohereTarget;

        LookWhereGoing myRotateType = new LookWhereGoing();
        myRotateType.character = this;

        //Vector3 centerOfMass = Vector3.zero;
        //Vector3 flockVelocity = Vector3.zero;

        mySteering = new BlendedSteering();
        mySteering.behaviors = new BehaviorAndWeight[3];
        mySteering.behaviors[0] = new BehaviorAndWeight();
        mySteering.behaviors[0].behavior = separate;
        mySteering.behaviors[0].weight = 1f;
        mySteering.behaviors[1] = new BehaviorAndWeight();
        mySteering.behaviors[1].behavior = cohere;
        mySteering.behaviors[1].weight = 1f;
        mySteering.behaviors[2] = new BehaviorAndWeight();
        mySteering.behaviors[2].behavior = myRotateType;
        mySteering.behaviors[2].weight = 1f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //Debug.Log(mySteering.getSteering().linear);
        /*foreach (Kinematic boid in boids)
        {
            centerOfMass += boid.transform.position;
            flockVelocity += boid.linearVelocity;
        }
        centerOfMass /= boids.Length;
        flockVelocity /= boids.Length;
        myCohereTarget.transform.position = centerOfMass;
        myCohereTarget.GetComponent<Kinematic>().linearVelocity = flockVelocity;*/

        steeringUpdate = new SteeringOutput();
        steeringUpdate = mySteering.getSteering();
        Debug.Log(steeringUpdate.linear);
        base.Update();
    }



}
