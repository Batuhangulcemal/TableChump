using System.Collections.Generic;
using AsepStudios.TableChump.UI;
using UnityEngine;

namespace AsepStudios.TableChump.ScriptableObjects
{
    [CreateAssetMenu()]
    public class ViewRefHolderSo : ScriptableObject
    {
        public List<View> viewList;
        public View defaultView;
    }
}
