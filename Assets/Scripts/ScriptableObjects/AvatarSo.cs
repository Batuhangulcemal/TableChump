using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu()]
    public class AvatarSo : ScriptableObject
    {
        public List<Sprite> avatars;

        public Sprite GetAvatarFromIndex(int index)
        {
            return index == -1 ? avatars[0] : avatars[index];
        }

        public int GetIndexFromAvatar(Sprite sprite)
        {
            return avatars.IndexOf(sprite);
        }
    }
}