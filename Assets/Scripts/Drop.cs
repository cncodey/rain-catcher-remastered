using UnityEngine;

public class Drop : MonoBehaviour
{
    public string dropType;
    public float dropSpeed;
    public GameManager manager;

    private void FixedUpdate()
    {
        if (!manager.isGamePaused)
        {
            transform.position += Vector3.down * dropSpeed * Time.deltaTime;
            transform.Rotate(0f, Time.deltaTime * 180f, 0f);
        }
        if (transform.position.y < 0 || Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 10)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
