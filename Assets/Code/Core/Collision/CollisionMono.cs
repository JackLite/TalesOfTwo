using UnityEngine;

namespace TheTalesOfTwo.Core.Collision
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionMono : MonoBehaviour
    {
        private bool _isInteractable = true;
        private bool _isCollide;

        private void OnTriggerEnter2D(Collider2D _)
        {
            _isCollide = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isCollide = false;
            _isInteractable = true;
        }

        public bool IsCollide()
        {
            return _isCollide && _isInteractable;
        }

        public void StopUntilExit()
        {
            _isInteractable = false;
        }
    }
}