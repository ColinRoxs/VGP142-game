using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeshAnimatorDriver : MonoBehaviour
{
    [SerializeField] private Transform playerRoot; // assign the parent player GameObject
    [SerializeField] private float moveSpeed = 5f; // same as controller moveSpeed
    [SerializeField] private string forwardParam = "Forward";
    [SerializeField] private float damping = 0.1f; // smooth parameter updates

    private Animator anim;
    private Vector3 lastPos;
    private int forwardHash;

    void Awake()
    {
        anim = GetComponent<Animator>();
        forwardHash = Animator.StringToHash(forwardParam);
        lastPos = playerRoot != null ? playerRoot.position : transform.position;

        if (anim.runtimeAnimatorController == null)
            Debug.LogError("Animator has no Controller assigned on child mesh.");
        if (playerRoot == null)
            Debug.LogError("Assign playerRoot (the parent player object).");
    }

    void Update()
    {
        if (anim.runtimeAnimatorController == null || playerRoot == null) return;

        Vector3 displacement = playerRoot.position - lastPos;
        Vector3 worldVel = displacement / Mathf.Max(Time.deltaTime, 0.0001f);
        worldVel.y = 0f;

        float forward = Vector3.Dot(worldVel.normalized, playerRoot.forward);
        float speedNorm = Mathf.Clamp(worldVel.magnitude / moveSpeed, 0f, 1f);

        float forwardAmount = Mathf.Clamp01(speedNorm) * Mathf.Max(0f, forward);
        anim.SetFloat(forwardHash, forwardAmount, damping, Time.deltaTime);

        lastPos = playerRoot.position;
    }
}

