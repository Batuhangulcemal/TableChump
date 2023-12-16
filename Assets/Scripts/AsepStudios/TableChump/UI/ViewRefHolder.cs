using System.Collections.Generic;
using AsepStudios.TableChump.ScriptableObjects;
using UnityEngine;

namespace AsepStudios.TableChump.UI
{
    internal class ViewRefHolder : MonoBehaviour
    {
        public List<View> ViewList => viewRefHolderSo.viewList;
        public View DefaultView => viewRefHolderSo.defaultView;

        [SerializeField] private ViewRefHolderSo viewRefHolderSo;

        [SerializeField] public Transform viewTransform;

    }

}
