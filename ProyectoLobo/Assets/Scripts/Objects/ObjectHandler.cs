using UnityEngine;
using System.Collections.Generic;
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
    //public ObjectData desiredObjectData;

    public static int pickRadius;
    public int radius;


	public GameObject currentObject = null;

    //public ObjectType lastObjecttype;

	public GameObject desiredObject;
    public GameObject positionGO;
	private AIPersonality personality;
    private PlayerPersonality playerPersonality;
    private AgentPositionController agentPositionControllerScript;
    private Dictionary<string, Vector3?> objSeenBefore;

    #endregion

    #region private domain
    public bool hasObject = false;
    private VisibilityConeCycleIA cycleIA;
    private VisibilityConeCycle cyclePlayer;

    #endregion

    void Start()
    {
        if(this.tag == "IA")
        {
            cycleIA = this.GetComponent<VisibilityConeCycleIA>();
			personality = this.GetComponent<AIPersonality> ();
            objSeenBefore = personality.myMemory.objectsSeenBefore;     
        }
        else if (this.tag == "Player")
        {
            cyclePlayer = this.GetComponent<VisibilityConeCycle>();
            playerPersonality = this.GetComponent<PlayerPersonality>();
        }
        agentPositionControllerScript = this.GetComponent<AgentPositionController>();
    }

	void OnTriggerEnter2D (Collider2D coll) {

        if (coll.gameObject == desiredObject)
        {
           if (currentObject) {
                currentObject.layer = LayerMask.NameToLayer("Objects");
                //Debug.Log( " Añadiendo a VisibleElements : " + currentObject.name);
                if(!VisibleElements.visibleGameObjects.Contains(currentObject))
                {
                    VisibleElements.visibleGameObjects.Add(currentObject);
                    Debug.Log("Estoy añadiendo a visible gameObjects: " + currentObject);
                }
                    
                
            }

            currentObject = coll.gameObject;
            if (this.tag == "IA"){
                //Debug.Log("Estoy colisionando con: " + currentObject);
                if (coll.gameObject.name == "Medicalaid")
                {
                    personality.health = 100;
                    Debug.Log("Me debería estar curándome");
                    hasObject = false;
                }
                else
                {
                    personality.myObject = GO2ObjectType(coll.gameObject.name);
                    hasObject = true;
                    currentObject.layer = LayerMask.NameToLayer("ObjectsHandler");
                    //Debug.Log("Añadiendo a VisibleElements : " + currentObject.name);
                    if (!VisibleElements.visibleGameObjects.Contains(currentObject))
                    {
                        VisibleElements.visibleGameObjects.Add(currentObject);
                        Debug.Log("Estoy añadiendo a visible gameObjects: " + currentObject);
                    }
                        
                }
                if (objSeenBefore.ContainsKey(coll.name)) //sólo si es una IA recuerda el objeto
                {
                    objSeenBefore.Remove(coll.name);
                }
            }
            else
            {
                
                if (coll.gameObject.name == "Medicalaid")
                {
                    playerPersonality.health = 100;
                    hasObject = false;
                }
                else {
                    playerPersonality.myObject = GO2ObjectType(coll.gameObject.name);
                    hasObject = true;
                    currentObject.layer = LayerMask.NameToLayer("ObjectsHandler");
                    VisibleElements.visibleGameObjects.Remove(currentObject);
                }     
            }
           
            ResetSkills();
            Improvement();
            desiredObject = null;
        }
        else
        {
            if (desiredObject && desiredObject.name == coll.gameObject.name) //SI RECUERDA UN OBJETO
            {
                Debug.Log("Recordadndo objeto");
                if (coll.gameObject.name == "Medicalaid")
                {
                    personality.health = 100;
                }
                else //SIEMPRE SE RECUERDA EL BOTIQUIN
                {
                    objSeenBefore.Remove(coll.name);
                }

                desiredObject = null;

            }
        }
	}

    void Update()
    {
        pickRadius = radius;
		if (hasObject) {
			pickupObject (currentObject);
            
		}

    }

	public void pickupObject(GameObject takedObject) {
        takedObject.transform.position =  positionGO.transform.position;

    }

    private void ResetSkills()
    {

        agentPositionControllerScript.maxLinearVelocity = 80;
        if (this.tag == "IA")
        {
            personality.attack = 10;
            personality.defense = 1.0f;
            cycleIA.changeRadius(1.0f);
            //Debug.Log("reseteo el radio");
        }
        else
        {
            playerPersonality.attack = 10;
            playerPersonality.defense = 1.0f;
            cyclePlayer.changeRadius(1.0f);
        }
    }

    private void Improvement()
    {
        switch (currentObject.name)
        {
            case "Axe":
                if (this.tag == "IA")
                    personality.attack = 15;
                else
                    playerPersonality.attack = 15;
                break;
            case "Boots":
                agentPositionControllerScript.maxLinearVelocity = 200;
                break;
            case "Flashlight":
                if (this.tag == "IA")
                {
                    cycleIA.changeRadius(1.25f);
                    //Debug.Log("cambio el radio");
                }
                else if (this.tag == "Player")
                {
                    cyclePlayer.changeRadius(1.25f);
                }
                break;
            case "Shield":
                if (this.tag == "IA")
                    personality.defense = 0.5f;
                else
                    playerPersonality.defense = 0.5f;
                break;
        }
    }

	private ObjectType GO2ObjectType(string ob) {
        ObjectType type;
		switch (ob) {
		case "Shield":
			type =  ObjectType.SHIELD;
			break;
		case "Axe":
			type = ObjectType.AXE;
			break;
		case "Flashlight":
			type = ObjectType.FLASHLIGHT;
			break;
		case "Boots":
			type = ObjectType.BOOTS;
			break;
		default:
			type = ObjectType.NONE;
			break;
		}

        return type;
	}
		
   /* public void youAreOnA(GameObject g)
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
                instantiateGO(lastObjecttype, this.transform.position);
                lastObjecttype = desiredObjectData.type;
            }
            else
            {
                lastObjecttype = desiredObjectData.type;
            }
            if(this.tag == "IA")
            {
                this.GetComponent<AIPersonality>().myObject = desiredObjectData.type ;
                cycleIA.visibleGameobjects.Clear();
            }
            CurrentObject.image.sprite = g.GetComponent<SpriteRenderer>().sprite;

            if (CurrentObject.image.color.a < 0.3f)
                CurrentObject.image.color = Color.white;

            VisibleElements.visibleGameObjects.Remove(g);

            //Destroy(g);
            g.SetActive(false);
            possibleObject = null;
            hasObject = true;
            
        }

    }
    private void instantiateGO( ObjectType type, Vector3 position)
    {
        GameObject auxGO = new GameObject();
        switch (type)
        {
            case ObjectType.NONE:
               
                break;

            case ObjectType.AXE:
                Destroy(auxGO);
                auxGO=(Instantiate(Resources.Load("Prefabs/Objects/Axe") as GameObject, position, Quaternion.identity) as GameObject);
                auxGO.name = "Axe";
                break;
            case ObjectType.SHIELD:
                Destroy(auxGO);
                auxGO = (Instantiate(Resources.Load("Prefabs/Objects/Shield") as GameObject, position, Quaternion.identity) as GameObject);
                auxGO.name = "Shield";
                break;
            case ObjectType.FLASHLIGHT:
                Destroy(auxGO);
                auxGO = (Instantiate(Resources.Load("Prefabs/Objects/Flashlight") as GameObject, position, Quaternion.identity) as GameObject);
                auxGO.name = "Flashlight";
                break;
            case ObjectType.MEDICALAID:
                break;
            case ObjectType.BOOTS:
                Destroy(auxGO);
                auxGO = (Instantiate(Resources.Load("Prefabs/Objects/Boots") as GameObject, position, Quaternion.identity) as GameObject);
                auxGO.name = "Boots";
                break;
            case ObjectType.JUMPSUIT:
                break;
            default:
                break;
        }
        VisibleElements.visibleGameObjects.Add(auxGO);      
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

    }*/

}
