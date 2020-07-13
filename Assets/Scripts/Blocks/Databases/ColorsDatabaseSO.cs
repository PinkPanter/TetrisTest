using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Blocks.Databases
{
    /// <summary>
    /// Holds block colors and materials. Allow to change all transparent material at ones
    /// </summary>
    [CreateAssetMenu(fileName = "ColorsDatabase", menuName = "Databases/ColorsDatabase")]
    public class ColorsDatabaseSO : ScriptableObject
    {
        [SerializeField]
        private Material defOpaqueMat;
        [SerializeField]
        private Material defTransparentMat;

        [SerializeField]
        private ColorScheme[] colors;

        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");
        private static readonly int GlowColor = Shader.PropertyToID("_GlowColor");
        private static readonly int Cutoff = Shader.PropertyToID("_Cutoff");

        /// <summary>
        /// Get Random ColorScheme
        /// </summary>
        /// <returns>Returns cortege of opaque and transparentColor</returns>
        public (Material, Material) GetRandom()
        {
            var index = Random.Range(0, colors.Length);

            if (colors[index].opaqueMat == null)
            {
                colors[index].opaqueMat = Instantiate(defOpaqueMat);
                colors[index].transparentMat = Instantiate(defTransparentMat);
                var current = colors[index];

                current.opaqueMat.SetColor(BaseColor, current.mainColor);
                current.transparentMat.SetColor(BaseColor, current.mainColor);
                current.transparentMat.SetColor(GlowColor, current.glowColor);
            }

            return (colors[index].opaqueMat, colors[index].transparentMat);
        }

        /// <summary>
        /// Set transparent materials alphas
        /// </summary>
        /// <param name="value">Ranged 0 - 1 alpha</param>
        public void SetAllTransparentAlpha(float value)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i].transparentMat != null)
                    colors[i].transparentMat.SetFloat(Cutoff, value);
            }
        }

        [Serializable]
        private struct ColorScheme
        {
            public Color mainColor;

            public Color glowColor;

            [NonSerialized]
            public Material opaqueMat;
            [NonSerialized]
            public Material transparentMat;
        }
    }
}
