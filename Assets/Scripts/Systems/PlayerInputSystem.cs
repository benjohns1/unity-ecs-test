using Unity.Entities;
using UnityEngine;

public class PlayerInputSystem : ComponentSystem
{
    private struct Data
    {
        public readonly int Length;
        public ComponentArray<PlayerInput> Input;
    }

    [Inject] private Data m_data;

	protected override void OnUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 mousePosition = Input.mousePosition;

        for (int i = 0; i < m_data.Length; i++)
        {
            m_data.Input[i].Move.x = horizontal;
            m_data.Input[i].Move.y = vertical;
            m_data.Input[i].Cursor = mousePosition;
        }
    }
}
