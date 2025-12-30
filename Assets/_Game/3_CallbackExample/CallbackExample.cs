using System.Collections;
using UnityEngine;

public class CallbackExample : MonoBehaviour
{
    [SerializeField] private ScaleAnimation _animation_1;
    [SerializeField] private ScaleAnimation _animation_2;
    [SerializeField] private ScaleAnimation _animation_3;

    [SerializeField] private ParticleSystem _animationEffectPrefab;

    private int _wallet;

    private void Awake()
    {
        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        _animation_1.Play();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F));

        _animation_2.Play(() =>
        {
            _wallet += 100;

            Debug.Log($"Wallet: {_wallet}");
        });

        yield return new WaitWhile(() => _wallet < 50);

        _animation_3.Play(() =>
        {
            if (_animationEffectPrefab != null)
                Instantiate(_animationEffectPrefab, _animation_3.transform.position, Quaternion.identity, null);
        });
    }
}
