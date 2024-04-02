using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Node : MonoBehaviour
{
    NodeType NodeType;
    public Vector2Int Position;
    public List<Node> InComing = new List<Node>();
    public List<Node> OutGoing = new List<Node>();
    public bool IsActive;
    public void Assign()
    {

    }

    public bool HasConnection(Node other)
    {
        if(InComing.Contains(other) || OutGoing.Contains(other))
        {
            return true;
        }
        return false;
    }
}
