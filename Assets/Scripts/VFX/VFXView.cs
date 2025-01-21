using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer vfxSprite; // VFX Sprite

        // Private Variables
        private VFXController vfxController;

        public void Init(VFXController _vfxController)
        {
            // Setting Variables
            vfxController = _vfxController;
            Reset();
        }

        public void Reset()
        {
            SetSprite(vfxController.GetVFXModel().VFXColor);
            Invoke(nameof(HideView), vfxController.GetVFXModel().VFXDuration); // HideView after the duration
        }

        private void SetSprite(Color _vfxColor)
        {
            vfxSprite.material.color = vfxController.GetVFXModel().VFXColor;
            vfxSprite.sprite = vfxController.GetVFXModel().VFXSprite;
        }

        public void SetTransform(Transform _vfxTransform)
        {
            transform.position = _vfxTransform.position;
            transform.rotation = _vfxTransform.rotation;

            // Fetching the actual size of the collided object
            Renderer targetRenderer = _vfxTransform.GetComponent<Renderer>();
            Renderer vfxRenderer = GetComponent<Renderer>();

            if (targetRenderer != null && vfxRenderer != null)
            {
                // Calculating the scaling factor to match the world size
                Vector3 targetSize = targetRenderer.bounds.size; // World size of the target
                Vector3 vfxSize = vfxRenderer.bounds.size;       // World size of the VFX

                // Scaling the VFX to match the target object's size
                Vector3 scaleFactor = new Vector3(
                    targetSize.x / vfxSize.x,
                    targetSize.y / vfxSize.y,
                    targetSize.z / vfxSize.z
                );

                transform.localScale = Vector3.Scale(transform.localScale, scaleFactor) *
                    vfxController.GetVFXModel().VFXScaleMultiplier;
            }
            else
            {
                Debug.LogWarning("Renderer not found on VFX or target object.");
            }
        }

        public void ShowView()
        {
            gameObject.SetActive(true);
        }

        public void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}