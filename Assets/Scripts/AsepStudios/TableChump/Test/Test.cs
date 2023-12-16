using AsepStudios.TableChump.Mechanics.GameCore.Helper;
using UnityEngine;

namespace AsepStudios.TableChump.Test
{
    public class Test : MonoBehaviour
    {

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.T))
            {
                CardDealer cardDealer = new CardDealer();
            }
        
        }
    }
}
