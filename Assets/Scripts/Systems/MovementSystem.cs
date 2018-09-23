using Unity.Entities;
using UnityEngine;

public class MovementSystem : ComponentSystem
{
    private struct RigidbodyData
    {
        public readonly int Length;
        public readonly ComponentArray<Position> Position;
        public readonly ComponentArray<Rotation> Rotation;
        public ComponentArray<Rigidbody> Rigidbody;
    }

    [Inject] private RigidbodyData m_rigidbodyData;

	protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

        // Update rigidbody position & rotation
        for (int i = 0; i < m_rigidbodyData.Length; i++)
        {
            m_rigidbodyData.Rigidbody[i].MovePosition(m_rigidbodyData.Position[i].Value);
            m_rigidbodyData.Rigidbody[i].MoveRotation(m_rigidbodyData.Rotation[i].Value);
        }
    }
}
