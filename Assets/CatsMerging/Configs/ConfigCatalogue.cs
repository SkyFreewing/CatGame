using System.Collections.Generic;
using UnityEngine;

namespace CatMerge 
{
    [CreateAssetMenu(fileName = "ConfigCatalogue", menuName = "ScriptableObjects/ConfigCatalogue", order = 1)]
    public class ConfigCatalogue : ScriptableObject, IConfigCatalogue
    {
        [SerializeField] GameConfig _gameConfig;
        [SerializeField] InputConfig _inputConfig;
        [SerializeField] AnimConfig _animConfig;
        public IGameConfig GameConfig => _gameConfig;
        public IInputConfig InputConfig => _inputConfig;
        public IAnimConfig AnimConfig => _animConfig;
    }
}
