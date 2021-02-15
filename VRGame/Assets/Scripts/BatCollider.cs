using UnityEngine;

public class BatCollider : MonoBehaviour
{
    public Transform parent;
    [SerializeField]
    private BatColliderFollower _batCapsuleFollowerPrefab;
    public GameObject player;
    public bool colliding;
    public bool collisionHappened;
    public GameObject followerObj;
    public GameObject follower;

    private void SpawnBatCapsuleFollower()
    {
        //var follower = Instantiate(_batCapsuleFollowerPrefab);
        follower.transform.position = transform.position;
        follower.GetComponent<BatColliderFollower>().SetFollowTarget(this);
        followerObj = follower.gameObject;
        follower.GetComponent<BatColliderFollower>()._batGuide = this.gameObject.GetComponent<BatCollider>();
        //follower.GetComponent<BatColliderFollower>().guide = player.gameObject.GetComponent<Rigidbody>();
        //follower.GetComponent<CollisionControl>().guide = player.gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        //player = GameObject.FindWithTag("Player");
        SpawnBatCapsuleFollower();
    }

    public void FixedUpdate()
    {
        if (collisionHappened)
        {
            //player.GetComponent<Rigidbody>().AddForce(-followerObj.GetComponent<Rigidbody>().velocity);
            //print("colliding");
        }
        //if (colliding && !collisionHappened)
        //{
        //    player.GetComponent<Rigidbody>().AddForce(followerObj.GetComponent<Rigidbody>().velocity);
        //}
    }

    public void setColliding(bool boolean)
    {
        colliding = boolean;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            collisionHappened = true;
        }
        //if (collisionHappened)
        //{
        //    collisionHappened = false;
        //}
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            colliding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            colliding = false;
            collisionHappened = false;
        }
    }
    //private void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.gameObject.tag == "Wall")
    //    {
    //        colliding = true;
    //    }
    //}
    //private void OnTriggerExit(Collider collider)
    //{
    //    if (collider.gameObject.tag == "Wall")
    //    {
    //        colliding = false;
    //    }
    //}


}
