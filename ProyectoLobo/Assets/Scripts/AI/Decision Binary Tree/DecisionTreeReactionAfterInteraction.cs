using UnityEngine;
using System.Collections;

public class DecisionTreeReactionAfterInteraction : DecisionTreeNode {

    // public DecisionTreeNode root;

    public ActionAttack root;


    private Action actionNew;
    private Action actionOld;

    void Awake() { }
    public override void Start() {
        root = gameObject.AddComponent<ActionAttack>();
        Debug.Log("añado script ataque");
        actionNew = root;
    }


    public override DecisionTreeNode MakeDecision()
    {
        Debug.Log("llamo a root.makeDecision()");
        return root.MakeDecision();
    }

    void Update() {
        Debug.Log("Entro en update");
        actionNew.activated = false;
        actionOld = actionNew;
        actionNew = root.MakeDecision() as Action;

        if (actionNew == null) {
            actionNew = actionOld;
        }
        actionNew.activated = true;
        Debug.Log("fin update");

    }
}
