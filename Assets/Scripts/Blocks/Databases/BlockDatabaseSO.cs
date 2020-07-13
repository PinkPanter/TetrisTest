using System;
using Helpers.BoolStructs;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Blocks.Databases
{
    /// <summary>
    /// Holds block structs and handle random of next element
    /// </summary>
    [CreateAssetMenu(fileName = "BlockDatabase", menuName = "Databases/BlockDatabase")]
    public class BlockDatabaseSO : ScriptableObject
    {
        [SerializeField]
        private Block[] blocks;

        private int lastIndex = -1;

        public Matrix4x4Bool GetRandom()
        {
            //Algorithm from NES Tetris
            var currentIndex = Random.Range(0, blocks.Length + 1);

            if(currentIndex == blocks.Length || lastIndex == currentIndex)
                currentIndex = Random.Range(0, blocks.Length);

            lastIndex = currentIndex;
            return blocks[currentIndex].blockStruct;
        }

        [Serializable]
        private struct Block
        {
            public Matrix4x4Bool blockStruct;
        }
    }
}
