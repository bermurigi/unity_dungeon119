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

        // 'Speed' �Ķ���͸� ����� �ִϸ��̼� ���¸� ������Ʈ�մϴ�.
        // ���⼭�� �����ϰ� �ӵ��� 0���� ũ�� �ȴ� ������, �׷��� ������ ���� �ִ� ������ �����մϴ�.
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
            Debug.Log("����");
            col.enabled = false;
            anim.SetBool("Rescue", true);
        }
            
        
        
    }
    
    private void AfterRescueSpeed()
    {
        nav.speed = 7;
    }
    
}
