using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private bool isLive=true;
    public Transform target;
    public NavMeshAgent nav;
    Material[] myMaterial;

    [SerializeField]
    Animator anim;
    // Start is called before the first frame update

    public float health;
    public float maxHealth;
    public float speed;

   
    SkinnedMeshRenderer[] skinnedMeshRenderers;

    
  
    void Start()
    {

       
        nav=GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        // 각 SkinnedMeshRenderer의 Material을 변경하거나 접근할 수 있음
        foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
        {
            myMaterial = smr.materials; // SkinnedMeshRenderer의 모든 Material 배열 가져오기
            // materials 배열을 사용하여 필요한 작업 수행
        }
        


    }

    private void OnEnable()
    {
        target = GameManager.instance.player.transform;
        health = maxHealth;
        
        isLive = true;
        gameObject.GetComponent<Animator>().enabled = true;
        
        setColliderState(false);


    }
    public void Init(SpawnData data)
    {
        speed = Random.Range(data.speed-1,data.speed+1);
        maxHealth = data.health;
        health = data.health;

       
    }

    private void Update()
    {
        if (!isLive)
        {
            return;
        }
        if (nav.enabled == false)
        {
            nav.enabled = true;
            nav.speed = speed;
        }
        nav.SetDestination(target.position);
    }




    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.CompareTag("Bullet") || !isLive)
            return;
        Debug.Log("충돌");
        health -= other.GetComponent<Bullet>().damage;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        if (health>0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            Dead();
            isLive = false;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isLive && !GameManager.instance.noHit)
        {
            GameManager.instance.health -= 1;
            GameManager.instance.bloodImage.color = GameManager.instance.bloodColor;
        }
    }






    void Dead()
    {
        isLive = false;
   
        //gameObject.SetActive(false);
        //GameManager.instance.kill++;
        GameManager.instance.GetExp();
        GameManager.instance.kill++;
        gameObject.GetComponent<Animator>().enabled = false;
        
        nav.enabled = false;
        setColliderState(true);
        StartCoroutine(Deadfalse());
    }

    IEnumerator Deadfalse()
    {
        
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    


    void setRigidbodyState(bool state)
    {
        //gameObject.GetComponent<Rigidbody>().isKinematic = state;
        //Rigidbody[] rigidbodies = skeleton.GetComponentsInChildren<Rigidbody>();
        //foreach (Rigidbody r in rigidbodies) {
        //    r.isKinematic = state;
        //}

    }

    void setColliderState(bool state)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            c.enabled = state;
        }
        colliders[0].enabled = true;
    }
}
