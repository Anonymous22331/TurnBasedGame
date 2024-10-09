using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] Slider playerHealth;
    [SerializeField] Text playerHealthText;
    [SerializeField] Slider enemyHealth;
    [SerializeField] Text enemyHealthText;
    [Header("Player Status")]
    [SerializeField] GameObject playerFireStatus;
    [SerializeField] GameObject playerBarrierStatus;
    [SerializeField] GameObject playerHealStatus;
    [Header("Enemy Status")]
    [SerializeField] GameObject enemyFireStatus;
    [SerializeField] GameObject enemyBarrierStatus;
    [SerializeField] GameObject enemyHealStatus;

    public void UpdatePlayersState(Unit player, Unit enemy)
    {
        playerHealth.value = player.Health;
        playerHealthText.text = player.Health + "/20";
        enemyHealth.value = enemy.Health;
        enemyHealthText.text = enemy.Health + "/20";

        playerFireStatus.SetActive(IsBurning(player));
        playerBarrierStatus.SetActive(HasBarrier(player));
        playerHealStatus.SetActive(IsHealing(player));

        enemyFireStatus.SetActive(IsBurning(enemy));
        enemyBarrierStatus.SetActive(HasBarrier(enemy));
        enemyHealStatus.SetActive(IsHealing(enemy));
    }

    private bool IsBurning(Unit unit)
    {
        return unit.BurningDuration > 0;
    }

    private bool IsHealing(Unit unit)
    {
        return unit.HealDuration > 0;
    }
    private bool HasBarrier(Unit unit)
    {
        return unit.BarrierValue > 0;
    }
}
