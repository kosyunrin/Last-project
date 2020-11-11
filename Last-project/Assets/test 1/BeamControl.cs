using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamControl : MonoBehaviour
{

    public enum BeamState
    {
        BS_WAIT,
        BS_START,
        BS_EXIST,
        BS_OVER
    };

    public BeamState        eState                  = BeamState.BS_WAIT;
    public float            fChargeTime             = 1.0f;
    public float            fLaunchTime             = 0.2f;
    public float            fDurationTime           = 2.0f;
    public float            fLength                 = 1.0f;
    public float            fLaserOverOffset        = 0.5f;
    public float            fLaserParticleOffset    = 0.2f;
    private float           fCurrentTime            = 0.0f;
    private Vector3         vecDirection            = Vector3.left;

    public GameObject       objLinePerfab           = null;
  
    public GameObject       objParticlePerfab       = null;
    
    private RayTest         cRayTest                = null;
    private LineRenderer    rLineRender             = null;
    private Renderer        rParticleRender         = null;

    private void Clear()
    {
        rLineRender.enabled = false;
        eState = BeamState.BS_WAIT;
        fCurrentTime = 0.0f;
    }

    public void Init()
    {
        cRayTest = new RayTest();
        var objL = GameObject.Instantiate(objLinePerfab);
        objL.transform.localScale = transform.localScale;
        objLinePerfab = objL;
      
        rLineRender = objL.GetComponent<LineRenderer>();
        rLineRender.positionCount = 2;

        var objP = GameObject.Instantiate(objParticlePerfab);
        objP.transform.localScale = transform.localScale;
        objParticlePerfab = objP;
        rParticleRender = objP.GetComponent<Renderer>();
        rParticleRender.enabled = false;

        Clear();
    }

    public void SetStartPosition(Vector3 vec)
    {
        transform.position = vec;
    }

    public void SetDirection(Vector3 vec)
    {
        vecDirection = vec;
    }
    public void RangeAttack(Vector3 vec)
    {
       
        SetDirection(vec);
        fChargeTime = 0.1f;
        fLaunchTime = 0.1f;
        fDurationTime = 0.1f;
        fLength = 10.0f;
        OnLaunchBeam();
    }
    public void CirleRangeAttack(Vector3 vec)
    {
      
        SetDirection(vec);
        fChargeTime = 0.1f;
        fLaunchTime = 0.1f;
        fDurationTime =1.5f;
        fLength = 10.0f;
        OnLaunchBeam();
    }

    public void OnLaunchBeam()
    {
        if (eState == BeamState.BS_WAIT)
        {
            cRayTest.vecStartPosition = transform.position;
            cRayTest.vecDirection = vecDirection;
            cRayTest.fLength = 0.0f;
            bool bHit = cRayTest.OnHitted();
            rLineRender.SetPosition(0, cRayTest.vecStartPosition);
            rLineRender.SetPosition(1, cRayTest.vecCastPosition);
            eState = BeamState.BS_START;
            fCurrentTime = 0.0f;
            rParticleRender.enabled = true;
        }
    }

    private void UpdateRayTest()
    {
        cRayTest.vecStartPosition = transform.position;
        cRayTest.vecDirection = vecDirection;
        bool bHit = cRayTest.OnHitted();
        rLineRender.SetPosition(0, cRayTest.vecStartPosition);
        rLineRender.SetPosition(1, cRayTest.vecCastPosition + fLaserOverOffset * vecDirection);
    }

    private void UpdateStartState()
    {

        float tstart = Mathf.Lerp(0.0f, fChargeTime, fCurrentTime / fChargeTime);
        if (Mathf.Abs(tstart - fChargeTime) <= 0.01f)
        {
            eState = BeamState.BS_EXIST;
            fCurrentTime = 0.0f;
            rLineRender.enabled = true;
        }

    }

    private void UpdateExistState()
    {

        float texist = Mathf.Lerp(0.0f, fLaunchTime + fDurationTime, fCurrentTime / (fLaunchTime + fDurationTime));
        if (texist <= fLaunchTime)
        {
            float t = texist / fLaunchTime;
            cRayTest.fLength = Mathf.Lerp(0.0f, fLength, t);
        }
        if (Mathf.Abs(texist - (fLaunchTime + fDurationTime)) <= 0.01f)
        {
            eState = BeamState.BS_OVER;
            fCurrentTime = 0.0f;
            rParticleRender.enabled = false;
        }

    }

    private void UpdateOverState()
    {

        float texist = Mathf.Lerp(0.0f, fLaunchTime, fCurrentTime / fLaunchTime);
        float t = texist / fLaunchTime;
        Vector3 startPos = Vector3.Lerp(cRayTest.vecStartPosition, cRayTest.vecCastPosition, t);
        rLineRender.SetPosition(0, startPos);
        if (Mathf.Abs(texist - fLaunchTime) <= 0.01)
        {
            eState = BeamState.BS_WAIT;
            rLineRender.enabled = false;
        }
    }

    private void UpdateBeam()
    {
        if (eState != BeamState.BS_WAIT)
            fCurrentTime += Time.deltaTime;

        switch (eState)
        {
            case BeamState.BS_START:

                UpdateStartState();

                break;

            case BeamState.BS_EXIST:

                UpdateExistState();
                UpdateRayTest();

                break;

            case BeamState.BS_OVER:

                UpdateOverState();

                break;
        }

        objParticlePerfab.transform.position = transform.position + fLaserParticleOffset * vecDirection;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown("j"))
            OnLaunchBeam();
        UpdateBeam();
    }
}