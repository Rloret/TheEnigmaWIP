using UnityEngine;
using System.Collections;

public class ObjectHandler : MonoBehaviour {

    public enum ObjectType
    {
        NONE,AXE,SHIELD,FLASHLIGHT,MEDICALAID,BOOTS
    };
    [System.Serializable]
    public struct ObjectData
    {
        [SerializeField]
        ObjectType type;
        [SerializeField]
        Vector3 position;
        public ObjectData(ObjectType type, Vector3 position) {
            this.type = type;
            this.position = position;
        }
        public static bool operator ==(ObjectData object1, ObjectData object2)
        {
            float distance = Vector3.Distance(object1.position, object2.position);
            Debug.Log(distance + "distancia o1 : " + object1.position + "distancia o2: " +object2.position + "tipos o1 : " + object1.type + "tipo o2: " + object2.type);
            if (object1.type ==object2.type && distance<ObjectHandler.pickRadius ) // idk
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(ObjectData object1, ObjectData object2)
        {
            float distance = Vector3.Distance(object1.position, object2.position);
            if (object1.type == object2.type &&distance < ObjectHandler.pickRadius) // idk
            {
                return false;
            }
            return true;
        }
    }
    public static int pickRadius;
    public int radius;
    public ObjectData desiredObjectData;

    void Update()
    {
        pickRadius = radius;
    }

    public void youAreOnA(GameObject g)
    {
        string name = g.name;
        ObjectType colliderType;
        switch (name)
        {
            case "Axe":
                colliderType = ObjectType.AXE;
                break;
            case "Shield":
                colliderType = ObjectType.SHIELD;
                break;
            case "Flashlight":
                colliderType = ObjectType.FLASHLIGHT;
                break;
            case "Medicalaid":
                colliderType = ObjectType.MEDICALAID;
                break;
            case "Boots":
                colliderType = ObjectType.BOOTS;
                break;
            default:
                colliderType = ObjectType.NONE;
                break;
        }

        if(desiredObjectData == new ObjectData(colliderType, g.transform.position))
        {
            Destroy(g);
            //pick it up
        }

    }
}
