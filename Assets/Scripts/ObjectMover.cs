using UnityEngine;

public class ObjectMover : MonoBehaviour {

    public float speed = 100;

    Animation anim;
    
	void Start () {
        Destroy(gameObject, 10 / GameManager.instance.gameSpeed);
        anim = GetComponent<Animation>();
        Debug.Log(name + " anim succesfull");
	}

    private void Update()
    {
        transform.position = new Vector3(transform.position.x - Time.deltaTime * speed * GameManager.instance.gameSpeed, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
