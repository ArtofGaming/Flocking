using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ObstacleAvoid : Seek
{

    public float avoidDistance = 3f;
    public float lookahead = 5f;

    protected override Vector3 getTargetPosition()
    {

        RaycastHit hit;

        if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookahead))
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * hit.distance, Color.red, 0.5f);
            return hit.point + (hit.normal * avoidDistance);
        }
        else
        {
            Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * lookahead, Color.green, 0.5f);
            return base.getTargetPosition();
        }
    }

}
