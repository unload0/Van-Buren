using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private GameObject cameraHolder;
    private GameObject modelHolder;

    private float movSpeed = 2f;
    private float cameraRotSpeed = 2f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private Vector3 moveDir;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        cameraHolder = transform.Find("CameraHolder").gameObject;
        modelHolder = transform.Find("GFX").gameObject;

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

        playerRB.linearVelocity += new Vector3(moveDir.normalized.x * movSpeed,
            playerRB.linearVelocity.y, moveDir.normalized.z * movSpeed) * 0.2f;

        // playerRB.AddForce(moveDir, ForceMode.Acceleration);

        Vector3 flatVel = new Vector3(playerRB.linearVelocity.x, 0f, playerRB.linearVelocity.z);

        if (flatVel.magnitude > movSpeed)
        {
            Vector3 limitedVel = Vector3.ClampMagnitude(flatVel, movSpeed);
            playerRB.linearVelocity = new Vector3(limitedVel.x, playerRB.linearVelocity.y, limitedVel.z);
        }
        else if (flatVel.magnitude != 0 && moveDir.magnitude == 0)
        {
            playerRB.linearVelocity -= new Vector3(playerRB.linearVelocity.x, 0f, playerRB.linearVelocity.z) * 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();

        Movement();

        Debug.Log(playerRB.linearVelocity.magnitude);
    }

    void CameraControl()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * (cameraRotSpeed * 100f);
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * (cameraRotSpeed * 100f);

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

        moveDir = cameraDir * (moveVect.normalized * 20f);

        // transform.Translate(moveDir * movSpeed * Time.deltaTime);
    }
}
