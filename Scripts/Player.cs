using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    private float _speed = 6.0f;

    [SerializeField]
    private float _jumpSpeed = 8.0f;

    [SerializeField]
    private float _gravity = 20.0f;

    [SerializeField]
    private float _rotationSpeed = 90.0f;

    private Vector3 _motion = Vector3.zero;
    private float _pitch = 0.0f, _yaw = 0.0f;
    private Weapon _weapon;

    static void LockCursor(bool value)
    {       
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !value;       
    }
    
    void Start ()
    {
        LockCursor(true);
        _weapon = GameObject.Find("Weapon").GetComponent<Weapon>();      
	}	
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LockCursor(false);

        _yaw += Input.GetAxis("Mouse X") * _rotationSpeed * Time.deltaTime;
        _pitch -= Input.GetAxis("Mouse Y") * _rotationSpeed * Time.deltaTime;

        if (_yaw >= 360)
            _yaw -= 360;

        _pitch = Mathf.Clamp(_pitch, -90, 90);
        transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);

        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            _motion = new Vector3(Input.GetAxis("Horizontal"), .0f, Input.GetAxis("Vertical"));
            _motion = transform.TransformDirection(_motion);
            _motion *= _speed;
            if (Input.GetButton("Jump"))
            {
                _motion.y = _jumpSpeed;
            }            
        }
        _motion.y -= _gravity * Time.deltaTime;
        controller.Move(_motion * Time.deltaTime);
    }
}
