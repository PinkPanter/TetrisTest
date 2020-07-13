using System;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Align panels according to Anchors(borders) on game field 
    /// </summary>
    public class PanelsAligner : MonoBehaviour
    {
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private Transform leftAnchor;
        [SerializeField]
        private Transform rightAnchor;

        [SerializeField]
        private RectTransform leftPanel;
        [SerializeField]
        private RectTransform rightPanel;

        private void Awake()
        {
            var leftVPos = mainCamera.WorldToViewportPoint(leftAnchor.position);
            leftPanel.anchorMax = new Vector2(leftVPos.x, leftPanel.anchorMax.y);

            var rightVPos = mainCamera.WorldToViewportPoint(rightAnchor.position);
            rightPanel.anchorMin = new Vector2(rightVPos.x, rightPanel.anchorMin.y);
        }
    }
}
