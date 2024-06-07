using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower;
    bool isJump;
    public int Speed;
    Rigidbody rigid;

    private void Awake() //일어난다 Awake > start
    {
        rigid = GetComponent<Rigidbody>();
        isJump = false; //점프가안된 상태라고 가정
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    private void FixedUpdate() //1초에 50번 엔진이 물리연산 전에 하는 update
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v) * Speed * Time.deltaTime, ForceMode.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !isJump) //점프상태 아닐때
        {
            isJump = true;
            rigid.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == "Cube")
        {
            rigid.AddForce(Vector3.up * 1, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor") isJump = false;
    }
}
