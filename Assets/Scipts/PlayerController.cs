using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Animator anim;
    [SerializeField] float shiftSpeed = 10f;

    [SerializeField] float movementSpeed = 5f;
    Vector3 direction;
    float currentSpeed;
    float stamina = 5f;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Image crosshairImage;
    [SerializeField] private float rayDistance = 100f;
    private Color defaultColor = Color.white;
    private Color highlightColor = Color.green;

    [SerializeField] GameManager gameManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        currentSpeed = movementSpeed;
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                stamina -= Time.deltaTime;
                currentSpeed = shiftSpeed;
            }
            else
            {
                currentSpeed = movementSpeed;
            }
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            stamina += Time.deltaTime;
            currentSpeed = movementSpeed;
        }
        if (stamina > 5f)
        {
            stamina = 5f;
        }
        else if (stamina < 0)
        {
            stamina = 0;
        }

        ChangeAnimations();
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.fixedDeltaTime);
        DetectDog();
    }

    private void ChangeAnimations()
    {
        anim.SetBool("Walk", (direction.x != 0 || direction.z != 0) && currentSpeed == movementSpeed);
        anim.SetBool("Run", (direction.x != 0 || direction.z != 0) && currentSpeed == shiftSpeed);
        anim.SetBool("Wiggling", direction.x == 0 && direction.z == 0);
    }

    private void DetectDog()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = mainCamera.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.CompareTag("HiddenDog"))
            {
                crosshairImage.color = highlightColor;
                Destroy(hit.collider.gameObject);
                gameManager.ChangeScore(); 
            }
            else
            {
                crosshairImage.color = defaultColor;
            }
        }
        else
        {
            crosshairImage.color = defaultColor;
        }
    }

}