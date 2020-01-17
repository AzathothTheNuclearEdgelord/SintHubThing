using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    private Transform target;

    public void Seek(Transform _tartget)
    {
        target = _tartget;
    }
    void Update()
    {
        if (target == null)
        {
            //Destroy(GameObject);
            return;
        }
    }
}
