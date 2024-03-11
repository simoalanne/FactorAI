
using UnityEngine;
using Global;

namespace Factory
{
    public class MiniGameManager : MonoBehaviour
    {
        void Start()
        {
            UnlockMiniGame(GameManager.Instance.MiniGameName);
        }

        private void UnlockMiniGame(string miniGameName)
        {
            // TODO: Enable the play button for the respective minigame.
        }
    }
}


