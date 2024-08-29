using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController door; // 引用门的脚本
    public float requiredScale = 1.5f; // 需要的玩家最小缩放比例
    private bool isPlayerOnButton = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 检查玩家是否变大
            if (other.transform.localScale.x >= requiredScale)
            {
                isPlayerOnButton = true;
                door.StartOpening();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnButton = false;
            door.StopOpening();
        }
    }
}
