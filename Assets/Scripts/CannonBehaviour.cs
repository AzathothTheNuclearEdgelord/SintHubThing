using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : TurretBehaviour
{
    protected override void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, emitter).gameObject;
        CannonBallBehaviour cannonBallBehaviour = bulletGO.GetComponent<CannonBallBehaviour>();

        if (cannonBallBehaviour != null)
            cannonBallBehaviour.Seek(target);
    }
}
