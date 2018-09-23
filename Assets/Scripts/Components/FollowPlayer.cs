using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public bool UseInitialTransformAsOffset = true;
    public Vector3 OffsetPosition = Vector3.zero;
    public Quaternion OffsetRotation = Quaternion.identity;

    [HideInInspector] public bool Initialized = false;
}
