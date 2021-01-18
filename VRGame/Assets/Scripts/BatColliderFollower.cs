using UnityEngine;

public class BatColliderFollower : MonoBehaviour
{
    private BatCollider _batFollower;
    public Rigidbody guide;
    private Rigidbody _rigidbody;
    private Vector3 _velocity;

    [SerializeField]
    private float _sensitivity = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        guide = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        Vector3 destination = _batFollower.transform.position;
        _rigidbody.transform.rotation = transform.rotation;

        _velocity = (destination - _rigidbody.transform.position) * _sensitivity;
        _velocity = _velocity + guide.velocity;

        _rigidbody.velocity = _velocity;
        transform.localScale = _batFollower.transform.localScale;
        transform.rotation = _batFollower.transform.rotation;
    }

    public void SetFollowTarget(BatCollider batFollower)
    {
        _batFollower = batFollower;
    }

    public void SetGuide(Rigidbody guider)
    {
        guide = guider;
    }
}
