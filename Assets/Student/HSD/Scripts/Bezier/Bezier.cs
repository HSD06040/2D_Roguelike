using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BezierProjectile2D : MonoBehaviour
{
    public Transform target;

    public float bezierDuration = 1f;
    public float postBezierSpeed = 10f;

    [SerializeField] private float x;
    [SerializeField] private float y;

    private float timeElapsed = 0f;
    private bool isBezierDone = false;
    private Rigidbody2D rb;
    private Vector3 start;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        start = transform.position;
    }

    void FixedUpdate()
    {
        if (!isBezierDone)
        {
            timeElapsed += Time.fixedDeltaTime;
            float t = timeElapsed / bezierDuration;

            Vector2 dir = (target.position - start).normalized;
            Vector2 control = Vector2.Lerp(start, target.position, x) + Vector2.Perpendicular(dir) * y;

            if (t >= 1f)
            {
                t = 1f;
                isBezierDone = true;
                               
                Vector2 lastPos = CalculateQuadraticBezierPoint(t - 0.01f, start, control, target.position);
                Vector2 finalPos = CalculateQuadraticBezierPoint(t, start, control, target.position);
                Vector2 direction = (finalPos - lastPos).normalized;

                rb.velocity = direction * postBezierSpeed;
                return;
            }

            Vector2 pos = CalculateQuadraticBezierPoint(t, start, control, target.position);
            rb.MovePosition(pos);

        }
    }

    Vector2 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        Vector2 a = p0;
        Vector2 b = p1;
        Vector2 c = p2;

        return Mathf.Pow(1 - t, 2) * a +
               2 * (1 - t) * t * b +
               Mathf.Pow(t, 2) * c;
    }
}
