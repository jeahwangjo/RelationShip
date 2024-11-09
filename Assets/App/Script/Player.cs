using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int itemCount;
    public Transform Face;
    Coroutine FaceChangeTimeWait;
    [SerializeField] Texture Sleep;
    [SerializeField] Texture GetItem;
    #region [=== Unity===]
    private void Awake()
    {
        Locator.RegisterService(this);
    }
    void Update()
    {
        Face.transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        if (transform.position.y < -1)
        {
            Locator.GetService<SceneLevelManager>().LoadLevel();
            GetComponent<PlayerMove>().Initialized();
        }
    }
    #endregion
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            GetComponent<PlayerMove>().isJump = false;
        }
        if (other.gameObject.name == "Goal"
            && Locator.GetService<SceneLevelManager>().CurrentItemCount <= itemCount)
        {
            Locator.GetService<SceneLevelManager>().LevelComplete();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("DeadZone"))
        {

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
}
