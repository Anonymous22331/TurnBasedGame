using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Unit Player;
    public Unit Enemy;
    private IGameState _currentState;
    private Ability _selectedAbility;
    private PlayerState _playersUIState;


    void Start()
    {
        Player = new Unit("Player", 20, new List<Ability> { new Attack(), new Barrier(), new Heal(), new Fireball(), new Purify() });
        Enemy = new Unit("Enemy", 20, new List<Ability> { new Attack(), new Barrier(), new Heal(), new Fireball(), new Purify() });

        _playersUIState = FindFirstObjectByType<PlayerState>();

        ChangeState(new PlayerTurnState(this));
    }

    
    public void PlayerSelectAbility(int abilityIndex)
    {
        _selectedAbility = Player.Abilities[abilityIndex];
        if (_currentState is PlayerTurnState)
        {
            ((PlayerTurnState)_currentState).SetSelectedAbility(_selectedAbility);
        }
    }

    public void MakeMove()
    {
        if (_selectedAbility != null)
        {
            ((PlayerTurnState)_currentState).Execute();
            _selectedAbility = null;
        }
    }

    public void ChangeState(IGameState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _playersUIState.UpdatePlayersState(Player, Enemy);
        _currentState.Enter();
    }

    public void ExecuteAbility(Unit user, Unit target, Ability ability)
    {
        if (ability.IsReady())
        {
            ability.Use(user, target);
        }

        ApplyEffects(user);

        CheckGameOver();
    }

    private void ApplyEffects(Unit unit)
    {
        if (unit.BurningDuration > 0)
        {
            unit.TakeDamage(1);
            unit.BurningDuration--;
        }

        if (unit.HealDuration > 0)
        {
            unit.Heal(2);
            unit.HealDuration--;
        }

        foreach (var ability in unit.Abilities)
        {
            ability.DecreaseCooldown();
        }
    }

    private void CheckGameOver()
    {
        if (Player.Health <= 0 || Enemy.Health <= 0)
        {
            ChangeState(new GameOverState(this));
        }
    }

    public void RestartGame()
    {
        Player = new Unit("Player", 20, new List<Ability> { new Attack(), new Barrier(), new Heal(), new Fireball(), new Purify() });
        Enemy = new Unit("Enemy", 20, new List<Ability> { new Attack(), new Barrier(), new Heal(), new Fireball(), new Purify() });
        _playersUIState.UpdatePlayersState(Player, Enemy);
        ChangeState(new PlayerTurnState(this));
    }
}