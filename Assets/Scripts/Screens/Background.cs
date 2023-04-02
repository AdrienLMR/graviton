using UnityEngine;

[DisallowMultipleComponent]
public class Background : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.parent);
    }
}
