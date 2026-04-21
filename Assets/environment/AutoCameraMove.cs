using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoCameraMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

}