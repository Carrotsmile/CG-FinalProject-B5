using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeSharpPlus;

public class NPCWandererController : MonoBehaviour {

    private CharacterController cc;

    public Transform goToPoint;
    public GameObject participant;

    private BehaviorAgent behaviorAgent;

    // Use this for initialization
    void Start () {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    protected Node BuildTreeRoot()
    {
        Val<Vector3> v = new Val<Vector3>(goToPoint.position);
        Node roam = new Sequence(new LeafTrace("sequence being started"),participant.GetComponent<BehaviorMecanim>().ST_TurnToFace(v), participant.GetComponent<BehaviorMecanim>().Node_GoTo(v));
        return roam;
    }
}
