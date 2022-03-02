using JHiga.RTSEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LoadSceneAction", menuName = "RTS/Behaviour/Actions/LoadSceneAction")]
public class LoadSceneAction : BaseAction
{
    [SerializeField] private string sceneName;
    public override void Run(IExtendableEntity entity)
    {
        SceneManager.LoadScene(sceneName);
    }

    public override void Stop(IExtendableEntity entity)
    {
    }
}
