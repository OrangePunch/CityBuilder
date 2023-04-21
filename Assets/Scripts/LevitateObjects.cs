using UnityEngine;

public class LevitateObjects : MonoBehaviour
{
    [SerializeField] private float _frequncy = 1f;
    [SerializeField] private float _amplitude = 1f;
    [SerializeField] private bool _randomize;

    private float _originalY;
    private Rigidbody _rigidbody;
    private float _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _originalY = _rigidbody.position.y;

        if (_randomize)
        {
            _speed = Random.value * Mathf.PI * 2;
        }
    }

    private void Update()
    {
        var pos = _rigidbody.position;
        pos.y = _originalY + Mathf.Sin(_speed + Time.time * _frequncy) * _amplitude;
        _rigidbody.MovePosition(pos);
    }
}
