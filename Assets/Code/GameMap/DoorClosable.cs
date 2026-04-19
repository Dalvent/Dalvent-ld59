using UnityEngine;

namespace Code.GameMap
{
    public class DoorClosable : MonoBehaviour
    {
        public bool IsClosed
        {
            get => gameObject.activeSelf;
            set
            {
                if (IsClosed == value)
                    return;

                gameObject.SetActive(value);
            }
        }
    }
}