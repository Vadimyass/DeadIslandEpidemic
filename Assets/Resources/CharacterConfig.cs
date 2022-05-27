using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Installers/CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private List<string> _heroProperties;
    public List<string> HeroProperties => _heroProperties;
}