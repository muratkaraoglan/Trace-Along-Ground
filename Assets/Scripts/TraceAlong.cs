using UnityEngine;

public class TraceAlong : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rayDistance;
    Vector3 _direction;
    float _yOffset;
    private void Start()
    {
        _direction = transform.forward;
        _yOffset = transform.position.y;
    }

    private void Update()
    {
        RaycastHit forwardHit, downHit;
        Vector3 up = transform.up;
        bool down = false;
        bool forward = false;

        if (Physics.Raycast(transform.position, (transform.up * -1).normalized, out downHit, Mathf.Infinity))
        {
            if ((downHit.point - transform.position).sqrMagnitude < .1f)
            {
                up = downHit.normal;
                down = true;
            }
        }
        if (Physics.Raycast(transform.position, transform.forward.normalized, out forwardHit, _rayDistance))
        {
            up = forwardHit.normal;
            forward = true;
        }

        if (!down && !forward)
        {
            Vector3 rayDirection = (-transform.forward - transform.up) * .5f;

            if (Physics.Raycast(transform.position, rayDirection.normalized, out RaycastHit hit, 2f))
            {
                up = hit.normal;
            }
        }

        transform.up = up;
        transform.Translate(_moveSpeed * Time.deltaTime * _direction);
 
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward.normalized * _rayDistance);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + transform.up.normalized * -1 * Mathf.Infinity);


    }
}
