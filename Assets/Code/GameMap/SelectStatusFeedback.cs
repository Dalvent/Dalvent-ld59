using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.GameMap
{
    public class SelectStatusFeedback : MonoBehaviour
    {
        public GameMapSelectable GameMapSelectable;
        public Image SelectedImage;

        private void OnEnable()
        {
            GameMapSelectable.Selected += OnSelected;
            GameMapSelectable.Unselected += OnUnselected;
            
            SelectedImage.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            GameMapSelectable.Selected -= OnSelected;
            GameMapSelectable.Unselected -= OnUnselected;
        }

        private void OnSelected()
        {
            SelectedImage.gameObject.SetActive(true);
        }

        private void OnUnselected()
        {
            SelectedImage.gameObject.SetActive(false);
        }
    }
}