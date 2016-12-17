using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResponseController : MonoBehaviour {

    // Use this for initialization
    public GameObject Reactions;
    public Sprite[] Actions;
    public enum responseEnum{
        EVADE,OFFER,GROUP,ATTACK,OK,NOTOK,QUESTIONMARK,NONE
    }

    private Dictionary<responseEnum, Sprite> ActionsAndConditionals;

    void Start()
    {
        responseEnum auxenum=responseEnum.NONE;
        ActionsAndConditionals = new Dictionary<responseEnum, Sprite>();
        foreach (var Action in Actions)
        {
            switch (Action.name)
            {
                case "evade":
                    auxenum = responseEnum.EVADE;
                    break;
                case "group":
                    auxenum = responseEnum.GROUP;
                    break;
                case "offer":
                    auxenum = responseEnum.OFFER;
                    break;
                case "attack":
                    auxenum = responseEnum.ATTACK;
                    break;
                case "ok":
                    auxenum = responseEnum.OK;
                    break;
                case "notOk":
                    auxenum = responseEnum.NOTOK;
                    break;
                case "question":
                    auxenum = responseEnum.QUESTIONMARK;
                    break;
                case "none":
                default:
                    auxenum = responseEnum.NONE;
                    break;
            }
            ActionsAndConditionals.Add(auxenum, Action);
        }
    }
    public void configureActions(responseEnum Act,responseEnum Condit, ref Image action,ref Image conditional)
    {
        action.sprite = ActionsAndConditionals[Act];
        conditional.sprite = ActionsAndConditionals[Condit];
    }

    public void spawnReaction(responseEnum Act, responseEnum Condit, GameObject target)
    {
        GameObject whichConversation = Instantiate(Reactions) as GameObject;
        Image[] reactions = whichConversation.GetComponentsInChildren<Image>();
        Image action = reactions[0].name == "WhichAction" ? reactions[1] : reactions[2];
        Image conditional = reactions[0].name == "WhichAction" ? reactions[2] : reactions[1];

        configureActions(Act, Condit, ref action, ref conditional);

        whichConversation.GetComponent<DestroyTimed>().lifeTime = 3f;
        whichConversation.GetComponent<MoveUp>().whoToFollow = target;
        whichConversation.transform.position = target.transform.position;
        whichConversation.transform.parent = target.transform;
    }

}
