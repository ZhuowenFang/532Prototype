using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openingSpeed = 2f; // 门上升的速度
    public float maxHeight = 7f; // 门可以上升的最大高度
    private bool isOpening = false;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; // 记录门的初始位置
    }

    void Update()
    {
        if (isOpening && transform.position.y < initialPosition.y + maxHeight)
        {
            // 门缓慢上升
            transform.position += Vector3.up * openingSpeed * Time.deltaTime;
        }
    }

    public void StartOpening()
    {
        isOpening = true;
    }

    public void StopOpening()
    {
        isOpening = false;
    }
}
