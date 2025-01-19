using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer vfxSprite; // VFX Sprite
        [SerializeField] private Animator vfxAnimator; // VFX Animator

        // Private Variables
        private VFXController vfxController;
        private static readonly int FADE_HASH = Animator.StringToHash("Fade");
        public void Init(VFXController _vfxController)
        {
            // Setting Variables
            vfxController = _vfxController;
            Reset();
        }

        public void Reset()
        {
            SetSprite(vfxController.GetVFXModel().VFXColor);
            SetTransform(vfxController.GetVFXModel().VFXTransform);
            FadeAnimation();
            Object.Destroy(gameObject, vfxController.GetVFXModel().VFXDuration);
        }

        private void SetSprite(Color _vfxColor)
        {
            vfxSprite.sprite = vfxController.GetVFXModel().VFXSprite;
            vfxSprite.color = new Color(_vfxColor.r, _vfxColor.g, _vfxColor.b, vfxSprite.color.a);
        }

        private void SetTransform(Transform _vfxTransform)
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

                // Scale the VFX to match the target object's size
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

        private void FadeAnimation()
        {
            float animationDuration = vfxController.GetVFXModel().VFXDuration;
            float defaultClipDuration = vfxAnimator.GetCurrentAnimatorStateInfo(0).length; // Default animation length
            vfxAnimator.speed = defaultClipDuration / animationDuration; // Adjust speed
        }
    }
}