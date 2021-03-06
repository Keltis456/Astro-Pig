﻿using UnityEngine;

public class PlayerController : MonoBehaviour {

    public KeyCode moveUp;
    public KeyCode moveDown;

    public float speed = 150f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            GameManager.instance.TakeDamage(1);
        }
        if (collision.gameObject.tag == "Fuel")
        {
            GameManager.instance.AddFuel(15);
        }
        if (collision.gameObject.tag == "Oxygen")
        {
            GameManager.instance.AddOxygen(30);
        }
        if (collision.gameObject.tag == "LifeBonus")
        {
            GameManager.instance.AddHP(1);
        }
        if (collision.gameObject.tag == "speedBonus")
        {
            GameManager.instance.DecreaseGameSpeed(2);
        }
    }

    void Update()
    {
        if (Input.GetKey(moveUp))
        {
            if (GameManager.instance.CheckFuel())
            {
                if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f)).y - 17f)
                    transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime * GameManager.instance.gameSpeed);
                GameManager.instance.ActivateTrusters();
            }
        }

        if (Input.GetKey(moveDown))
        {
            if (GameManager.instance.CheckFuel())
            {
                if (transform.position.y > Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).y + 17f)
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime * GameManager.instance.gameSpeed);
                GameManager.instance.ActivateTrusters();
            }
        }
    }
}
