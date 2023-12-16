using System.Collections.Generic;
using AsepStudios.TableChump.ScriptableObjects;
using UnityEngine;

namespace AsepStudios.TableChump.Utils
{
    public class ResourceProvider : MonoBehaviour
    {
        private static ResourceProvider Instance { get; set; }
        public static List<Sprite> Avatars => Instance.avatarSo.avatars;
        public static int GetIndexFromSprite(Sprite sprite) => Instance.avatarSo.GetIndexFromAvatar(sprite);
        public static Sprite GetAvatarFromIndex(int index) => Instance.avatarSo.GetAvatarFromIndex(index);


        [SerializeField] private AvatarSo avatarSo;

        public void Initialize()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
