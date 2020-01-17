using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : TurretBehaviour
{
    protected override void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, emitter.transform).gameObject;
        CannonBallBehaviour bullet = bulletGO.GetComponent<CannonBallBehaviour>();

        if (bullet != null)
            bullet.Seek(target);
    }
}
