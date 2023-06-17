using AsepStudios.UI;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ViewRefHolderSO : ScriptableObject
{
    public List<View> ViewList;
    public View DefaultView;
}
