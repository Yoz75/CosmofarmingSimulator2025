using UnityEngine;
using UnityEngine.UI;

namespace CS25
{
    public class ObjectUnderMouseHelper : MonoBehaviour
    {
        public static ObjectUnderMouseHelper Instance { get; private set; }

        [SerializeField] private RawImage RenderTextureImage;
        [SerializeField] private Camera RenderTextureCamera;

        private RectTransform RectTransform;

        private void Awake()
        {
            Instance = this;
            RectTransform = RenderTextureImage.rectTransform;
        }

        /// <summary>
        /// Is object under mouse? (supports only camera rendeer to RenderTexture)
        /// </summary>
        public bool IsUnderMouse(Vector3 mousePosition, GameObject target)
        {
            if(!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    RectTransform, mousePosition, null, out Vector2 localPoint))
                return false;

            Vector2 normalized = new Vector2(
                (localPoint.x + RectTransform.rect.width * 0.5f) / RectTransform.rect.width,
                (localPoint.y + RectTransform.rect.height * 0.5f) / RectTransform.rect.height);

            if(normalized.x < 0f || normalized.x > 1f || normalized.y < 0f || normalized.y > 1f)
                return false;

            Vector2 renderTexturePixel = new Vector2(
                normalized.x * RenderTextureCamera.pixelWidth,
                normalized.y * RenderTextureCamera.pixelHeight);

            Vector3 worldPoint = RenderTextureCamera.ScreenToWorldPoint(
                new Vector3(renderTexturePixel.x, renderTexturePixel.y, RenderTextureCamera.nearClipPlane));

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, RenderTextureCamera.transform.forward);

            return hit.collider != null && hit.collider.gameObject == target;
        }
    }

}