using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerInput playerInput;
    HeadMovement headMovement;
    BodyMovement bodyMovement;
    SmoothHead smoothHead;
    Rigidbody rb; // main rb of body
    private void Start()
    {
        headMovement = GetComponentInChildren<HeadMovement>();
        bodyMovement = GetComponentInChildren<BodyMovement>();
        smoothHead = GetComponentInChildren<SmoothHead>();
        rb = bodyMovement.gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        playerInput.tick();
        headMovement.tick(playerInput);
        bodyMovement.Tick(playerInput , headMovement.gameObject.transform);
    }
    private void LateUpdate()
    {
        smoothHead.Tick(rb , headMovement.gameObject.transform);
    }
}
