using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Paddle : MonoBehaviour
{
    public float MoveSpeed = 10f;
    public float MoveRange = 10f;
    public bool AcceptsInput;
    public 
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        AcceptsInput = view.IsMine;
    }

    void Update()
    {
        if (AcceptsInput)
        {
            if (!AcceptsInput)
                return;
            float input = Input.GetAxis("Vertical");
            Vector3 pos = transform.position;
            pos.z += input * MoveSpeed * Time.deltaTime;
            pos.z = Mathf.Clamp(pos.z, -MoveRange, MoveRange);
            transform.position = pos;
        }
    }

    public void MoveUp()
    {
        if (!AcceptsInput)
            return;
        Vector3 pos = transform.position;
        pos.z += 1 * MoveSpeed * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, -MoveRange, MoveRange);
        transform.position = pos;
    }

    public void MoveDown()
    {
        if (!AcceptsInput)
            return;
        Vector3 pos = transform.position;
        pos.z += - 1 * MoveSpeed * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, -MoveRange, MoveRange);
        transform.position = pos;
    }
}
