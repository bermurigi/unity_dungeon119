using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;


    Vector3 offset;
    public float timer;
    public Player player;

    public Transform shield;
    private void Awake()
    {
        
       
    }
    private void Start()
    {
        player = GameManager.instance.player.gameObject.GetComponent<Player>();
        offset = transform.position - player.transform.position;
       
    }

   
    private void Update()
    {
        switch(id)
        {
            case 0:
                //transform.position = player.transform.position+offset;
                transform.position = player.transform.position;
                transform.Rotate(Vector3.up, speed);

                offset = transform.position - player.transform.position;
                break;
            case 1:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
            case 2:
                transform.position = player.transform.position;
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    GetComponentInChildren<Collider>().enabled = false;
                    GetComponentInChildren<Collider>().enabled = true;
                }
                break;
            case 3:
                transform.position = player.transform.position;
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Attack();
                }
                break;
            case 4:
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Shield();
                }
                break;
            case 5:
                transform.position = player.transform.position;
                timer += Time.deltaTime;
                if (timer > speed)
                {
                    timer = 0f;
                    Attack2();
                }
                break;
            default:
                
                break;
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;
        if (id == 0)
            Batch();

        GameManager.instance.weaponGroup.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = GameManager.instance.weaponGroup;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount;
        
        for(int index=0; index< GameManager.instance.pool.prefabs.Length; index++)
        {
            if(data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabId = index;
                break;
            }
        }
        switch (id)
        {
            case 0:
                speed = 2;
                Batch();
                break;
            case 2:
                speed = 0.5f;
                Batch2();
                break;
            case 3:
                speed = 1.5f;
                break;
            case 4:
                speed = 10f;
                
                break;
            case 5:
                speed = 20f;
                break;
            default:
                speed = 1f;
                break;
        }
        GameManager.instance.weaponGroup.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }
    void Batch()
    {
        for(int index=0; index<count; index++)
        {
            Transform bullet;
            if(index<transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            
          

            bullet.localPosition = new Vector3(0,-0.5f,0);
            bullet.localRotation = Quaternion.identity;
            Vector3 rotVec = Vector3.up * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1f, Space.World);
            bullet.Translate(bullet.forward * 1f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage,-1,Vector3.zero,id); //-1은 무한
            
            
        }
    }

    void Batch2()
    {
       
            Transform bullet;
 
            bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;
          



            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;
           
            
          
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero,id); //-1은 무한


    }
    

    void Fire()
    {
        float delay = 0.1f; // 총알 생성 간격 (초)
        
        //dir = dir.normalized;
        StartCoroutine(SpawnFireWithDelay(count, delay));
        
        //bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
       
     
        
    }
    IEnumerator SpawnFireWithDelay(int count, float delay) //돌떨구기
    {
        for (int i = 0; i < count; i++)
        {
            // 랜덤한 x, y, z 좌표 설정
            float randomX = Random.Range(-3f, 3f); // 원하는 범위로 변경 가능
            float randomY = Random.Range(18f, 20f); // 원하는 범위로 변경 가능
            float randomZ = Random.Range(-3f, 3f); // 원하는 범위로 변경 가능

            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.localPosition = new Vector3(player.transform.position.x + randomX,
                                           player.transform.position.y + randomY,
                                          player.transform.position.z + randomZ);
            Debug.Log(bullet.position);

            bullet.GetComponent<Bullet>().Init(damage, 1, Vector3.zero, id); // count 대신 1을 전달하여 한 번에 하나씩 생성
            yield return new WaitForSeconds(delay);
        }
    }

    void Attack()
    {
        float delay = 0.1f; // 총알 생성 간격 (초)

        StartCoroutine(SpawnBulletsWithDelay(count, delay));
        //dir = dir.normalized;



    }
    IEnumerator SpawnBulletsWithDelay(int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            Vector3 localForward = bullet.transform.forward;

            Quaternion additionalRotation = Quaternion.Euler(0, -40f + (360 * i / count), 0);
            bullet.rotation = additionalRotation;

            bullet.position = player.transform.position + localForward * 3f + new Vector3(0,1,0);
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero, id);

            yield return new WaitForSeconds(delay);
        }
    }
    void Attack2()
    {
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        Vector3 localForward = bullet.transform.forward;




        bullet.position = player.transform.position+new Vector3(0,1f,0);

        bullet.rotation = player.transform.rotation;

        bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero, id);
        bullet.GetComponent<Collider>().enabled = false;

        //dir = dir.normalized;



    }
    void Shield()
    {
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        GameManager.instance.noHit = true;
        bullet.position = player.transform.position;
        bullet.transform.parent = player.transform;
        bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero, id);

    }
}
