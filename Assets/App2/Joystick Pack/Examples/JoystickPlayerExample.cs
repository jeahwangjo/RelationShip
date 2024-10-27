﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JoystickPlayerExample : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    Rigidbody rigid;
    public Transform Face;

    public float JumpPower = 1f;
    public int itemCount;
    private bool isJump;

    Coroutine FaceChangeTimeWait;

    [SerializeField] Texture Sleep;
    [SerializeField] Texture GetItem;

    private void Awake()
    {
        Locator.RegisterService(this);
        rigid = GetComponent<Rigidbody>();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Face.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    public void FixedUpdate()
    {
        if (variableJoystick.Vertical > 0 || variableJoystick.Horizontal > 0)
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
        if (transform.position.y < -1)
        {
            SceneManager.LoadScene("Level1");
            Locator.GetService<GameManager>().CurrentLevel = 0;
        }
    }

    public void GetItemCall()
    {
        itemCount++;
        Face.gameObject.GetComponent<MeshRenderer>().material.mainTexture = GetItem;
        if (FaceChangeTimeWait != null)
            StopCoroutine(FaceChangeTimeWait);
        FaceChangeTimeWait = StartCoroutine(FaceTimeChange());
    }

    IEnumerator FaceTimeChange()
    {
        yield return new WaitForSeconds(0.7f);
        Face.gameObject.GetComponent<MeshRenderer>().material.mainTexture = Sleep;
        FaceChangeTimeWait = null;
    }

    public void Jump()
    {
        if (!isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            isJump = false;
        }
        Debug.Log($"JoystickPlayerExample :: OnCollisionEnter :: Locator.GetService<GameManager>().CurrentItemCount : {Locator.GetService<GameManager>().CurrentItemCount}");
        Debug.Log($"JoystickPlayerExample :: OnCollisionEnter :: itemCount : {itemCount}");
        if (other.gameObject.name == "Goal"
            && Locator.GetService<GameManager>().CurrentItemCount <= itemCount)
        {
            Locator.GetService<GameManager>().LevelComplete();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("DeadZone"))
        {

        }
    }
}