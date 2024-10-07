using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(NavMeshAgent))]
public class NaviManager : MonoBehaviour
{
    // 컴포넌트
    private LineRenderer mLineRenderer; //라인 렌더러
    private NavMeshAgent mNavAgent; //네브 에이전트

    // 네브 타겟 위치
    private Vector3 mTargetPos; //네브 타겟의 위치
    private Transform mOriginTransform; //네브의 시작점 위치

    public void InitNaviManager(Transform trans, Vector3 pos, float updateDelay)
    {
        SetOriginTransform(trans);
        SetDestination(pos);

        mLineRenderer = GetComponent<LineRenderer>();
        mLineRenderer.startWidth = 0.5f;
        mLineRenderer.endWidth = 0.5f;
        mLineRenderer.positionCount = 0;

        //Material mat = new Material(Shader.Find("Samples/Shader Graphs/14.0.8/Procedural Patterns/SampleTwinkle"));
        //mat.SetColor("_BaseColor", Color.green);
        //mLineRenderer.material = mat;

        mNavAgent = GetComponent<NavMeshAgent>();
        mNavAgent.isStopped = true;
        mNavAgent.radius = 1.0f;
        mNavAgent.height = 1.0f;

        StartCoroutine(UpdateNavi(updateDelay));
    }

    private IEnumerator UpdateNavi(float updateDelay)
    {
        WaitForSeconds delay = new WaitForSeconds(updateDelay);
        while (true)
        {
            //타겟 위치 설정
            transform.position = mOriginTransform.position;
            mNavAgent.SetDestination(mTargetPos);

            //패스 그리기
            DrawPath();

            yield return delay;
        }
    }
    public void SetDestination(Vector3 pos)
    {
        mTargetPos = pos;
    }

    public void SetOriginTransform(Transform trans)
    {
        mOriginTransform = trans;
        transform.position = mOriginTransform.position;
    }
    private void DrawPath()
    {
        mLineRenderer.positionCount = mNavAgent.path.corners.Length;
        mLineRenderer.SetPosition(0, transform.position);

        if (mNavAgent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < mNavAgent.path.corners.Length; ++i)
        {
            mLineRenderer.SetPosition(i, mNavAgent.path.corners[i]);
        }
    }
}
