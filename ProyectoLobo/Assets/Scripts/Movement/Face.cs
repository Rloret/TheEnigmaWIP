using UnityEngine;
using System.Collections;

public class Face : Align
{
    protected GameObject targetAux;
    //private GameObject target;

    void OnDestroy()
    {
        //Debug.Log("destruyo al target (no deberia destruir la tile) ");
        //Destroy(target);
    }

    ///<summary>
    ///Computes target's orientation and then delegates to Align.
    /// A.K.A lookAt().
    ///</summary>
    public override SteeringOutput GetSteering()
    {
        SteeringOutput steering;
        if (target != null)
        {


            targetAux = target;

            target = new GameObject();
            target.transform.rotation = targetAux.transform.rotation;
            target.transform.position = targetAux.transform.position;

            Vector3 direction = target.transform.position - this.transform.position;

            if (direction.magnitude > 0.0f)
            {
                float targetOrientation = Mathf.Atan2(-direction.x, direction.y);
                targetOrientation *= Mathf.Rad2Deg;
                target.transform.rotation = Quaternion.Euler(0, 0, targetOrientation);
            }

            steering = base.GetSteering();
            //targetAux.transform.rotation = target.transform.rotation;
            DestroyImmediate(target);
            target = targetAux;
            return steering;
        }
        else
        {
            return new SteeringOutput() ;
        }
      
    }

    public override void OnDrawGizmos()
    {
        if (this != null && target != null)
        {
            Vector3 direction = target.transform.position - this.transform.position;
            float targetOrientation = 0;
            if (direction.magnitude > 0.0f)
            {
                targetOrientation = Mathf.Atan2(direction.x, direction.y);
                targetOrientation *= Mathf.Rad2Deg;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawRay(target.transform.position, base.GetOriAsVec(targetOrientation) * 30);
            base.OnDrawGizmos();
        }
    }


}
