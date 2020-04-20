using UnityEngine;
using System.Collections;

public class CameraExtensions : MonoBehaviour
{
    private Transform player;
    private Camera mainCamera;

    public Vector2 margin = new Vector2(1, 1); // If the player stays inside this margin, the camera won't move.
    public Vector2 smoothing = new Vector2(3, 3); // The bigger the value, the faster is the camera.

    public BoxCollider2D cameraBounds;

    private Vector3 min, max;

    public bool isFollowing;

    [SerializeField]
    private float xMin;
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMin;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float offset;
    Vector3 velocity = Vector3.zero;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 targetPos = player.transform.position;

        this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPos, ref velocity, 0.15f);


        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, xMin, xMax),
            Mathf.Clamp(this.transform.position.y, yMin, yMax),
            transform.position.z - offset);

    }
}