using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Features.SceneTransitions
{
    public abstract class SceneTransition : MonoBehaviour, ISceneTransition
    {
        public abstract UniTask PlayOnAsync();

        public abstract UniTask PlayOffAsync();
    }
}