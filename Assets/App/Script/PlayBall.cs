using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBall : MonoBehaviour
{
    Rigidbody rigid;

    public float Speed = 0.25f;
    public float JumpPower = 1f;
    public int itemCount;
    private bool isJump;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }

        rigid.AddForce(new Vector3(h * Speed, 0, v * Speed), ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            isJump = false;
        }

        if (other.gameObject.name == "Goal")
        {
            SceneManager.LoadScene("Level2");
        }
    }
}
