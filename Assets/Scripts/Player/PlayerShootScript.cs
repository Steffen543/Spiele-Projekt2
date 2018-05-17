using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    private float shootTimer = 0f;

    public GameObject Weapon;

    public GameObject Bullet;

    public float Speed = 2000;

    public float ShootRate = 0.3f;

    public AudioSource ShootSound;

  

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shoot(Vector3 rotationTo) {
        shootTimer += Time.deltaTime;
        if (shootTimer >= ShootRate)
        {
            var rotation = Weapon.transform.rotation;
            rotation.y = 0;
            var spawnPosition = Weapon.transform.position;
            //spawnPosition.x += 0.3f;
            spawnPosition.y -= 0.4f;
            spawnPosition.z += 0.1f;
            GameObject g = (GameObject) Instantiate(Bullet, spawnPosition, rotation);
            g.transform.LookAt(rotationTo);
            var destination = g.transform.forward;
            g.GetComponent<Rigidbody>().AddForce(destination * Speed);
            shootTimer = 0f;
            Destroy(g, 10f);
        }
    }
}
