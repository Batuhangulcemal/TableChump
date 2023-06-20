using UnityEngine;

namespace AsepStudios.Mechanic.PlayerCore.LocalPlayerCore
{
    public class LocalPlayer : MonoBehaviour
    {
        public static LocalPlayer Instance { get; private set; }
        public Player Player { get; private set; }
        public bool IsPlayerAttached => Player != null;

        private void Awake()
        {
            Instance = this;
        }

        public void AttachPlayer(Player player)
        {
            Player = player;
        }

        public void TestChangeName(string asd)
        {
            Player.SetUserName(asd);
        }
    }
}
