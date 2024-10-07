using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    private List<Transform> nearbyUnits = new List<Transform>();
    public Transform nearestTarget;

    private void FixedUpdate()
    {

        ScanForNearbyUnits();
    }

    void ScanForNearbyUnits()
    {
        Collider[] colliders = Physics.OverlapCapsule(transform.position,Vector3.zero,scanRange, targetLayer); // 스캔 반경 내의 모든 충돌체 검색

        if (colliders.Length > 0)
        {
            float shortestDistance = Mathf.Infinity;
         

            foreach (Collider targetCollider in colliders)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetCollider.transform.position);

                if (distanceToTarget < shortestDistance)
                {
                    shortestDistance = distanceToTarget;
                    nearestTarget = targetCollider.transform;
                }
            }

           
        }

       
        
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, scanRange); // 스캔 반경을 시각적으로 표시합니다.
    }


}
