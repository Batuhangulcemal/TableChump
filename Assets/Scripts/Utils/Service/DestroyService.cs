using UnityEngine;

namespace AsepStudios.Utils
{
    public class DestroyService
    {
        public static void ClearChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
