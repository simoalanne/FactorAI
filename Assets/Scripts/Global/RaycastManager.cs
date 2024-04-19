using UnityEngine;

namespace Global
{
    public class RaycastManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup[] canvasGroups;

        public void DisableOtherCanvasesRaycasting()
        {
            if (canvasGroups == null || canvasGroups.Length == 0) return;

            foreach (var group in canvasGroups)
            {
                group.blocksRaycasts = false;
            }
        }

        public void EnableOtherCanvasesRaycasting()
        {
            if (canvasGroups == null || canvasGroups.Length == 0) return;

            foreach (var group in canvasGroups)
            {
                group.blocksRaycasts = true;
            }
        }
    }
}