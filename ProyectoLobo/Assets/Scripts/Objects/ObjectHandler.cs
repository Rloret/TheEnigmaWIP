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
        //si el objeto que nos manda el mensaje es el que queremos, lo colocamos en el centro del personaje.
        //el tema de si ya tenemos un objeto lo tendria que manjear el arbol de prioridades, esto solo se encarga
        //de reemplazar o poner un objeto donde estaba el abnterior.
        ObjectData? possibleObject = new ObjectData(colliderType, g.transform.position);
        if (desiredObjectData == possibleObject )
        {
            CurrentObject.image.sprite = g.GetComponent<SpriteRenderer>().sprite;
            if (CurrentObject.image.color.a < 0.3f)
                CurrentObject.image.color = Color.white;
           
            Destroy(g);
            possibleObject = null;
        }

    }
}
