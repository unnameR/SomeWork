using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour {

    public Transform partToRotate;
    public float rotateSpeed;
    public bool targetIsPlayer;

    [SerializeField]
    private Transform target;
    void Awake()
    {
        if (targetIsPlayer)
            PlayerSpawner.spawnEvent += SetTardet;
    }
    public void SetTardet(Transform _target)
    {
        target = _target;
    }
    void Update()
    {
        if (target == null)
            return;

        
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        partToRotate.rotation =Quaternion.Slerp(partToRotate.rotation, Quaternion.Euler(0, 0, 90 + angle), rotateSpeed*Time.deltaTime);
    }
    void OnDisable()
    {
        PlayerSpawner.spawnEvent -= SetTardet;
    }
}
