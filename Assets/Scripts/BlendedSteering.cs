using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class BlendedSteering
{
    public BehaviorAndWeight[] behaviors;
    public float maxAcceleration = 1f;
    public float maxRotation = 5f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();
       
        foreach (BehaviorAndWeight b in behaviors)
        {
           
            if (b.behavior.getSteering() != null)
            {
                result.linear += b.behavior.getSteering().linear * b.weight;
                result.angular += b.behavior.getSteering().angular * b.weight;
            }
        }
        result.linear = result.linear.normalized * maxAcceleration;
        float angularAcceleration = Mathf.Abs(result.angular);

        if(angularAcceleration > maxRotation)
        {
            result.angular /= angularAcceleration;
            result.angular *= maxRotation;
        }
        return result;
    }

}*/


public class BlendedSteering
{

    public BehaviorAndWeight[] behaviors;

    float maxAcceleration = 1f; // 1
    float maxRotation = 5f;

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        // accumulate all accelrations
        foreach (BehaviorAndWeight b in behaviors)
        {
            SteeringOutput s = b.behavior.getSteering();
            if (s != null)
            {
                result.angular += s.angular * b.weight;
                result.linear += s.linear * b.weight;
            }
        }

        // crop the result and return
        //result.linear = Mathf.Max(result.linear, maxAcceleration);
        //result.angular = Mathf.Max(result.angular, maxRotation);
        result.linear = result.linear.normalized * maxAcceleration;
        float angularAcceleration = Mathf.Abs(result.angular);
        if (angularAcceleration > maxRotation)
        {
            result.angular /= angularAcceleration;
            result.angular *= maxRotation;
        }

        return result;
    }

}

