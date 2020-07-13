using System.Collections.Generic;
using System.Linq;
using Helpers.BoolStructs;
using UnityEngine;

namespace Blocks
{
    /// <summary>
    /// Grid of blocks, GameFiels
    /// Handles collision, removing blocks, adding blocks
    /// </summary>
    public class Grid : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int size = new Vector2Int(10, 20);

        private bool[,] grid;
        private GameObject[,] realBlocks;
        private Material[,] fadeMaterials;

        private void Awake()
        {
            grid = new bool[size.x, size.y];
            realBlocks = new GameObject[size.x, size.y];
            fadeMaterials = new Material[size.x, size.y];
        }

        public void UpdateGrid(Vector2Int position, GameObject[] objects, Matrix4x4Bool blockStruct, Material fadeMat, out bool isOutOfBoundsTop)
        {
            if (position.y >= size.y)
            {
                isOutOfBoundsTop = true;
                return;
            }

            isOutOfBoundsTop = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blockStruct[i, j])
                    {
                        var actualPos = position + new Vector2Int(i, -j);
                        if (actualPos.x >= 0 && actualPos.x < size.x && actualPos.y >= 0 && actualPos.y < size.y)
                        {
                            grid[actualPos.x, actualPos.y] = true;
                        }
                    }
                }
            }

            foreach (var o in objects)
            {
                var objectPos = transform.InverseTransformPoint(o.transform.position);
                o.transform.SetParent(transform);
                realBlocks[Mathf.FloorToInt(objectPos.x), Mathf.FloorToInt(objectPos.y)] = o;
                fadeMaterials[Mathf.FloorToInt(objectPos.x), Mathf.FloorToInt(objectPos.y)] = fadeMat;
            }
        }

        public bool CheckCollision(Vector2Int position, Matrix4x4Bool blockStruct, out bool byBoundsHorizontal)
        {
            var bounds = blockStruct.GetBounds();
            byBoundsHorizontal = true;
            
            if (position.x < 0 || position.x + bounds.x > size.x)
                return true;

            byBoundsHorizontal = false;

            if (position.y - bounds.y + 1 < 0)
                return true;

            for (int i = 0; i < bounds.x; i++)
            {
                var currentColumnSize = blockStruct.GetRow(i).Max;

                for (int j = 0; j < currentColumnSize; j++)
                {
                    if(position.y - j >= size.y)
                        continue;
                    
                    if (grid[position.x + i, position.y - j] && blockStruct[i, j])
                        return true;
                }
            }


            return false;
        }

        public int[] CheckLines()
        {
            List<int> lineIndexes = null;

            for (int y = 0; y < size.y; y++)
            {
                bool emptyCell = false;
                for (int x = 0; x < size.x; x++)
                {
                    if (!grid[x, y])
                    {
                        emptyCell = true;
                        break;
                    }
                }

                if (!emptyCell)
                {
                    if(lineIndexes == null)
                        lineIndexes = new List<int>();

                    lineIndexes.Add(y);
                }
            }

            
            return lineIndexes?.ToArray();
        }

        public void SwitchLineMaterial(int[] lines)
        {
            foreach (var line in lines)
            {
                for (int x = 0; x < size.x; x++)
                {
                    realBlocks[x, line].GetComponent<MeshRenderer>().sharedMaterial = fadeMaterials[x, line];
                }
            }
        }

        public void DestroyLines(int[] lines)
        {
            lines = lines.OrderByDescending(l => l).ToArray();

            foreach (var line in lines)
            {
                for (int x = 0; x < size.x; x++)
                {
                    if (realBlocks[x, line] != null)
                    {
                        Destroy(realBlocks[x, line].gameObject);
                    }

                    grid[x, line] = false;
                }

                for (int i = line; i < size.y - 1; i++)
                {
                    for (int x = 0; x < size.x; x++)
                    {
                        grid[x, i] = grid[x, i + 1];
                        realBlocks[x, i] = realBlocks[x, i + 1];
                        if (realBlocks[x, i] != null)
                            realBlocks[x, i].transform.localPosition = new Vector3(x, i);
                        fadeMaterials[x, i] = fadeMaterials[x, i + 1];
                    }
                }

                for (int x = 0; x < size.x; x++)
                {
                    grid[x, size.y - 1] = false;
                    realBlocks[x, size.y - 1] = null;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    if(grid != null)
                        Gizmos.color = grid[x, y] ? Color.red : Color.white;

                    Gizmos.DrawWireCube(transform.TransformPoint(new Vector3(x, y)), Vector3.one * 0.95f);
                }
            }
        }
    }
}
