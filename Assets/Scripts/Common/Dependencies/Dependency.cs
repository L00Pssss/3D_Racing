using UnityEngine;

public abstract class Dependency : MonoBehaviour
{
    protected virtual void BindAll(MonoBehaviour monoBehaviourInScene)
    {

    } 
    protected void FindAllObjectToBind()
    {
        MonoBehaviour[] allmonoBehavioursInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < allmonoBehavioursInScene.Length; i++)
        {
            BindAll(allmonoBehavioursInScene[i]);
        }
    }

    protected void Bind<T>(MonoBehaviour bindObject, MonoBehaviour target) where T : class
    {
        if (target is IDependency<T>) (target as IDependency<T>).Construct(bindObject as T);
    }
 

}
