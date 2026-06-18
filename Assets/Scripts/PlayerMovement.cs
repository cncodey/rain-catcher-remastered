using System;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public Transform turtle;
    public Transform turtle2;
    public CharacterController characterController;
    public GameManager manager;
    public TextMeshPro rainCounter;
    public GameObject tankWater;
    public Material iceMaterial;
    public Material waterMaterial;
    public string material = "water";
    private int rainCount;
    private float iceTimer;

    void FixedUpdate()
    {
        if (manager != null)
        {
            float moveX = Input.GetAxis("Horizontal"); 
            Vector3 movement = moveSpeed * Time.deltaTime * new Vector3(moveX, 0f, 0f);
            if (!manager.isGameOver)
            {
                animator.speed = (moveX < 0) ? (moveX * -1) : moveX;
                characterController.Move(movement);
            }

            if (moveX > 0f && turtle.rotation.y == 0f)
            {
                turtle.rotation = Quaternion.Euler(0f, 180f, 0f);
                turtle2.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (moveX < 0f && turtle.rotation.y != 0f)
            {
                turtle.rotation = Quaternion.Euler(0f, 0f, 0f);
                turtle2.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

            if (transform.position.x > 10f)
            {
                transform.position -= new Vector3(20f, 0f, 0f);
                GameObject[] drops = GameObject.FindGameObjectsWithTag("Drop");
                foreach (GameObject d in drops)
                {
                    d.transform.position -= new Vector3(20f, 0f, 0f);
                }
            }
            else if (transform.position.x < -10f)
            {
                transform.position += new Vector3(20f, 0f, 0f);
                GameObject[] drops = GameObject.FindGameObjectsWithTag("Drop");
                foreach (GameObject d in drops)
                {
                    d.transform.position += new Vector3(20f, 0f, 0f);
                }
            }

            //Debug.Log($"Y-rot 0? {(turtle.rotation.y == 0 ? "Yes." : "No.")}MoveX? {moveX}.");
        }
        else
        {
            animator.speed = 0f;
        }
    }

    void Start()
    {
        SkinCheck();
    }

    public void SkinCheck()
    {
        if (PlayerPrefs.GetInt("Skin") == 0)
        {
            turtle.gameObject.SetActive(true);
            turtle2.gameObject.SetActive(false);
        }
        else
        {
            turtle.gameObject.SetActive(false);
            turtle2.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Drop"))
        {
            string dropType = hit.gameObject.GetComponent<Drop>().dropType;
            switch (dropType)
            {
                case "water":
                    rainCount++;
                    if (rainCount == 11) { rainCount = 0; manager.AddScore(1); }
                    float newYpos = 0.315f * (rainCount/10f);
                    tankWater.transform.localPosition = new Vector3(tankWater.transform.localPosition.x, newYpos + 0.185f, tankWater.transform.localPosition.z);
                    rainCounter.text = rainCount.ToString();
                    break;
                case "snowflake":
                    tankWater.GetComponent<Renderer>().material = iceMaterial;
                    iceTimer = 5.0f;
                    //Debug.Log("iced");
                    break;
                case "lnbolt":
                    if (iceTimer <= 0f)
                    {
                        rainCount = 0;
                        float newYpos1 = 0.315f * (rainCount/10f);
                        tankWater.transform.localPosition = new Vector3(tankWater.transform.localPosition.x, newYpos1 + 0.185f, tankWater.transform.localPosition.z);
                        rainCounter.text = rainCount.ToString();
                        //Debug.Log("shocked");
                    }
                    //Debug.Log("got ln");
                    break;
            }
            Destroy(hit.gameObject);
        }
    }

    void Update()
    {
        if (iceTimer > 0)
        {
            iceTimer -= Time.deltaTime;
        }
        else if (iceTimer < 0)
        {
            iceTimer = 0;
            tankWater.GetComponent<Renderer>().material = waterMaterial;
        }
    }
}
