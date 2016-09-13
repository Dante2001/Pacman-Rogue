using UnityEngine;
using System.Collections;

public class Spotter : MonoBehaviour {

    public float inRange;
    public float leashRange;
    public GameObject target;

    private enum STATE
    {
        NOT_FOUND,
        FOUND
    };

    private STATE state;

	// Use this for initialization
	void Start () {
        state = STATE.NOT_FOUND;
	}
	
	// Update is called once per frame
	void Update () {
        
	    switch (state)
        {
            case (STATE.NOT_FOUND):
                if (CheckIfTargetInRange(inRange)) state = STATE.FOUND;
                break;
            case (STATE.FOUND):
                if (!CheckIfTargetInRange(leashRange)) state = STATE.NOT_FOUND;
                break;
            default: break;
        }
	}

    private bool CheckIfTargetInRange(float range)
    {
        if ((target.transform.position - this.transform.position).magnitude <= range)
            return true;
        return false;
    }

    public bool IsTargetSpotted()
    {
        if (state == STATE.FOUND) return true;
        return false;
    }
}
