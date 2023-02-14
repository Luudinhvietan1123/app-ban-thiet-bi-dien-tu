using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Ball : MonoBehaviourPunCallbacks
{
    PhotonView view;
    public float ResetBallTime = 2.0f;
    public float StartSpeed = 10f;
    public float MaxSpeed = 40f;
    public float SpeedIncrease = 0.5f;
    private float currentSpeed;
    private Vector2 currentDir;
    private bool resetting = false;
    void Start()
    {
        view = GetComponent<PhotonView>();
        currentSpeed = StartSpeed;
        currentDir = Random.insideUnitCircle.normalized;
    }
    void Update()
    {
        if (resetting)
            return;
        if (PhotonNetwork.PlayerList.Length < 2)
        {
            return;
        }
        if (view.IsMine)
        {
            Vector2 moveDir = currentDir * currentSpeed * Time.deltaTime;
            transform.Translate(new Vector3(moveDir.x, 0f, moveDir.y));
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            currentDir.y *= -1;
        }
        else if (other.tag == "Player")
        {
            currentDir.x *= -1;
        }
        else if (other.tag == "Goal")
        {
            StartCoroutine(resetBall());
            other.SendMessage("GetPoint",SendMessageOptions.DontRequireReceiver);
        }
        currentSpeed += SpeedIncrease;

        currentSpeed = Mathf.Clamp(currentSpeed, StartSpeed, MaxSpeed);
    }
    IEnumerator resetBall()
    {
        resetting = true;
        transform.position = Vector3.zero;
        currentDir = Vector3.zero;
        currentSpeed = 0f;
        yield return new WaitForSeconds(ResetBallTime);
        Start();
        resetting = false;
    }
}
    