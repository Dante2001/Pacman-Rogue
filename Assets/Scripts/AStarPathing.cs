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
        waypointPaths[i] = AStar(waypoints[i], waypoints[0]);
        currentWaypoint = 0;
    }

    public List<Vector2> GetNextPath()
    {
        if (currentWaypoint >= waypointPaths.GetLength(0))
            currentWaypoint = 0;
        currentWaypoint++;
        return waypointPaths[currentWaypoint - 1];
    }

    private float Hueristic(Vector2 cur, Vector2 goal)
    {
        return (cur - goal).magnitude;
    }

    private float Cost(Vector2 cur, Vector2 next)
    {
        if (cur.x == next.x && cur.y == next.y) return 0;
        return (float)(validSqrCosts[grid[(int)cur.y, (int)cur.x]] + validSqrCosts[grid[(int)next.y, (int)next.x]]) / 2f;
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
                gScore[y, x] = -1;
                fScore[y, x] = -1;
            }
        }
        cameFrom = new Dictionary<int, int>();
    }

    private List<Vector2> AStar(Vector2 start, Vector2 goal)
    {
        Reset();
        gScore[(int)start.y, (int)start.x] = 0;
        fScore[(int)start.y, (int)start.x] = Hueristic(start, goal);
        Dictionary<int, bool> closed = new Dictionary<int, bool>();
        SortedDictionary<HeapKey, Vector2> open = new SortedDictionary<HeapKey, Vector2>();
        open.Add(new HeapKey(V2toID(start), 0), start);
        Vector2 current;
        while (open.Count > 0)
        {
            SortedDictionary<HeapKey,Vector2>.Enumerator e = open.GetEnumerator();
            current = e.Current.Value;
            if (current.x == goal.x && current.y == goal.y)
                return ReconstructPath(start);
            open.Remove(e.Current.Key);
            closed.Add(V2toID(current),true);
            List<Vector2> neighbours = FindValidNeighbours(current);
            bool o;
            int j;
            for (int i = 0; i < neighbours.Count; i++)
            {
                int neighbourID = V2toID(neighbours[i]);
                if (closed.TryGetValue(neighbourID, out o))
                    continue;
                float tent_gScore = gScore[(int)current.y, (int)current.x] + Cost(current, neighbours[i]);
                HeapKey neighKey = new HeapKey(neighbourID, tent_gScore);
                if (open.ContainsKey(neighKey))
                    open.Add(neighKey, neighbours[i]);
                else if (tent_gScore >= gScore[(int)neighbours[i].y, (int)neighbours[i].x])
                    continue;
                if (cameFrom.TryGetValue(neighbourID, out j))
                    cameFrom[neighbourID] = V2toID(current);
                else
                    cameFrom.Add(neighbourID, V2toID(current));
                gScore[(int)neighbours[i].y, (int)neighbours[i].x] = tent_gScore;
                fScore[(int)neighbours[i].y, (int)neighbours[i].x] = tent_gScore + Hueristic(neighbours[i], goal);
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
        return path;
    }

    private List<Vector2> FindValidNeighbours(Vector2 current)
    {
        int maxy = grid.GetLength(0);
        int maxx = grid.GetLength(1);
        List<Vector2> validNeighbours = new List<Vector2>();
        int o;
        Vector2 neighbour;
        for (int i = -1; i <= 1; i++)
        {
            neighbour = current;
            neighbour.x += i;
            if (neighbour.x >= 0 && neighbour.x < maxx)
            {
                for (int j = -1; j <= 1; j++)
                {
                    neighbour.y = current.y;
                    neighbour.y += j;
                    if (neighbour.y >= 0 && neighbour.y < maxy && validSqrCosts.TryGetValue(grid[(int)neighbour.y, (int)neighbour.x], out o))
                        if (neighbour.x != current.x && neighbour.y != current.y)
                            validNeighbours.Add(neighbour);
                }
            }
        }
        return validNeighbours;
    }

    class HeapKey : IComparer<HeapKey>
    {
        public int Id { get; private set; }
        public float Value { get; private set; }

        public HeapKey(int id, float value)
        {
            Id = id;
            Value = value;
        }

        public int Compare(HeapKey x, HeapKey y)
        {
            float comp = x.Value - y.Value;
            if (comp > 0) return 1;
            else if (comp < 0) return -1;
            else return 0;
        }
    }
}