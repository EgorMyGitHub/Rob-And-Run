using UnityEngine;

namespace UI.GUI
{
    public class ScreenLayer : MonoBehaviour
    {
        [field: SerializeField]
        public GuiLayer layer{ get; private set; }
    }
}