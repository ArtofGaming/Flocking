using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendedFlocking : Kinematic
{
    public bool avoidObstacles = false;
    public GameObject myCohereTarget;
    public BlendedSteering mySteering;
    public PrioritySteering myAdvancedSteering;
    public Kinematic[] boids;
    public GameObject[] gBoids;

    // Start is called before the first frame update
    void Start()
    {
        Separation separate = new Separation();
        separate.character = this;
        
        gBoids = GameObject.FindGameObjectsWithTag("boid");
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

        //PrioritySteering Setup
        ObstacleAvoid myAvoid = new ObstacleAvoid();
        myAvoid.character = this;
        myAvoid.target = myCohereTarget;
        myAvoid.flee = true;

        BlendedSteering myHighPrioritySteering = new BlendedSteering();
        myHighPrioritySteering.behaviors = new BehaviorAndWeight[1];
        myHighPrioritySteering.behaviors[0] = new BehaviorAndWeight();
        myHighPrioritySteering.behaviors[0].behavior = myAvoid;
        myHighPrioritySteering.behaviors[0].weight = 1f;

        myAdvancedSteering = new PrioritySteering();
        myAdvancedSteering.groups = new BlendedSteering[2];
        myAdvancedSteering.groups[0] = new BlendedSteering();
        myAdvancedSteering.groups[0] = myHighPrioritySteering;
        myAdvancedSteering.groups[1] = new BlendedSteering();
        myAdvancedSteering.groups[1] = mySteering;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        if (!avoidObstacles)
        {
            steeringUpdate = mySteering.getSteering();
        }
        else
        {
            steeringUpdate = myAdvancedSteering.getSteering();
        }
        Debug.Log(steeringUpdate.linear);
        base.Update();
    }



}
