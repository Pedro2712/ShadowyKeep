using UnityEngine;

public class DestroyAfterAnimation : MonoBehaviour
{
    // Essa função será chamada pela animação

    public GameObject levelUp;
    public void DestroyObject()
    {
        // Destrua o objeto atual
        Destroy(levelUp);
    }
}