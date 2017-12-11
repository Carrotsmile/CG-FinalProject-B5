using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TreeSharpPlus;

public class NPCBehaviorTree : MonoBehaviour {

    public List<GameObject> participants;
    public List<Transform> startLocations;
    public List<Transform> patrolLocations;

    public bool suspended;

    private List<LineOfSightTest> lineOfSights;
    private List<AlertChaseBehavior> alertChases;
    private List<ResetGameObject> resetObjects;
    private bool toResume;
    private BehaviorAgent behaviorAgent;

    public bool toReset;
	// Use this for initialization
	void Start () {
        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
        toResume = false;
        lineOfSights = new List<LineOfSightTest>();
        alertChases = new List<AlertChaseBehavior>();
        resetObjects = new List<ResetGameObject>();
        foreach(GameObject part in participants)
        {
            LineOfSightTest l = part.GetComponent<LineOfSightTest>();
            if(l != null)
            {
                lineOfSights.Add(l);
            }
            else
            {
                Debug.Log("line of sight test component not found for: " + part.name);
            }
            AlertChaseBehavior a = part.GetComponent<AlertChaseBehavior>();
            if(a != null)
            {
                alertChases.Add(a);
            }
            else
            {
                Debug.Log("Alert Chase behavior doesn't exist for: " + part.name);
            }
            ResetGameObject r = part.GetComponent<ResetGameObject>();
            if(r != null)
            {
                resetObjects.Add(r);
            }
            else
            {
                Debug.Log("Reset behavior not given for participant: " + part.name);
            }
        }
    }
	
    public void ResetBehaviorAgents()
    {
        Debug.Log("resetBehaviorAgents");
        suspended = false;
        toResume = false;
        behaviorAgent.StopBehavior();
        foreach(ResetGameObject r in resetObjects)
        {
            r.RestoreToStartState();
        }
        behaviorAgent.StartBehavior();
        toReset = false;
    }
	// Update is called once per frame
	void Update () {
        bool see = false;

        foreach(LineOfSightTest l in lineOfSights)
        {
            if(l.hasLineOfSight == true)
            {
                see = true;
            }
        }
        foreach(AlertChaseBehavior a in alertChases)
        {
            if(a.isChase)
            {
                see = true;
            }
        }

        if(!suspended && see)
        {
            suspended = true;
        }
        else if(suspended && !see)
        {
            suspended = false;
        }
        if (suspended)
        {
            behaviorAgent.StopBehavior();
            toResume = true;
        }
        if(!suspended && toResume)
        {
            behaviorAgent.StartBehavior();
            toResume = false;
        }
        if(toReset)
        {
            ResetBehaviorAgents();
        }
	}

    protected Node ST_Approach(GameObject participant, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        //return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
        return participant.GetComponent<BehaviorMecanim>().Node_GoTo(position);
    }

    protected Node BuildTreeRoot()
    {
        List<Node> behaviorNodes = new List<Node>();
        List<Node> returnNodes = new List<Node>();
        for(int i = 0; i < participants.Count; i++)
        {
            //behaviorNodes.Add(new Sequence(ST_Approach(participants[i], patrolLocations[i]), ST_Approach(participants[i], startLocations[i])));
            behaviorNodes.Add(ST_Approach(participants[i], patrolLocations[i]));
            returnNodes.Add(ST_Approach(participants[i], startLocations[i]));
        }
        behaviorNodes.AddRange(returnNodes);
        return new DecoratorLoop(new SequenceShuffle(behaviorNodes.ToArray()));
    }
}
