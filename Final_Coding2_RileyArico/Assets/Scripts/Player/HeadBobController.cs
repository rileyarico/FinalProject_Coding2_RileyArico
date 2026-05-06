using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool enable = true;

    [SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    [SerializeField, Range(0, 30f)] private float frequency = 10.0f;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform cameraHolder = null;

    private float toggleSpeed = 3.0f;
    private Vector3 startPos;
    private Rigidbody player;

    private void Awake()
    {
        player = FindFirstObjectByType<FinalPlayer>().gameObject.GetComponent<Rigidbody>();
        startPos = _camera.localPosition;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(player.linearVelocity.x, 0, player.linearVelocity.z).magnitude;
        if (speed < toggleSpeed) return;
        if (!player.gameObject.GetComponent<FinalPlayer>().isGrounded) return;

        FootStepMotion();
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }
}
