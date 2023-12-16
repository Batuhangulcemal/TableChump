using UnityEngine;

namespace AsepStudios.TableChump.Utils.Service
{
    public static class DestroyService
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
