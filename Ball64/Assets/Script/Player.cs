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

    private void Awake() //�Ͼ�� Awake > start
    {
        rigid = GetComponent<Rigidbody>();
        audio = rigid.GetComponent<AudioSource>();
        isJump = false; //�������ȵ� ���¶�� ����
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    private void FixedUpdate() //1�ʿ� 50�� ������ �������� ���� �ϴ� update
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        rigid.AddForce(new Vector3(h, 0, v) * Speed * Time.deltaTime, ForceMode.Impulse);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump") && !isJump) //�������� �ƴҶ�
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
        { //�浹 ������Ʈ�� �������϶�
            itemCount++; // ���� �ø��� 
            audio.Play(); // ���� ��� 
            other.gameObject.SetActive(false); //������ ��Ȱ��ȭ 

        }
        else if (other.CompareTag("FinishPoint"))
        { //FinishPoint�� �浹 �� ��� 
            if (itemCount == manager.TotalItemCount)
            {
                //���� �� (���� ������)
                SceneManager.LoadScene("Stage2");
            }
            else
            {
                //����� 
                SceneManager.LoadScene("Stage1");
            }
        }
    }
}
