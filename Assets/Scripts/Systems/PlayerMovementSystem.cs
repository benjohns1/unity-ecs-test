using Unity.Entities;
using UnityEngine;

public class PlayerMovementSystem : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public readonly ComponentArray<PlayerInput> Input;
        public readonly ComponentArray<Transform> Transform;
        public ComponentArray<Position> Position;
        public ComponentArray<Rotation> Rotation;
    }

    [Inject] private Data m_data;

	protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;
        int floorLayer = LayerMask.GetMask(Bootstrap.Settings.FloorLayer);
        Camera mainCamera = Camera.main;
        float playerSpeed = Bootstrap.Settings.PlayerSpeed;

        for (int i = 0; i < m_data.Length; i++)
        {
            // Update position
            Vector3 moveVector = new Vector3(m_data.Input[i].Move.x, 0, m_data.Input[i].Move.y);
            m_data.Position[i].Value += moveVector.normalized * playerSpeed * deltaTime;

            // Update rotation
            Ray cameraRay = mainCamera.ScreenPointToRay(m_data.Input[i].Cursor);
            RaycastHit hit;
            if (!Physics.Raycast(cameraRay, out hit, 100, floorLayer))
            {
                continue;
            }

            Vector3 forward = hit.point - m_data.Transform[i].position;
            Quaternion rotation = Quaternion.LookRotation(forward);
            m_data.Rotation[i].Value = new Quaternion(0, rotation.y, 0, rotation.w).normalized;
        }
    }
}
