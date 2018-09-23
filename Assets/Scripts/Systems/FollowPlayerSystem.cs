using Unity.Entities;
using UnityEngine;

[UpdateBefore(typeof(UnityEngine.Experimental.PlayerLoop.FixedUpdate))]
public class FollowPlayerSystem : ComponentSystem
{
    private struct FollowerData
    {
        public readonly int Length;
        public readonly ComponentArray<FollowPlayer> FollowPlayer;
        public ComponentArray<Position> Position;
    }

    [Inject] private FollowerData m_followerData;

    private struct PlayerData
    {
        public readonly int Length;
        public readonly ComponentArray<PlayerInput> Input;
        public readonly ComponentArray<Position> Position;
        public readonly ComponentArray<Rigidbody> Rigidbody;
    }

    [Inject] private PlayerData m_playerData;
    
    private struct TransformData
    {
        public readonly int Length;
        public readonly ComponentArray<Position> Position;
        public readonly ComponentArray<Rotation> Rotation;
        public readonly ComponentArray<Transform> Transform;
        public ComponentArray<FollowPlayer> FollowPlayer;
        public SubtractiveComponent<Rigidbody> Rigidbody;
    }
    
    [Inject] private TransformData m_transformData;

	protected override void OnUpdate()
    {
        if (m_playerData.Length <= 0)
        {
            return;
        }

        Vector3 playerPosition = m_playerData.Rigidbody[0].position;

        for (int i = 0; i < m_followerData.Length; i++)
        {
            m_followerData.Position[i].Value = playerPosition + m_transformData.FollowPlayer[i].OffsetPosition;
        }

        // Directly update transform position
        for (int i = 0; i < m_transformData.Length; i++)
        {
            FollowPlayer followPlayer = m_transformData.FollowPlayer[i];
            Transform transform = m_transformData.Transform[i];

            if (!followPlayer.Initialized && followPlayer.UseInitialTransformAsOffset)
            {
                followPlayer.OffsetPosition = transform.position;
                followPlayer.OffsetRotation = transform.rotation;
                followPlayer.Initialized = true;
            }
            
            transform.position = m_transformData.Position[i].Value;
        }
    }
}
