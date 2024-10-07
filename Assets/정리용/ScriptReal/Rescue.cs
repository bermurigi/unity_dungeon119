using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Rescue : MonoBehaviour
{
    Collider col;
    NavMeshAgent nav;
    Animator anim;
    private bool isFollow;

    public GameObject light;

    public GameObject jail;

    public bool inputE;
    public GameObject fractured;

    public void BreakTheThing()
    {
        Instantiate(fractured, transform.position, transform.rotation);

    }
    private void Start()
    {
        col = GetComponent<Collider>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float speed = nav.remainingDistance > nav.stoppingDistance && !nav.isPathStale ? 1.0f : 0.0f;

        // 'Speed' 파라미터를 사용해 애니메이션 상태를 업데이트합니다.
        // 여기서는 간단하게 속도가 0보다 크면 걷는 것으로, 그렇지 않으면 멈춰 있는 것으로 간주합니다.
        anim.SetFloat("Speed", speed);
        if (isFollow)
        {
            nav.SetDestination(GameManager.instance.player.transform.position);
        }
        if(Input.GetKey(KeyCode.E))
        {
            inputE = true;
        }
        else
        {
            inputE = false;
        }
        

    }
    private void OnTriggerStay(Collider other)
    {
        
        if (!other.CompareTag("Player"))
            return;
        if (!isFollow && inputE)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.jail);
            GameManager.instance.Rescue++;
            anim.SetTrigger("Hooray");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Rescue);
            jail.SetActive(false);
            BreakTheThing();
            light.SetActive(false);
            
           


            isFollow = true;
            Invoke("AfterRescueSpeed", 4f);
            Debug.Log("구조");
            col.enabled = false;
            anim.SetBool("Rescue", true);
        }
            
        
        
    }
    
    private void AfterRescueSpeed()
    {
        nav.speed = 7;
    }
    
}
