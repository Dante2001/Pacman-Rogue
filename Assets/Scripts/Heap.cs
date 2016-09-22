using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Heap {

    private List<HeapValue> heap;

    public Heap()
    {
        heap = new List<HeapValue>();
    }

    public void Insert(HeapValue hv)
    {
        heap.Add(hv);
        HeapifyUP(heap.Count-1);
    }

    private void HeapifyUP(int c)
    {
        if (c <= 0)
            return;
        int cur = c;
        int par = Mathf.FloorToInt((cur - 1) / 2);
        if (heap[cur].value < heap[par].value)
        {
            Swap(cur, par);
            HeapifyUP(par);
        }
    }

    public HeapValue Pop()
    {
        HeapValue ret = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDOWN(0);
        return ret;
    }
    
    private void HeapifyDOWN(int c)
    {
        int cur = c;
        int left = 2 * cur + 1;
        int right = 2 * cur + 2;
        int largest;
        if (left >= heap.Count)
            return;
        else if (left < heap.Count && right >= heap.Count)
            largest = left;
        else if (heap[left].value <= heap[right].value)
            largest = left;
        else
            largest = right;

        if (heap[cur].value > heap[largest].value)
        {
            Swap(cur, largest);
            HeapifyDOWN(largest);
        }
    }

    private void Swap(int c, int p)
    {
        HeapValue temp = heap[p];
        heap[p] = heap[c];
        heap[c] = temp;
    }

    public int Count()
    {
        return heap.Count;
    }

    public bool Contains(Vector2 v)
    {
        for (int i = 0; i < heap.Count; i++)
        {
            if (heap[i].v2.x == v.x && heap[i].v2.y == v.y)
                return true;
        }
        return false;
    }
}
