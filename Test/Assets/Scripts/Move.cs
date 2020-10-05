using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject Player;
    private int loopCount = 1;
    private float move_speed = 5.0f;
    private Vector3 pos;
    public Material mat;
    public bool onGround = true;
    public float distFromGround = 0.6f;
    public bool isAlive = true;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (isAlive)
        {
            onGround = isGrounded();      
            Player.transform.Translate(Vector3.forward * move_speed * Time.deltaTime);
            pos = Player.transform.position; //Player 포지션 설정
            if (onGround == true)
            {
                GameObject Player2 = GameObject.CreatePrimitive(PrimitiveType.Cube); //PlayerClone 오브젝트 생성
                Player2.transform.position = pos; //PlayerClone 포지션 설정
                Player2.GetComponent<MeshRenderer>().material = mat; //PlayerClone 색 설정
                Player2.GetComponent<BoxCollider>().isTrigger = true; // Player와 Clone이 충돌하지 않도록 함


                if (Input.GetMouseButtonDown(0)) // 클릭시 회전
                {
                    if (loopCount % 2 != 0)
                    {
                        Player.transform.eulerAngles = new Vector3(0, -90, 0);
                        loopCount++;
                    }
                    else
                    {
                        Player.transform.eulerAngles = new Vector3(0, 0, 0);
                        loopCount++;
                    }
                }
            }
        }
    }
    public bool isGrounded()
    {
        return Physics.Raycast(Player.transform.position, Vector3.down, distFromGround); //Player가 밑으로 쏘는 레이저에 바닥이 맞으면 isGround 리턴
    }
    private void OnCollisionEnter(Collision collision) // 장애물과의 충돌여부 
    {
        if(collision.gameObject.tag == "obstacle")
        {
            isAlive = false;
        }
    }
}

