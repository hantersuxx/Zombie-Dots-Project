using UnityEngine;

public class SceneData<T> : MonoBehaviour where T : MonoBehaviour, IStorage
{
    public T Storage => FindObjectOfType<T>();

    public TreeNode<string> SceneNode { get; set; }
}
