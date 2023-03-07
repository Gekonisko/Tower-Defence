using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Managers
{
    public class PathManager : Singleton<PathManager> 
    {

        [SerializeField] private Tilemap path;
        private Grid _grid;

        public override void Awake()
        {
            base.Awake();
            _grid = new Grid(path);
            _grid.CalculateCosts();
        }

        public Vector3 GetNextPosition(Vector3 actualPos)
        {
            var posInGrid = _grid.WorldToCellPosition(actualPos);
            var nextNode = _grid.GetNeighbours(new Node(actualPos, posInGrid), MaxCostFilter.Filter).First();
            return nextNode.WorldPos + new Vector3(0.5f, 0.5f);
        }

        public List<Vector3> GetStartPositions()
            => _grid.starts.Select(e => e.WorldPos).ToList();

        public bool IsEndOfPath(Vector3 worldPos)
        {
            var correctPos = _grid.GetCorrectWorldPosition(worldPos);
            return _grid.ends.Any(n => n.WorldPos == correctPos);
        }
    
        void OnDrawGizmos() {
            if (_grid != null) {
                foreach (var node in _grid.nmap) {
                    Handles.Label(node.WorldPos, $"{node.Cost}");
                }
            }
        }
    }
}
