using System;
using UnityEngine;

public struct Node
{
    public static Node NULL = new Node(Vector3.zero, Vector2Int.zero);
    
    public readonly Vector3 WorldPos;
    public readonly Vector2Int Pos;
    public int Cost;

    public Node(Vector3 worldPos, Vector2Int pos, int cost = -1)
    {
        WorldPos = worldPos;
        Pos = pos;
        Cost = cost;
    }
    
    public override bool Equals(object obj)
    {
        if (obj is Node other)
        {
            return WorldPos.Equals(other.WorldPos) && Pos.Equals(other.Pos) && 
                   Cost == other.Cost;
        }
        return false;
         
    }
    public override int GetHashCode() => HashCode.Combine(WorldPos, Pos, Cost);
    public static bool operator == (Node a, Node b) => a.Equals(b);
    public static bool operator != (Node a, Node b) => !a.Equals(b);
}