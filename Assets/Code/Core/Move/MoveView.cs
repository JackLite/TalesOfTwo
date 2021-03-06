using UnityEngine;

namespace TheTalesOfTwo.Core.Move
{
    /// <summary>
    /// Отвечает за управление движением объекта в юнити
    /// </summary>
    public class MoveView : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigid;

        public void SetVelocity(Vector2 velocity)
        {
            // _rigid.velocity = velocity;
            if (float.IsNaN(velocity.x) || float.IsNaN(velocity.y))
            {
                UnityEngine.Debug.LogError("WTF?!");
                return;
            }
            transform.position += new Vector3(velocity.x, velocity.y, 0);
        }

        public void SetY(float y)
        {
            var oldPos = transform.position;
            transform.position = new Vector3(oldPos.x, y, oldPos.z);
        }

        public void AddDeltaY(float delta)
        {
            transform.position += Vector3.up * delta;
        }
    }
}