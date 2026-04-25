using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private GameObject cameraHolder;
    private GameObject modelHolder;
    [SerializeField] public GameObject armatureHead;

    private Animator playerAnimator;

    private float movSpeed = 1.4f;
    private float movSpeedCrouched = 0.6f;
    private float cameraRotSpeed = 2f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private Vector3 moveDir;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        cameraHolder = transform.Find("CameraHolder").gameObject;
        modelHolder = transform.Find("GFX").gameObject;
        playerAnimator = this.GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void FixedUpdate()
    {
        // playerRB.linearVelocity = new Vector3(moveDir.normalized.x * movSpeed,
        // playerRB.linearVelocity.y, moveDir.normalized.z * movSpeed);
        float getMoveSpeed = playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle") 
        ? movSpeed : playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("crouch")  
        || playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("crouch_idle") 
        ? movSpeedCrouched : movSpeed;

        playerRB.linearVelocity += new Vector3(moveDir.normalized.x * getMoveSpeed,
            playerRB.linearVelocity.y, moveDir.normalized.z * getMoveSpeed) * 0.2f;

        // playerRB.AddForce(moveDir, ForceMode.Acceleration);

        Vector3 flatVel = new Vector3(playerRB.linearVelocity.x, 0f, playerRB.linearVelocity.z);


        if (flatVel.magnitude > getMoveSpeed)
        {
            Vector3 limitedVel = Vector3.ClampMagnitude(flatVel, getMoveSpeed);
            playerRB.linearVelocity = new Vector3(limitedVel.x, playerRB.linearVelocity.y, limitedVel.z);
        }
        else if (flatVel.magnitude != 0 && moveDir.magnitude == 0)
        {
            playerRB.linearVelocity -= new Vector3(playerRB.linearVelocity.x, 0f, playerRB.linearVelocity.z) * 0.1f;
        }

        playerAnimator.SetFloat("playerVel", flatVel.magnitude);
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();

        Movement();
    }

    void CameraControl()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * (cameraRotSpeed * 1f);
        float mouseY = Input.GetAxisRaw("Mouse Y") * (cameraRotSpeed * 1f);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 50);

        yRotation += mouseX;

        cameraHolder.transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        modelHolder.transform.localRotation = Quaternion.Euler(0f, cameraHolder.transform.localEulerAngles.y, 0f);
    }

    void Movement()
    {
        Vector3 moveVect = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        Quaternion cameraDir = Quaternion.Euler(0, cameraHolder.transform.eulerAngles.y, 0);

        moveDir = cameraDir * moveVect.normalized;

        // transform.Translate(moveDir * movSpeed * Time.deltaTime);
        bool isCrouching = playerAnimator.GetBool("playerCrouched");

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if(!isCrouching)
            {
                moveVect -= moveVect;
            }
            playerAnimator.SetBool("playerCrouched", true);
        }
        else
        {
            playerAnimator.SetBool("playerCrouched", false);
        }


        Transform target = isCrouching ? armatureHead.transform : this.transform;
        Vector3 targetPos = isCrouching || !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle") ? armatureHead.transform.position : 
        this.transform.TransformPoint(new Vector3(0f, 0.4f, 0f));

        cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position, targetPos, 0.1f);
        // cameraHolder.transform.rotation = Quaternion.Lerp(cameraHolder.transform.rotation, target.rotation, 0.1f);
    }
}
