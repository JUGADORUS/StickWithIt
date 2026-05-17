using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject _dustEffect;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Transform _positionWhereToSpawnVFX;
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private float cooldown = 0.2f; //1 эффект max в 0.2 сек
    private float lastEffectTime;
        
    private void OnEnable()
    {
        _groundChecker.OutOfGround += PlayLandingEffects;
        _groundChecker.OutOfGround += _audioManager.PlayJumpSound;
        //_groundChecker.Landing += _audioManager.PlayJumpSound;
        _groundChecker.Landing += PlayLandingEffects;
    }

    private void OnDisable() // ДОБАВЬ ЭТО!
    {
        _groundChecker.OutOfGround -= PlayLandingEffects; // Отписались!
        _groundChecker.OutOfGround -= _audioManager.PlayJumpSound;
        //_groundChecker.Landing -= PlayLandingEffects;
    }

    private void PlayLandingEffects()
    {
        if (Time.time - lastEffectTime < cooldown) return;
        GameObject dust = Instantiate(_dustEffect, _positionWhereToSpawnVFX.transform.position, transform.rotation);
        Destroy(dust, 3f);
        lastEffectTime = Time.time;
    }
}
