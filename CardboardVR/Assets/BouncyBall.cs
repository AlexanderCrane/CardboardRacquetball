using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BouncyBall : MonoBehaviour {

    public float speed;
    public GameObject room;
    public GameObject canvas;
    public Text scoreText;

    private Rigidbody RB;
    private Vector3 velocity;
    private int bounceCount = 0;

	// Use this for initialization
	void Start () {
        canvas.SetActive(false);
        RB = GetComponent<Rigidbody>();
        float offset = Random.Range(-10.0f, 10.0f);
        velocity = transform.forward * speed;
        velocity.x += offset;
	}
	
	// Update is called once per frame
	void Update () {
        velocity = velocity.normalized * speed;
        RB.velocity = velocity;
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            return;
        }
        else if(collision.gameObject.tag == "GameOver")
        {
            Time.timeScale = 0;
            canvas.SetActive(true);
        }

        if (collision.gameObject.tag == "Paddle")
        {
            bounceCount++;
            scoreText.text = bounceCount.ToString();
            if(bounceCount % 5 == 0)
            {
                Vector3 newRot = room.transform.eulerAngles + new Vector3(0, 90, 0);
                StartCoroutine(Rotate(newRot));
            }
        }
        foreach (ContactPoint contact in collision.contacts)
        {
            velocity = Vector3.Reflect(velocity, contact.normal);
            return;
        }
    }

    IEnumerator Rotate(Vector3 rotation)
    {
        while(room.transform.eulerAngles.y <= rotation.y)
        {
            room.transform.eulerAngles = Vector3.Lerp(room.transform.eulerAngles, rotation, 2 * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }
}
