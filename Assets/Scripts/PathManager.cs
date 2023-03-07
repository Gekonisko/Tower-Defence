using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathManager : MonoBehaviour
{

    [SerializeField] private Tilemap path;
    private Grid _grid;
    
    void Awake()
    {
        _grid = new Grid(path);
        _grid.CalculateCosts();
    }
    
    void OnDrawGizmos() {
        if (_grid != null) {
            foreach (var node in _grid.nmap) {
                Handles.Label(node.WorldPos, $"{node.Cost}");
            }
        }
    }
}
