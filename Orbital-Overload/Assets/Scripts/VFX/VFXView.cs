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
            vfxSprite.sprite = vfxController.GetVFXModel().VFXSprite;

            Object.Destroy(gameObject, vfxController.GetVFXModel().VFXDuration);
        }
    }
}