using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest
{

    public Vector3      vecStartPosition    = Vector3.zero;
    public Vector3      vecDirection        = Vector3.forward;
    public float        fLength             = 1.0f;

    public Vector3      vecCastPosition     = Vector3.zero;
    public float        fCastDistance       = 0.0f;
    public GameObject   objCastObject       = null;

    public bool         bOnHit              = false;

    public bool OnHitted()
    {
        if (fLength <= 0.0f)
        {
            vecCastPosition = vecStartPosition;
            fCastDistance = 0.0f;
            objCastObject = null;
            bOnHit = false;
            return bOnHit;
        }

        RaycastHit beHitted;
        bOnHit = Physics.Raycast(vecStartPosition, vecDirection, out beHitted);

        if(bOnHit)
        {
            fCastDistance = beHitted.distance;
            objCastObject = beHitted.collider.gameObject;
            if (fCastDistance <= fLength)
            {
                vecCastPosition = beHitted.point;
            }
            else
            {
                vecCastPosition = vecStartPosition + vecDirection * fLength;
                fCastDistance = fLength;
                bOnHit = false;
            }
        }
        else
        {
            vecCastPosition = vecStartPosition + vecDirection * fLength;
            fCastDistance = fLength;
            objCastObject = null;
        }

        return bOnHit;
    }
}