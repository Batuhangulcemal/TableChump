using System.Collections.Generic;
using UnityEngine;

namespace BoardsStake.UI
{
    internal class ViewRefHolder : MonoBehaviour
    {
        public List<View> ViewList => viewRefHolderSO.ViewList;
        public View DefaultView => viewRefHolderSO.DefaultView;

        [SerializeField] private ViewRefHolderSO viewRefHolderSO;

        [SerializeField] public Transform ViewTransform;

    }

}
