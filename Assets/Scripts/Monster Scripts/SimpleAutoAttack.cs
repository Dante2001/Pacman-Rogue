using UnityEngine;
using System.Collections;

public class SimpleAutoAttack : MonoBehaviour {

    public float power;
    public float speedMultiplier;
    public float range;
    public float cooldown;
    public bool alwaysAttack;
    public string[] nameOfTargets;
    public int layerMask;
    public string aName = "Attack";

    private Vector2 direction;
    private Vector3 lastPos;

    private enum STATE
    {
        ON_CD,
        OFF_CD
    };

    private STATE state;

	// Use this for initialization
	void Start () {
        state = STATE.OFF_CD;
        lastPos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        direction = (this.transform.position - lastPos).normalized;
        if (this.transform.position != lastPos)
            lastPos = this.transform.position;
        Debug.DrawLine(this.transform.position, this.transform.position + (Vector3)direction * range, Color.red);
        switch (state)
        {
            case (STATE.ON_CD):
                // wait till off CD, ie do nothing
                break;
            case (STATE.OFF_CD):
                if (alwaysAttack) Attack();
                break;
            default: break;
        }       
	}

    public void Attack()
    {
        // start attack animation
        Collider2D col = new Collider2D();
        col.name = aName;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(this.transform.position, direction, range, layerMask);
        if (hit.collider != null)
        {
            int len = nameOfTargets.GetLength(0);
            for (int i = 0;  i < len; i++)
            {
                if (hit.collider.name == nameOfTargets[i])
                {
                    hit.collider.GetComponent<Collider2D>().SendMessageUpwards
                        ("OnTriggerEnter2D", col, SendMessageOptions.DontRequireReceiver);
                    state = STATE.ON_CD;
                    break;
                }
            }
        }
    }

    public void AttackAnimationOver()
    {
        StartCoroutine(CooldownTimer());
    }

    private IEnumerator CooldownTimer()
    {
        yield return new WaitForSeconds(cooldown);
        state = STATE.OFF_CD;
    }

}
