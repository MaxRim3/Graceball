using UnityEngine;

public class BatColliderFollower : MonoBehaviour
{
    public BatCollider _batGuide;
    public Rigidbody guide;
    public GameObject guideTwo;
    private Rigidbody _rigidbody;
    private Vector3 _velocity;
    public bool colliding;

    [SerializeField]
    private float _sensitivity = 10f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
       // _batGuide.GetComponent<BatCollider>().follower = this.gameObject;
        //guide = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        //guideTwo = GameObject.FindWithTag("PlayerChild");
        //this.gameObject.transform.parent = guideTwo.transform;
        
    }

    private void FixedUpdate()
    {
        if (_batGuide != null)
        {
            Vector3 destination = _batGuide.transform.position;
            _rigidbody.transform.rotation = transform.rotation;

            _velocity = (destination - _rigidbody.transform.position) * _sensitivity;
            _velocity = _velocity + guide.velocity;

            _rigidbody.velocity = _velocity;
            transform.localScale = _batGuide.transform.lossyScale + _batGuide.transform.lossyScale;
            transform.rotation = _batGuide.transform.rotation;

            //if (colliding)
            //{
            //    guide.AddForce(-_velocity);
            //    print("im colliding");
            //}
        }
        else
        {
            print("_batGuide is null on object" + this.gameObject.name);
        }
    }

    public void SetFollowTarget(BatCollider batGuide)
    {
        _batGuide = batGuide;
    }

    public void SetGuide(Rigidbody guider)
    {
        guide = guider;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            colliding = true;
        }
        colliding = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            colliding = false;
        }
    }


}
