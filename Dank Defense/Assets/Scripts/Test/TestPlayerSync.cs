using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

[NetworkSettings(channel = 0, sendInterval = 0.1f)]
public class TestPlayerSync : NetworkBehaviour
{
    [SyncVar(hook = "SyncPositionValues")]
    private Vector3 syncPos;

    [SerializeField]
    Transform myTransform;

    [SerializeField]
    float lerpRate = 15f;
    private float normalLerpRate = 16;
    private float fasterLerpRate = 27;

    private Vector3 lastPos;
    private float threshold = 0.5f;

    private List<Vector3> syncPosList = new List<Vector3>();
    [SerializeField]
    private bool useHistoricalLerping = false;
    private float closeEnough = 0.11f;

    void Start()
    {
        lerpRate = normalLerpRate;
    }

    void Update()
    {
        LerpPosition();
    }

    void FixedUpdate()
    {
        TransmitPosition();
    }

    [Command]
    void CmdPosToServer(Vector3 pos)
    {
        syncPos = pos;
    }

    [Client]
    void SyncPositionValues(Vector3 latestPos)
    {
        syncPos = latestPos;
        syncPosList.Add(syncPos);
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if(isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshold)
        {
            CmdPosToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }

    void LerpPosition()
    {
        if (!isLocalPlayer)
        {
            if (useHistoricalLerping)
            {
                HistoricalLerping();
            }
            else
            {
                OrdinaryLerping();
            }
        }
    }

    void OrdinaryLerping()
    {
        myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);
    }

    void HistoricalLerping()
    {
        if (syncPosList.Count > 0)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPosList[0], Time.deltaTime * lerpRate);

            if (Vector3.Distance(myTransform.position, syncPosList[0]) < closeEnough)
            {
                syncPosList.RemoveAt(0);
            }

            if (syncPosList.Count > 10)
            {
                lerpRate = fasterLerpRate;
            }
            else
            {
                lerpRate = normalLerpRate;
            }

           
        }
    }
}
