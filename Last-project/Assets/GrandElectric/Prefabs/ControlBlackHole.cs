using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ControlBlackHole : MonoBehaviour
{

    public enum BlackHoleState
    {
        BHS_WAIT,
        BHS_START,
        BHS_EXIST,
        BHS_OVER
    };

    public GameObject objParent;
    public GameObject objBlackHole;
    public GameObject objHeatDistortion;
    public GameObject objSwiril;
    public GameObject objOrb;

    public BlackHoleState eState;
    private float fLaunchTime;
    public float fDurationTime;
    private float fOverLifeTime;
    private float fCurrentTime;

    private Renderer rendBlackHole;
    private Renderer rendHeatDistortion;
    private ParticleSystem psSwiril;
    private VisualEffect veOrb;

    public void Init()
    {

        objBlackHole.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        objHeatDistortion.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        objSwiril.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);

        rendBlackHole.material.SetFloat("_FresnelPower", 0.0f);
        rendHeatDistortion.material.SetFloat("_DistortionScale", 0.0f);
        rendHeatDistortion.material.SetFloat("_RotationAmount", -10.0f);

        eState = BlackHoleState.BHS_WAIT;
        fCurrentTime = 0.0f;
        fLaunchTime = 1.0f;
        fOverLifeTime = 2.0f;

        rendBlackHole.enabled = false;
        rendHeatDistortion.enabled = false;
        psSwiril.Stop();
        veOrb.Stop();

    }

    public void OnLaunch()
    {

        if (eState != BlackHoleState.BHS_WAIT)
            return;

        eState = BlackHoleState.BHS_START;
        rendBlackHole.enabled = true;
        rendHeatDistortion.enabled = true;
        psSwiril.Play();
        veOrb.Play();

    }

    private void UpdateStartState()
    {

        float tstart = Mathf.Lerp(0.0f, fLaunchTime, fCurrentTime / fLaunchTime);
        float t = tstart / fLaunchTime;

        objBlackHole.transform.localScale = new Vector3(t, t, t);
        objHeatDistortion.transform.localScale = new Vector3(t, t, t);
        objSwiril.transform.localScale = new Vector3(t, t, t);

        float tvalue = Mathf.Lerp(0.0f, 22.0f, t);
        rendHeatDistortion.material.SetFloat("_DistortionScale", tvalue);
        tvalue = Mathf.Lerp(0.0f, -0.59f, t);
        rendHeatDistortion.material.SetFloat("_RotationAmount", tvalue);

        if (Mathf.Abs(tstart - fLaunchTime) <= 0.01f)
        {
            objBlackHole.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            objHeatDistortion.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            objSwiril.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            rendHeatDistortion.material.SetFloat("_DistortionScale", 22.0f);
            rendHeatDistortion.material.SetFloat("_RotationAmount", -0.59f);

            eState = BlackHoleState.BHS_EXIST;
            fCurrentTime = 0.0f;
        }

    }

    private void UpdateExistState()
    {

        float texist = Mathf.Lerp(0.0f, fDurationTime, fCurrentTime / fDurationTime);
        float t = texist / fDurationTime;

        float tvalue = Mathf.Lerp(0.0f, 4.0f, t);
        rendBlackHole.material.SetFloat("_FresnelPower", tvalue);

        if (Mathf.Abs(texist - fDurationTime) <= 0.01f)
        {
            rendBlackHole.material.SetFloat("_FresnelPower", 4.0f);
            eState = BlackHoleState.BHS_OVER;
            fCurrentTime = 0.0f;
            psSwiril.Stop();
            veOrb.Stop();
        }

    }

    private void UpdateOverState()
    {

        float tover = Mathf.Lerp(0.0f, fOverLifeTime, fCurrentTime / fOverLifeTime);
        float t = tover / fOverLifeTime;

        objBlackHole.transform.localScale = new Vector3(1.0f - t, 1.0f - t, 1.0f - t);
        objSwiril.transform.localScale = new Vector3(1.0f - t, 1.0f - t, 1.0f - t);

        float tvalue = Mathf.Lerp(4.0f, 16.0f, t);
        rendBlackHole.material.SetFloat("_FresnelPower", tvalue);
        tvalue = Mathf.Lerp(22.0f, 0.0f, t);
        rendHeatDistortion.material.SetFloat("_DistortionScale", tvalue);
        tvalue = Mathf.Lerp(-0.59f, 0.0f, t);
        rendHeatDistortion.material.SetFloat("_RotationAmount", tvalue);

        if (Mathf.Abs(tover - fOverLifeTime) <= 0.01)
        {
            Init();
        }

    }

    private void UpdateManager()
    {

        if (eState != BlackHoleState.BHS_WAIT)
            fCurrentTime += Time.deltaTime;

        switch (eState)
        {
            case BlackHoleState.BHS_START:

                UpdateStartState();

                break;

            case BlackHoleState.BHS_EXIST:

                UpdateExistState();

                break;

            case BlackHoleState.BHS_OVER:

                UpdateOverState();

                break;
        }


    }

    void Start()
    {

        var obj0 = GameObject.Instantiate(objBlackHole);
        objBlackHole = obj0;

        var obj1 = GameObject.Instantiate(objHeatDistortion);
        objHeatDistortion = obj1;

        var obj2 = GameObject.Instantiate(objSwiril);
        objSwiril = obj2;

        var obj3 = GameObject.Instantiate(objOrb);
        objOrb = obj3;


        objBlackHole.transform.SetParent(objParent.transform);
        objHeatDistortion.transform.SetParent(objParent.transform);
        objSwiril.transform.SetParent(objParent.transform);
        objOrb.transform.SetParent(objParent.transform);

        rendBlackHole = objBlackHole.GetComponent<Renderer>();
        rendHeatDistortion = objHeatDistortion.GetComponent<Renderer>();
        psSwiril = objSwiril.GetComponent<ParticleSystem>();
        veOrb = objOrb.GetComponent<VisualEffect>();

        Init();

    }

    void Update()
    {

        if (Input.GetKeyDown("k"))
            OnLaunch();

        UpdateManager();

    }
}
