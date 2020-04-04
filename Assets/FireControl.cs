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

    [Command]
    void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
        //bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * 2000);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward*50;
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 3.0f);
    }
}
