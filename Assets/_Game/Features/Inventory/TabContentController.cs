using UnityEngine;
using UnityEngine.Events;
using UnityWeld.Binding;

namespace _Game.Features.Inventory
{
    public class TabContentBindingInputEvent : UnityEvent<int>
    {
    }

    [Binding]
    public class TabContentController : MonoBehaviour
    {
        [Binding]
        public int ActiveTabIndex
        {
            get => _activeTabIndex;
            set
            {
                _activeTabIndex = value;
                UpdateView();
            }
        }

        private int _activeTabIndex = 0;

        public Transform[] contents;

        private void UpdateView()
        {
            for (var index = 0; index < contents.Length; index++)
            {
                try
                {
                    contents[index]?.gameObject.SetActive(index == ActiveTabIndex);
                }
                catch (UnassignedReferenceException) { }
            }
        }
    }
}