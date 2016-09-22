using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AStarPathing : MonoBehaviour {

    public char[,] grid;
    public Dictionary<char,int> validSqrCosts;
    public List<Vector2>[] waypointPaths;
    private float[,] gScore;
    private float[,] fScore;
    private Dictionary<int, int> cameFrom;
    private int currentWaypoint;

    void Start()
    {
        validSqrCosts = new Dictionary<char, int>();
        currentWaypoint = 0;
       
    }

    public void CalculatePathForWaypoints(Vector2[] waypoints)
    {
        waypointPaths = new List<Vector2>[waypoints.GetLength(0)];
        int i;
        for (i = 0; i < waypoints.GetLength(0)-1; i++)
        {
            waypointPaths[i] = AStar(waypoints[i], waypoints[i + 1]);
        }
        //i = waypoints.GetLength(0) - 1;
        waypointPaths[i] = AStar(waypoints[i], waypoints[0]);
        currentWaypoint = -1;
    }

    public List<Vector2> GetNextPath()
    {
        if (currentWaypoint >= waypointPaths.GetLength(0)-1)
            currentWaypoint = -1;
        currentWaypoint++;
        return waypointPaths[currentWaypoint];
    }

    private float Hueristic(Vector2 cur, Vector2 goal)
    {
        return (cur - goal).magnitude;
    }

    private float Cost(Vector2 cur, Vector2 next)
    {
        if (cur.x == next.x && cur.y == next.y) return 0;
        int o1;
        int o2;
        if (!validSqrCosts.TryGetValue(grid[(int)cur.y, (int)cur.x], out o1))
            return 999f;
        if (!validSqrCosts.TryGetValue(grid[(int)next.y, (int)next.x], out o2))
            return 999f;
        return (float)(o1 + o1) / 2f;
    }

    private Vector2 IDtoV2(int id)
    {
        Vector2 v2 = Vector2.zero;
        int x;
        Math.DivRem(id, grid.GetLength(0), out x);
        v2.y = (int)((id - x) / grid.GetLength(0));
        v2.x = x;
        return v2;
    }

    private int V2toID(Vector2 v2)
    {
        return (int)(v2.y * grid.GetLength(0) + v2.x);
    }

    private void Reset()
    {
        gScore = new float[grid.GetLength(0), grid.GetLength(1)];
        fScore = new float[grid.GetLength(0), grid.GetLength(1)];
        for (int y = 0; y < grid.GetLength(0); y ++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                gScore[y, x] = 999;
                fScore[y, x] = 999;
            }
        }
        cameFrom = new Dictionary<int, int>();
    }

    private List<Vector2> AStar(Vector2 start, Vector2 goal)
    {
        Reset();
        gScore[(int)start.y, (int)start.x] = 0;
        //fScore[(int)start.y, (int)start.x] = Hueristic(start, goal);
        Dictionary<int, bool> closed = new Dictionary<int, bool>();
        Heap open = new Heap();
        open.Insert(new HeapValue(start, 0));
        Vector2 current;
        while (open.Count() > 0)
        {         
            current = open.Pop().v2;
            if (current.x == goal.x && current.y == goal.y)
            {
                Debug.Log("FINI");
                return ReconstructPath(current);
            }
            //Debug.Log("CHOOSING:  " + current);
            closed.Add(V2toID(current),true);
            List<Vector2> neighbours = FindValidNeighbours(current);
            bool o;
            int j;
            for (int i = 0; i < neighbours.Count; i++)
            {
                //Debug.Log("Neighbour: " + neighbours[i]);
                int neighbourID = V2toID(neighbours[i]);
                if (closed.TryGetValue(neighbourID, out o))
                    continue;
                float tent_gScore = gScore[(int)current.y, (int)current.x] + Cost(current, neighbours[i]);
                if (!open.Contains(neighbours[i]))
                {
                    open.Insert(new HeapValue(neighbours[i], tent_gScore + Hueristic(neighbours[i], goal)));
                    //Debug.Log("Adding " + neighbours[i] + " to open with value: " + tent_gScore);
                }
                else if (tent_gScore >= gScore[(int)neighbours[i].y, (int)neighbours[i].x])
                    continue;
                if (cameFrom.TryGetValue(neighbourID, out j))
                    cameFrom[neighbourID] = V2toID(current);
                else
                    cameFrom.Add(neighbourID, V2toID(current));
                //Debug.Log(neighbours[i] + " came from " + current);
                gScore[(int)neighbours[i].y, (int)neighbours[i].x] = tent_gScore;
                //fScore[(int)neighbours[i].y, (int)neighbours[i].x] = tent_gScore + Hueristic(neighbours[i], goal);
            }
        }
        return new List<Vector2>();
    }

    private List<Vector2> ReconstructPath(Vector2 current)
    {
        List<Vector2> path = new List<Vector2>();
        path.Add(current);
        int curID = V2toID(current);
        while (cameFrom.ContainsKey(curID))
        {
            current = IDtoV2(cameFrom[curID]);
            path.Add(current);
            curID = cameFrom[curID];
        }
        path.Reverse();
        return path;
    }

    private List<Vector2> FindValidNeighbours(Vector2 current)
    {
        //Debug.Log("Getting Neighbours");
        int maxy = grid.GetLength(0);
        int maxx = grid.GetLength(1);
        List<Vector2> validNeighbours = new List<Vector2>();
        int o;
        Vector2 neighbour;
        neighbour = current;
        neighbour.y--;
        if (neighbour.y >= 0 && neighbour.y < maxy)
            if (validSqrCosts.TryGetValue(grid[(int)neighbour.y, (int)neighbour.x], out o))
            {
                validNeighbours.Add(neighbour);
                //Debug.Log("Valid Neighbour: " + neighbour);
            }
        neighbour = current;
        neighbour.y++;
        if (neighbour.y >= 0 && neighbour.y < maxy)
            if (validSqrCosts.TryGetValue(grid[(int)neighbour.y, (int)neighbour.x], out o))
            {
                validNeighbours.Add(neighbour);
                //Debug.Log("Valid Neighbour: " + neighbour);
            }
        neighbour = current;
        neighbour.x--;
        if (neighbour.x >= 0 && neighbour.x < maxx)
            if (validSqrCosts.TryGetValue(grid[(int)neighbour.y, (int)neighbour.x], out o))
            {
                validNeighbours.Add(neighbour);
                //Debug.Log("Valid Neighbour: " + neighbour);
            }
        neighbour = current;
        neighbour.x++;
        if (neighbour.x >= 0 && neighbour.x < maxx)
            if (validSqrCosts.TryGetValue(grid[(int)neighbour.y, (int)neighbour.x], out o))
            {
                validNeighbours.Add(neighbour);
                //Debug.Log("Valid Neighbour: " + neighbour);
            }
        //for (int i = (int)current.x -1; i <= (int)current.x + 1; i++)
        //{
        //    neighbour = current;
        //    neighbour.x = i;
        //    if (neighbour.x >= 0 && neighbour.x < maxx)
        //    {
        //        for (int j = (int)current.y - 1; j <= (int)current.y + 1; j++)
        //        {
        //            neighbour.y = j;
        //            if (neighbour.y >= 0 && neighbour.y < maxy && validSqrCosts.TryGetValue(grid[j, i], out o))
        //            {
        //                if (!(neighbour.x == current.x && neighbour.y == current.y))
        //                {
        //                    validNeighbours.Add(neighbour);
        //                    Debug.Log("Valid Neighbour: " + neighbour);
        //                }
        //            }
        //        }
        //    }
        //}
        //Debug.Log(validNeighbours.ToArray());
        return validNeighbours;
    }

}