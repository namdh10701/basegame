using _Base.Scripts.Utils.Extensions;
using Fusion;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] MapConfig mapConfig;
    [SerializeField] Node nodePrefab;
    [SerializeField] GameObject linePrefab;
    [SerializeField] Transform mapRoot;
    public Node[,] Map;
    List<Node>[] Floors;
    private void Awake()
    {
        Map = new Node[mapConfig.Grid.x, mapConfig.Grid.y];
        Floors = new List<Node>[mapConfig.Grid.y];
        for (int i = 0; i < Floors.Length; i++)
        {
            Floors[i] = new List<Node>();
        }

        Generate();
    }
    public void Generate()
    {
        GenerateNodesGrid();
        ConnectNodes();
        RemoveCrossedConnection();
        RandomizeNodesPosition();

        GeneratePaths();

        AssignNodesType();
        AddBossNode();
    }

    void GenerateNodesGrid()
    {
        for (int y = 0; y < mapConfig.Grid.y; y++)
        {
            for (int x = 0; x < mapConfig.Grid.x; x++)
            {
                Node node = Instantiate(nodePrefab, new Vector2((x - mapConfig.Grid.x / 2) * mapConfig.DimensionSize.x + mapRoot.position.x, y * mapConfig.DimensionSize.y + mapRoot.position.y), Quaternion.identity, mapRoot);
                Map[x, y] = node;
                node.Position = new Vector2Int(x, y);
            }
        }
        // Select first node
        // reselect for more 6 time 
    }

    void ConnectNodes()
    {
        if (mapConfig.StartNodeNumber > mapConfig.Grid.x)
        {
            Debug.LogError("X");
        }
        List<int> availableFirstFloorIndex = new List<int>();
        List<int> availableFirstFloorIndexCopy = new List<int>();
        for (int i = 0; i < mapConfig.Grid.x; i++)
        {
            availableFirstFloorIndex.Add(i);
            availableFirstFloorIndexCopy.Add(i);
        }

        int startNodeNumber = mapConfig.StartNodeNumber;
        int tryTimes = Mathf.Max(startNodeNumber, 6);
        Debug.Log("S" + startNodeNumber + " TT" + tryTimes);
        for (int i = 0; i < tryTimes; i++)
        {
            int nodeIndex;
            if (i < startNodeNumber - 1)
            {
                nodeIndex = availableFirstFloorIndex.GetRandom();
                availableFirstFloorIndex.Remove(nodeIndex);
            }
            else
            {
                nodeIndex = availableFirstFloorIndexCopy.GetRandom();
            }
            Node node = Map[nodeIndex, 0];
            Debug.Log(nodeIndex + " FIRST NODE");
            Floors[0].Add(node);
            for (int j = 1; j < Map.GetLength(1); j++)
            {
                int nextIndexLower = Mathf.Max(nodeIndex - 1, 0);
                int nextIndexUpper = Mathf.Min(nodeIndex + 2, Map.GetLength(0));
                Debug.Log(nextIndexLower + " AA " + nextIndexUpper);

                nodeIndex = Random.Range(nextIndexLower, nextIndexUpper);
                Debug.Log(j + " " + nodeIndex);
                Node nextNode = Map[nodeIndex, j];
                node.OutGoing.Add(nextNode);
                nextNode.InComing.Add(node);
                Floors[j].Add(nextNode);
                node = nextNode;
            }
        }
    }

    void AssignNodesType()
    {

    }

    void RandomizeNodesPosition()
    {

    }
    void RemoveCrossedConnection()
    {
        for (int i = 0; i < Floors.Length; i++)
        {
            for (int j = 0; j < Floors[i].Count; j++)
            {
                Node node = Floors[i][j];
             /*   
                if()

                Node topRightNode = GetTopRightNode(node);
                Node topLeftNode = GetTopLeftNode(node);

                if (topRightNode != null && topRightNode.IsActive)
                {
                    if (node.HasConnection(topRightNode))
                    {
                        CheckAndRemoveCrossRightNode(node);
                    }
                }*/
                /*if (topLeftNode != null && topLeftNode.IsActive)
                {
                    if (node.HasConnection(topLeftNode))
                    {
                        CheckAndRemoveCrossLeftNode(node);
                    }

                }*/
            }
        }
    }

    void CheckAndRemoveCrossRightNode(Node node)
    {
        //Node rightNode = 
    }
    void CheckAndRemoveCrossLeftNode(Node node)
    {

    }

    public Node GetRightNode(Node node)
    {
        if (node.Position.x == Map.GetLength(1) - 1)
        {
            return null;
        }
        return Map[node.Position.x + 1, node.Position.y];
    }

    public Node GetLeftNode(Node node)
    {
        if (node.Position.x == Map.GetLength(1) - 1)
        {
            return null;
        }
        return Map[node.Position.x + 1, node.Position.y];
    }

    public Node GetTopRightNode(Node node)
    {
        if (node.Position.x == Map.GetLength(1) - 1
            || node.Position.y == Map.GetLength(0) - 1)
        {
            return null;
        }
        return Map[node.Position.x + 1, node.Position.y+1];
    }

    public Node GetTopLeftNode(Node node)
    {
        if (node.Position.x == 0
            || node.Position.y == Map.GetLength(0) - 1)
        {
            return null;
        }
        return Map[node.Position.x + 1, node.Position.y+1];
    }


    void GeneratePaths()
    {
        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1) - 1; j++)
            {
                Node node = Map[i, j];
                if (node.OutGoing.Count > 0)
                {
                    node.IsActive = true;
                    foreach (Node outGoingNode in node.OutGoing)
                    {
                        GameObject line = Instantiate(linePrefab, mapRoot);
                        line.transform.position = (node.transform.position + outGoingNode.transform.position) / 2;
                        line.transform.up = (outGoingNode.transform.position - node.transform.position).normalized;
                    }
                }
                else
                {
                    node.IsActive = false;
                    node.gameObject.SetActive(false);
                }
            }
            Node finalNode = Map[i, Map.GetLength(1) - 1];
            if (finalNode.InComing.Count == 0)
            {
                finalNode.IsActive = false;
                finalNode.gameObject.SetActive(false);
            }
        }
    }

    void AddBossNode()
    {

    }
}