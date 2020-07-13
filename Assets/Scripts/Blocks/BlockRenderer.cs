using UnityEngine;
using UnityEngine.UI;

namespace Blocks
{
    /// <summary>
    /// Tool to render flying blocks on RawTexture
    /// </summary>
    public class BlockRenderer : MonoBehaviour
    {
        [SerializeField]
        private Camera renderCamera;
        [SerializeField]
        private RawImage target;

        private RenderTexture renderTexture;

        private void Awake()
        {
            renderTexture = new RenderTexture((int)target.rectTransform.rect.height, (int)target.rectTransform.rect.height, 24);
            renderCamera.targetTexture = renderTexture;
            target.texture = renderTexture;
        }

        public void CaptureBlock(FlyingBlock block)
        {
            block.transform.SetParent(renderCamera.transform);
            var blocks = block.Blocks;
            Vector3 pos = Vector3.zero;

            foreach (var b in blocks)
            {
                pos += b.transform.localPosition;
            }
            pos /= blocks.Length;

            block.transform.localPosition = new Vector3(-pos.x, -pos.y, 5);

            renderCamera.gameObject.SetActive(true);
            renderCamera.Render();
            renderCamera.gameObject.SetActive(false);
        }
    }
}
