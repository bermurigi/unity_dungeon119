using Invector.vCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
   // public Scanner scanner;
    public vThirdPersonController vTPC;
    public float speed;
    public Animator anim;

    public float attackTime;
    public bool canAttack;


    

    public GameObject slashBullet;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        //scanner = GetComponent<Scanner>();
        vTPC = GetComponent<vThirdPersonController>();
    }
    private void Start()
    {
        anim.SetInteger("CNum", GameManager.instance.CNum);
    }
    private void Update()
    {
        

        SpeedUp();

        if (Input.GetMouseButton(1) && canAttack)
        {
            Attack();
        }

        if (canAttack)
            return;
        attackTime += Time.deltaTime;
        if (attackTime > 0.6f)
            canAttack = true;


        
    }

    public void Attack()
    {
        anim.SetTrigger("IsAttack");
        canAttack = false;
        attackTime = 0f;

        StartCoroutine(spawnSlash(0.3f));
        
    }
    IEnumerator spawnSlash(float delay)
    {

        yield return new WaitForSeconds(delay);
        if(GameManager.instance.CNum==4)
        {
            slashBullet.SetActive(true);
            slashBullet.transform.position = transform.position+new Vector3(0,1,0);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Attack);
        }
        else
        {
            slashBullet.SetActive(true);
            slashBullet.transform.position = transform.position + transform.forward * 3.0f;
            slashBullet.transform.position += new Vector3(0, 1, 0);
            slashBullet.transform.rotation = transform.rotation;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Attack);
        }
       

    }



    public void SpeedUp()
    {
        vTPC.freeSpeed.runningSpeed = speed* GameManager.instance.angry;
        vTPC.freeSpeed.walkSpeed = speed * 1.2f * GameManager.instance.angry;
        vTPC.freeSpeed.sprintSpeed = speed * 1.5f * GameManager.instance.angry;
    }
}
