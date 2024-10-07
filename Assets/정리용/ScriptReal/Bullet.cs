using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody rigid;

    public int id;

    public float timer;

    public bool basicAtk;

  
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public float range = 100000f;
    private void Update()
    {
        if(GameManager.instance.CNum==6)
        {
            if(basicAtk)
            {
                damage = 500 * GameManager.instance.angry;
            }
        }
        switch (id)
        {
            case 1:
                timer += Time.deltaTime;
                if (timer > 10f)
                {
                    timer = 0f;
                    gameObject.SetActive(false);
                }
                break;
            case 3:
                timer += Time.deltaTime;
                if (timer > 0.3f)
                {
                    timer = 0f;
                    gameObject.SetActive(false);
                }
                break;
            case 4:
                timer += Time.deltaTime;
               
                if (timer > damage)
                {
                    timer = 0f;
                    gameObject.SetActive(false);
                    GameManager.instance.noHit = false;
                    //캐릭터 무적처리해줘야함
                }
                break;
            case 5:
                timer += Time.deltaTime;
                if (timer > 2.2f)
                {
                    timer = 0f;
                    gameObject.SetActive(false);



                }
                else if (timer > 1.1f)
                {
                    transform.GetComponent<Collider>().enabled = false;
                }
                else if (timer > 1f)
                {


                    transform.GetComponent<Collider>().enabled = true;
                }
                break;
                


        }
    }


    public void Init(float damage,int per,Vector3 dir,int id)
    {
        this.damage = damage;
        this.per = per;
        this.id = id;
        if(per > -1)
        {
            rigid.AddForce(dir , ForceMode.Impulse);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Out"))
            gameObject.SetActive(false);
        if (!other.CompareTag("Enemy") || per == -1)
            return;
        per--;
        Debug.Log("충돌");
        if(per==0)
        {
            rigid.velocity = Vector3.zero;
            gameObject.SetActive(false);
            
        }
    }
}
