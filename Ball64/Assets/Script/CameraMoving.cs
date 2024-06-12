using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    Transform playerTransform; //�÷��̾��� ��ġ���� ���� 
    Vector3 Offset;//������ ( �÷��̾�� ī�޶� ������ ������ ���� ) 

    // Start is called before the first frame update
    void Awake()
    {//�ʱ�ȭ

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //�÷��̾��� ��ġ������ Tag�� �̿��� ������


        Offset = transform.position - playerTransform.position;
        //�ڽ��� ��ġ - �÷��̾��� ��ġ = �÷��̾�� �ڽ��� ������ ���Ͱ� 
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //ī�޶��� ��ġ = �÷��̾��� ��ġ + �÷��̾�� �ڽ��� ������ ������ 
        transform.position = playerTransform.position + Offset;
    }
}
