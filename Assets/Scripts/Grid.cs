using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid
{
    public readonly Node[, ] nmap;
    public readonly List<Node> starts = new(), ends = new();
    private readonly int _sizeX, _sizeY;
    private readonly BoundsInt _bounds;

    public Grid(Tilemap map)
    {
        _bounds = map.cellBounds;
        var allTiles = map.GetTilesBlock(_bounds);

        _sizeX = _bounds.size.x;
        _sizeY = _bounds.size.y;
        nmap = new Node[_sizeX, _sizeY];

        for (int x = 0; x < _sizeX; x++) {
            for (int y = 0; y < _sizeY; y++) {
                var tile = allTiles[x + y * _sizeX];
                if (tile is not null)
                {
                    var pos = new Vector2Int(x, y);
                    var worldPos = map.CellToWorld(
                        new Vector3Int(pos.x + _bounds.xMin, pos.y + _bounds.yMin));
                    var node = new Node(worldPos, pos);

                    switch (tile.name)
                    {
                        case "start":
                            starts.Add(node); break;
                        case "end":
                            ends.Add(node); break;
                    }
                    nmap[x, y] = node;
                }
            }
        }
    }

    public Vector2Int WorldToCellPosition(Vector3 pos)
    {
        var correctPos = GetCorrectWorldPosition(pos);
        return new Vector2Int((int)(correctPos.x - _bounds.xMin), (int)(correctPos.y - _bounds.yMin));
    }
    public Vector3 GetCorrectWorldPosition(Vector3 worldPos) 
        => new Vector3((int)Math.Floor(worldPos.x), (int)Math.Floor(worldPos.y));

    public List<Node> GetNeighbours(Node node, INodeFilter filter) 
        => filter.Filtrate(GetNeighbours(node));


    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                if (x == 0 || y == 0)
                {
                    var pos = new Vector2Int(node.Pos.x + x, node.Pos.y + y);
                    var neighbour = GetNode(pos);
                    if (neighbour != Node.NULL)
                        neighbours.Add(nmap[pos.x, pos.y]);
                }
            }
        }

        return neighbours;
    }
    
    public void CalculateCosts()
    {
        foreach (var start in starts)
            nmap[start.Pos.x, start.Pos.y].Cost = 0;

        var nodes = starts; 
        var cost = 1;
        while (nodes.Count > 0)
        {
            nodes = CalculateCost(nodes, cost);
            cost++;
        }

    }

    private List<Node> CalculateCost(List<Node> nodes, int cost)
    {
        var neighbours = new List<Node>();
        foreach (var node in nodes)
            neighbours.AddRange(GetNeighbours(node));

        var result = new List<Node>();
        foreach (var neighbour in neighbours)
        {
            if (neighbour.Cost == -1)
            {
                nmap[neighbour.Pos.x, neighbour.Pos.y].Cost = cost;
                result.Add(neighbour);
            }
        }

        return result;
    }

    private Node GetNode(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x >= _sizeX || pos.y < 0 || pos.y >= _sizeY) 
            return Node.NULL;
        return nmap[pos.x, pos.y];
    }
}