
using System.Linq;
using UnityEngine;

public interface IGameState
{
    void Enter();
    void Execute();
    void Exit();
}

public class PlayerTurnState : IGameState
{
    private GameManager _gameManager;
    private Ability _selectedAbility;

    public PlayerTurnState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("It's player's turn. Choose an ability.");
    }
    
    public void SetSelectedAbility(Ability ability)
    {
        _selectedAbility = ability;
    }

    public void Execute()
    {
        _gameManager.ExecuteAbility(_gameManager.Player, _gameManager.Enemy, _selectedAbility);
        _gameManager.ChangeState(new EnemyTurnState(_gameManager));
    }

    public void Exit()
    {
        Debug.Log("Player's turn ended.");
    }
}

public class EnemyTurnState : IGameState
{
    private GameManager _gameManager;

    public EnemyTurnState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("It's enemy's turn.");
        Execute();
    }

    public void Execute()
    {
        var randomAbility = _gameManager.Enemy.Abilities
            .Where(a => a.IsReady())
            .OrderBy(_ => Random.value)
            .First();

        _gameManager.ExecuteAbility(_gameManager.Enemy, _gameManager.Player, randomAbility);
        _gameManager.ChangeState(new PlayerTurnState(_gameManager));
    }

    public void Exit()
    {
        Debug.Log("Enemy's turn ended.");
    }

} 

public class GameOverState : IGameState
{
    private GameManager _gameManager;

    public GameOverState(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Enter()
    {
        Debug.Log("Game over. Restarting...");
        _gameManager.RestartGame();
    }

    public void Execute() { }

    public void Exit() { }
}