using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Security.Cryptography;
using UnityEngine.EventSystems;

public class Paddle : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float MoveSpeed = 10f;
    public float MoveRange = 10f;
    public bool AcceptsInput;
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        AcceptsInput = view.IsMine;
        if (view.IsMine)
        {
            GameControler.instance.controlUpButton.onClick.AddListener(MoveUp);
            GameControler.instance.controlDownButton.onClick.AddListener(MoveDown);
        }
    
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

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
        pos.z += 15 * MoveSpeed * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, -MoveRange, MoveRange);
        transform.position = pos;
    }

    public void MoveDown()
    {
        if (!AcceptsInput)
            return;
        Vector3 pos = transform.position;
        pos.z += - 15 * MoveSpeed * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, -MoveRange, MoveRange);
        transform.position = pos;
    }
}
