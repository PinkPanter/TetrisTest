using Helpers;
using Helpers.BoolStructs;
using InputControllers;
using UnityEngine;

namespace Blocks
{
    /// <summary>
    /// Struct of blocks that falling down
    /// Handles 3D objects creation, movement, rotation
    /// </summary>
    public class FlyingBlock : MonoBehaviour
    {
        private static GameObject block;

        public Vector2Int Position =>
            new Vector2Int(Mathf.FloorToInt(transform.localPosition.x), Mathf.FloorToInt(
                transform.localPosition.y));

        public Matrix4x4Bool BlockStruct => blockStruct;

        public GameObject[] Blocks { get; private set; }
        public Material FadeMaterial { get; private set; }

        private Matrix4x4Bool blockStruct;

        private Grid grid;

        private void OnEnable()
        {
            InputManager.OnMovedHorizontal += InputManagerOnMovedHorizontal;
            InputManager.OnRotate += InputManagerOnRotate;
        }

        private void OnDisable()
        {
            InputManager.OnMovedHorizontal -= InputManagerOnMovedHorizontal;
            InputManager.OnRotate -= InputManagerOnRotate;
        }

        private void InputManagerOnMovedHorizontal(int shift)
        {
            transform.localPosition += new Vector3(shift, 0, 0);

            if (grid.CheckCollision(Position, BlockStruct, out var byBounds))
            {
                transform.localPosition -= new Vector3(shift, 0, 0);
            }
        }

        private void InputManagerOnRotate()
        {
            var preRotateState = blockStruct;
            blockStruct.Rotate();
            blockStruct.MirrorWithShift();

            if (grid.CheckCollision(Position, BlockStruct, out var byBounds))
            {
                blockStruct = preRotateState;
            }
            else
                SetupBricks();
        }

        private void SetupBricks()
        {
            int index = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blockStruct[i, j])
                    {
                        Blocks[index].transform.localPosition = new Vector3(i, -j);
                        index++;
                    }
                }
            }
        }

        public static FlyingBlock Constract(Matrix4x4Bool blockStruct, Material opaqueMat, Material transparentMat, Grid grid)
        {
            if (block == null)
                block = Resources.Load<GameObject>("Prefabs/Block");

            var newBlock = new GameObject("FlyingBlock").AddComponent<FlyingBlock>();

            newBlock.FadeMaterial = transparentMat;
            newBlock.Blocks = new GameObject[blockStruct.GetSum()];
            newBlock.blockStruct = blockStruct;
            newBlock.grid = grid;

            int index = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blockStruct[i, j])
                    {
                        var blockInst = Instantiate(block, newBlock.transform);
                        blockInst.transform.localPosition = new Vector3(i, -j);

                        blockInst.GetComponent<MeshRenderer>().sharedMaterial = opaqueMat;
                        newBlock.Blocks[index] = blockInst;
                        index++;
                    }
                }

            }

            return newBlock;
        }
    }
}
