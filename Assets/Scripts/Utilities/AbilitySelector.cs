using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelector : MonoBehaviour
{
    [SerializeField] private Button attackButton;
    [SerializeField] private Button barrierButton;
    [SerializeField] private Button healButton;
    [SerializeField] private Button fireballButton;
    [SerializeField] private Button purifyButton;
    [SerializeField] private GameManager _gameManager;

    private Button _selectedButton;
    private List<Text> _buttonTexts;


    private void Start()
    {
        attackButton.onClick.AddListener(() => OnAbilitySelected(0, attackButton));
        barrierButton.onClick.AddListener(() => OnAbilitySelected(1, barrierButton));
        healButton.onClick.AddListener(() => OnAbilitySelected(2, healButton));
        fireballButton.onClick.AddListener(() => OnAbilitySelected(3, fireballButton));
        purifyButton.onClick.AddListener(() => OnAbilitySelected(4, purifyButton));

        _buttonTexts = new List<Text>
        {
            attackButton.GetComponentInChildren<Text>(),
            barrierButton.GetComponentInChildren<Text>(),
            healButton.GetComponentInChildren<Text>(),
            fireballButton.GetComponentInChildren<Text>(),
            purifyButton.GetComponentInChildren<Text>()
        };

    }

    private void OnAbilitySelected(int abilityIndex, Button selectedButton)
    {
        Ability selectedAbility = _gameManager.Player.Abilities[abilityIndex];
        if (selectedAbility.IsReady())
        {
            _gameManager.PlayerSelectAbility(abilityIndex);
            ActiveSelectedButton(selectedButton);
        } 
    }

    private void ActiveSelectedButton(Button selectedButton)
    {
        ResetSelectedButtonColor();
        selectedButton.GetComponent<Image>().color = Color.green;
        _selectedButton = selectedButton; 
    }

    public void ResetSelectedButtonColor()
    {
        if (_selectedButton != null)
        {
           _selectedButton.GetComponent<Image>().color = Color.white; 
        }
    }

    public void UpdateAbilityButtonTexts()
    {
        for (int i = 0; i < _buttonTexts.Count; i++)
        {
            Ability ability = _gameManager.Player.Abilities[i];
            if (ability.IsReady())
            {
                _buttonTexts[i].text = ability.Name;  
            }
            else
            {
                _buttonTexts[i].text = $"Cooldown: {ability.CooldownTimer}";
            }
        }
    }

}