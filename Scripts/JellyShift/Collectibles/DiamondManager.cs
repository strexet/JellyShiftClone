using System.Collections.Generic;
using UnityEngine;

namespace JellyShift.Collectibles
{
    public class DiamondManager : MonoBehaviour
    {
        [SerializeField] private List<Diamond> _diamonds;

        private int _diamondCount;

        private void OnEnable()
        {
            foreach (var diamond in _diamonds)
            {
                diamond.Collected += OnDiamondCollection;
            }
        }

        private void OnDisable()
        {
            foreach (var diamond in _diamonds)
            {
                diamond.Collected -= OnDiamondCollection;
            }
        }

        private void OnDiamondCollection()
        {
            _diamondCount++;
        }

        private void OnValidate()
        {
            var foundDiamonds = FindObjectsOfType<Diamond>();
            _diamonds = new List<Diamond>();
            _diamonds.AddRange(foundDiamonds);
        }
    }
}