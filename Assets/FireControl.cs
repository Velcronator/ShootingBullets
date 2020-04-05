using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class FireControl : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    void Update()
    {
        if (!isLocalPlayer) return;// Bullets only on client scene

        if (Input.GetKeyDown("space"))
        {
            CmdShoot();
        }
    }

    void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward * 50;
        Destroy(bullet, 3.0f);
    }

    [ClientRpc]// All ClientRPC classes need to start with Rpc.....
    void RpcCreateBullet()
    {
        if (!isServer)
            CreateBullet();
    }

    [Command]
    void CmdShoot()
    {
        CreateBullet();
        RpcCreateBullet();
    }
}
