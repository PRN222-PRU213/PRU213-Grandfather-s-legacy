using UnityEngine;

public class TestStory : MonoBehaviour
{
    void Start()
    {
        var ws = SaveManager.Instance.WorldState;

        // Giả lập player câu được cá dị dạng
        ws.Increment("aberration_caught");  // = 1
        StoryDirector.Instance.Evaluate();

        // Trigger "test_first_aberration" phải bắn
        // Flag "world_knows_aberration" phải = true
        Debug.Log("world_knows_aberration = "
            + ws.GetFlag("world_knows_aberration")); // true

        // Gọi Evaluate() lần 2 → trigger KHÔNG bắn lại
        ws.Increment("aberration_caught");  // = 2
        StoryDirector.Instance.Evaluate();
        Debug.Log("Trigger chỉ bắn 1 lần duy nhất ✓");
    }
}
