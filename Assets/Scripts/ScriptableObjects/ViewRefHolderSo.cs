using AsepStudios.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu()]
public class ViewRefHolderSo : ScriptableObject
{
    public List<View> viewList;
    public View defaultView;
}
