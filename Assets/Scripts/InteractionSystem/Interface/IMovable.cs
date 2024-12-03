using UnityEngine;

namespace PhotoSystem
{
    public interface IMovable
    {
        void Move(Transform playerPosition, float direction);
    }
}