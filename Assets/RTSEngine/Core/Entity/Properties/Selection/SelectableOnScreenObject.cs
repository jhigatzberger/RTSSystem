
using UnityEngine;

namespace RTSEngine.Core.Selection
{
    public class SelectableOnScreenObject : MonoBehaviour
    {
        private SelectableExtension main;
        public void Init(SelectableExtension main)
        {
            this.main = main;
        }
        private void OnBecameVisible()
        {
            print("visible");
            SelectionContext.onScreen.Add(main);
        }
        private void OnBecameInvisible()
        {
            SelectionContext.onScreen.Remove(main);
        }
    }

}
