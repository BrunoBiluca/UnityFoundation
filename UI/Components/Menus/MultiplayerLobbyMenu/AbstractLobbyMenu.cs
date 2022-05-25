using UnityEngine;

namespace Assets.UnityFoundation.UI.Menus.MultiplayerLobbyMenu
{
    public abstract class AbstractLobbyMenu : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();
    }
}