using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void OnIdle()
    {
        EventBus.Instance.Publish(GlobalEvent.AllIdle);
    }
    public void OnDead()
    {
        EventBus.Instance.Publish(GlobalEvent.AllDead);
    }
    public void OnAttack()
    {
        EventBus.Instance.Publish(GlobalEvent.AllAttack);
    }
    public void OnRun()
    {
        EventBus.Instance.Publish(GlobalEvent.AllRun);
    }
    public void OnSpin()
    {
        EventBus.Instance.Publish(GlobalEvent.AllSpin);
    }
    public void OnSpawn()
    {
        SpawnManager.Instance.Spawn();
    }

    public void OnNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
