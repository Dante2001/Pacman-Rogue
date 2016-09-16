using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Monster : Hittable {

    private enum STATE
    {
        RESPAWN,
        DIE,
        ATTACK,
        PATH
    };

    private STATE state;

    private enum PATH_STATE
    {
        FOLLOW,
        NOT_FOLLOW
    };

    private PATH_STATE pathState;

    public float speed;
    public float maxHP;
    public float currentHP;
    public float attackPower;
    public float attackSpeedMultiplier; // 1 is defualt >1 is faster <1 is slower
    public float attackCoolDown;
    public float attackRange;

    public Vector2 respawnPoint;
    public Vector2[] waypoints;
    public float respawnTime;
    public float spotRange;
    public float outOfSpotRange;

    public GameObject player;

    private List<Vector2> path;
    private int pathIndex;

    private Spotter spotter;
    private SimpleAutoAttack attack;
    private AStarPathing astar;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");

        pathIndex = -1;
        path = null;

        state = STATE.PATH;
        pathState = PATH_STATE.NOT_FOLLOW;

        spotter = this.GetComponent<Spotter>();
        spotter.inRange = spotRange;
        spotter.leashRange = outOfSpotRange;
        spotter.target = player;

        attack = this.GetComponent<SimpleAutoAttack>();
        attack.cooldown = attackCoolDown;
        attack.range = attackRange;
        attack.speedMultiplier = attackSpeedMultiplier;
        attack.power = attackPower;
        attack.layerMask = 1 << player.layer;
        // these last two variables were added to make
        // the component slightly more modular
        attack.alwaysAttack = false;
        attack.nameOfTargets = new string[] { player.name };

        astar = this.GetComponent<AStarPathing>();
        astar.grid = GameManager.level;
        astar.validSqrCosts.Add(GameManager.ITEM, 1);
        astar.validSqrCosts.Add(GameManager.COIN, 1);
        astar.CalculatePathForWaypoints(waypoints);
	}
	
	// Update is called once per frame
	void Update () {

        if (spotter.IsTargetSpotted()) pathState = PATH_STATE.FOLLOW;
        else pathState = PATH_STATE.NOT_FOLLOW;

        switch(state)
        {
            case (STATE.PATH):
                Move();
                // this is here so you can attack whenever in range
                if ((player.transform.position - this.transform.position).magnitude <= attackRange)
                    attack.Attack();
                break;
            case (STATE.ATTACK):
                // this state is here so you can attack when colliding with player 
                // but you should not even walk into the player while attacking
                // therefore when colliding with player you do not use Move();
                attack.Attack();
                break;
            case (STATE.DIE):
                //wait for death, ie do nothing
                break;
            case (STATE.RESPAWN):
                //wait for respawn, ie do nothing
                break;
            default: break;
        }
	}

    public override void GetHit(int dmg)
    {
        currentHP -= dmg;
        if (currentHP <= 0)
            Die();
    }

    private void Die()
    {
        this.GetComponent<Collider2D>().enabled = false;
        // start death animation
    }

    public void DieAnimationOver()
    {
        state = STATE.RESPAWN;
        this.GetComponent<SpriteRenderer>().enabled = false;
        Respawn();
    }

    private void Respawn()
    {
        currentHP = maxHP;
        this.transform.position = respawnPoint;
        pathIndex = -1;
        path = null;
        StartCoroutine(RespawnTimer());
    }

    private IEnumerator RespawnTimer()
    {
        yield return new WaitForSeconds(respawnTime);
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<Collider2D>().enabled = true;
        state = STATE.PATH;
    }

    private void Move()
    {
        if (pathIndex == -1 || pathIndex >= path.Count)
            GetNewPath();

        if (((Vector3)path[pathIndex] - this.transform.position).magnitude <= speed * Time.deltaTime)
            pathIndex++;

        this.transform.position += ((Vector3)path[pathIndex] - this.transform.position).normalized * speed * Time.deltaTime;       
    }

    private void GetNewPath()
    {
        if (pathState == PATH_STATE.NOT_FOLLOW) path = astar.GetNextPath();
        // else path = GetPathForTarget(x,y);
        pathIndex = 0;
        path[pathIndex] = new Vector2(path[pathIndex].x * GameManager.tileWidth, path[pathIndex].y * GameManager.tileHeight);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
            state = STATE.ATTACK;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Player")
            state = STATE.PATH;
    }
}
