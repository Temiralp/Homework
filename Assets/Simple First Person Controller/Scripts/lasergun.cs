using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class lasergun : NetworkBehaviour
{
    public Transform laserTransform;
    public LineRenderer line;


    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer && Input.GetMouseButtonDown(0))
        {

            CmdShoot();

        }
    }
    [Command]
        public void CmdShoot()
        {
            Ray ray = new Ray(laserTransform.position, laserTransform.forward);
            if (Physics.Raycast(ray,out RaycastHit hit, 100f))
            {

                RpcDrawlaser(laserTransform.position, hit.point);
            }

            else
            {
                RpcDrawlaser(laserTransform.position, laserTransform.position + laserTransform.forward * 100f);
            }



        }

    [ClientRpc]
        void RpcDrawlaser(Vector3 start, Vector3 end)
        {

            StartCoroutine(LaserFlash(start, end));
        }

        IEnumerator LaserFlash(Vector3 start, Vector3 end) 
        
        {
            line.SetPosition(0, start);
            line.SetPosition(1, end);
            yield return new WaitForSeconds(0.3f);
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);


        }
        
    
}
