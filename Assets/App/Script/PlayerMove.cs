using System.Collections;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public VariableJoystick variableJoystick;
    private Rigidbody rigid;

    public float speed;
    public bool isJump;
    public float JumpPower = 1f;



    #region [=== Unity ===]
    private void Awake()
    {
        Locator.RegisterService(this);
        rigid = GetComponent<Rigidbody>();
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Initialized();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
    #endregion
    public void Initialized()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }
    public void FixedUpdate()
    {
        if (Mathf.Abs(variableJoystick.Vertical) > 0 || Mathf.Abs(variableJoystick.Horizontal) > 0)
        {
            Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            rigid.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            rigid.AddForce(new Vector3(h, 0, v) * 0.1f * speed, ForceMode.Impulse);
        }
    }


    public void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

}