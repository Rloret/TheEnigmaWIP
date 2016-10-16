using UnityEngine;
using System.Collections;

public class Face : Align
{
    protected GameObject targetAux;
    //private GameObject target;

  /*  public override void Awake()
    {
        base.Awake();
        targetAux = target;
        target = new GameObject();
        //target.AddComponent<AgentPositionController>();
        Debug.Log("ahora Awake de face");
    }*/

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
        targetAux = target;
        target = new GameObject();
        target.transform.rotation = targetAux.transform.rotation;
        target.transform.position = targetAux.transform.position;

        //Vector2 direction = targetAux.GetComponent<AgentPositionController>().position - new Vector2(transform.position.x, transform.position.y);
        Vector3 direction = target.transform.position - this.transform.position;

        Debug.Log("ahora getsteering de face");
        if (direction.magnitude > 0.0f)
        {
            float targetOrientation = Mathf.Atan2(-direction.x,direction.y);
            targetOrientation *= Mathf.Rad2Deg;
            target.transform.rotation =Quaternion.Euler(0,0, targetOrientation);
        }

        steering= base.GetSteering();
        DestroyImmediate(target);
        target = targetAux;
        return steering;

       
    }
}
