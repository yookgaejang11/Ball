using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    AudioSource audio;
    public GameManagerLogic manager;
    public float jumpPower;
    bool isJump;
    public int itemCount;
    public int Speed;
    Rigidbody rigid;

    private void Awake() //일어난다 Awake > start
    {
        rigid = GetComponent<Rigidbody>();
        audio = rigid.GetComponent<AudioSource>();
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
        if(other.tag == "Jump_Pad")
        {
            rigid.AddForce(Vector3.up * 1, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor") isJump = false;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Item"))
        { //충돌 오브젝트가 아이템일때
            itemCount++; // 점수 올리기 
            audio.Play(); // 사운드 재생 
            other.gameObject.SetActive(false); //아이템 비활성화 

        }
        else if (other.CompareTag("FinishPoint"))
        { //FinishPoint와 충돌 할 경우 
            if (itemCount == manager.TotalItemCount)
            {
                //게임 끝 (다음 레벨로)
                SceneManager.LoadScene("Stage2");
            }
            else
            {
                //재시작 
                SceneManager.LoadScene("Stage1");
            }
        }
    }
}
