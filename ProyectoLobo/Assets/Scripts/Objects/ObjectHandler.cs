using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectHandler : MonoBehaviour {

    #region public domain
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
    public ObjectData desiredObjectData;

    public static int pickRadius;
    public int radius;

    public Button CurrentObject;

    #endregion

    #region private domain
    #endregion
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
            CurrentObject.image.sprite = g.GetComponent<SpriteRenderer>().sprite;
            if (CurrentObject.image.color.a < 0.3f)
                CurrentObject.image.color = Color.white;
           
            Destroy(g);

            //pick it up
        }

    }
}
