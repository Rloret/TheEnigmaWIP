using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectHandler : MonoBehaviour {

    #region public domain
    public enum ObjectType
    {
        NONE,AXE,SHIELD,FLASHLIGHT,MEDICALAID,BOOTS,JUMPSUIT
    };

    [System.Serializable]
    public struct ObjectData
    {
        [SerializeField]
       public ObjectType type;
        [SerializeField]
        Vector3 position;
        public ObjectData(ObjectType type, Vector3 position) {
            this.type = type;
            this.position = position;
        }
        public static bool operator ==(ObjectData object1, ObjectData object2)
        {
           // float distance = Vector3.Distance(object1.position, object2.position);

            if (object1.type ==object2.type ) // idk
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(ObjectData object1, ObjectData object2)
        {
           // float distance = Vector3.Distance(object1.position, object2.position);
            if (object1.type == object2.type/* &&distance < ObjectHandler.pickRadius*/) // idk
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
    public ObjectType lastObjecttype;




    #endregion

    #region private domain
    private bool hasObject = false;

    #endregion
    void Update()
    {
        pickRadius = radius;
    }

    public void youAreOnA(GameObject g)
    {
        string name = g.name;
        Debug.Log(name);
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
            case "Jumpsuit":
                colliderType = ObjectType.JUMPSUIT;
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
            if (hasObject)
            {
                   instantiateGO(lastObjecttype,this.transform.position);

            }
            if(this.tag == "IA")
            {
                this.GetComponent<AIPersonality>().myObject = desiredObjectData.type ;
            }
            CurrentObject.image.sprite = g.GetComponent<SpriteRenderer>().sprite;
            if (CurrentObject.image.color.a < 0.3f)
                CurrentObject.image.color = Color.white;

            VisibleElements.visibleGameObjects.Remove(g);
            Destroy(g);
            possibleObject = null;
            hasObject = true;
            
        }

    }
    private void instantiateGO( ObjectType type, Vector3 position)
    {

        switch (type)
        {
            case ObjectType.NONE:
               
                break;

            case ObjectType.AXE:
                VisibleElements.visibleGameObjects.Add(Instantiate(Resources.Load("Prefabs/Objects/Axe") as GameObject, position, Quaternion.identity) as GameObject);
                break;
            case ObjectType.SHIELD:
                VisibleElements.visibleGameObjects.Add(Instantiate(Resources.Load("Prefabs/Objects/Shield") as GameObject, position, Quaternion.identity)as GameObject);
                break;
            case ObjectType.FLASHLIGHT:
                VisibleElements.visibleGameObjects.Add(Instantiate(Resources.Load("Prefabs/Objects/Flashlight") as GameObject, position, Quaternion.identity) as GameObject);
                break;
            case ObjectType.MEDICALAID:
                break;
            case ObjectType.BOOTS:
                VisibleElements.visibleGameObjects.Add(Instantiate(Resources.Load("Prefabs/Objects/Boots") as GameObject, position, Quaternion.identity) as GameObject);
                break;
            case ObjectType.JUMPSUIT:
                break;
            default:

                break;
        }
      
    }

    public void setDesiredGameObject(GameObject desired)
    {
        string name =desired.name;

        switch (name)
        {
            case "Axe":
                desiredObjectData =new ObjectData( ObjectType.AXE,desired.transform.position);
                break;
            case "Shield":
                desiredObjectData = new ObjectData(ObjectType.SHIELD, desired.transform.position);
                break;
            case "Flashlight":
                desiredObjectData = new ObjectData(ObjectType.FLASHLIGHT, desired.transform.position);
                break;
            case "Medicalaid":
                desiredObjectData = new ObjectData(ObjectType.MEDICALAID, desired.transform.position);
                break;
            case "Boots":
                desiredObjectData = new ObjectData(ObjectType.BOOTS, desired.transform.position);
                break;
            case "Jumpsuit":
                desiredObjectData = new ObjectData(ObjectType.JUMPSUIT, desired.transform.position);
                break;
            default:
                desiredObjectData = new ObjectData(ObjectType.AXE, desired.transform.position);
                break;
        }

    }
}
