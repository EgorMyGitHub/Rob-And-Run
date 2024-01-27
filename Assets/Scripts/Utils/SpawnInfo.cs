using UnityEngine;

namespace Utils
{
    public readonly struct SpawnInfo
    {
        public SpawnInfo(
            Vector3 position)
        {
            Position = position;
            Rotation = Quaternion.identity;
            Parent = null;
        }
        public SpawnInfo(
            Vector3 position,
            Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
            Parent = null;
        }
        
        public SpawnInfo(
            Vector3 position,
            Transform transform)
        {
            Position = position;
            Rotation = default;
            Parent = transform;
        }
        public SpawnInfo(
            Vector3 position,
            Quaternion rotation,
            Transform parent)
        {
            Position = position;
            Rotation = rotation;
            Parent = parent;
        }
        
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        public readonly Transform Parent;
    }
}