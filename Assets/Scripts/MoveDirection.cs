using UnityEngine;

public class MoveDirection : MonoBehaviour
{
    [SerializeField] private float _forwardBound;
    [SerializeField] private float _backBound;
    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;
    [SerializeField] private float _speed;
    [SerializeField] private string _direction;

    private void Update()
    {
        if (_direction == "forward")
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.forward);

            if (transform.position.z >= _forwardBound)
            {
                Destroy(this.gameObject);
            }
        }
        else if (_direction == "back")
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.back);

            if (transform.position.z <= _backBound)
            {
                Destroy(this.gameObject);
            }
        }
        else if (_direction == "left")
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.left);

            if (transform.position.z >= _leftBound && this.gameObject.tag != "Helper")
            {
                Destroy(this.gameObject);
            }

            if (transform.position.z <= _leftBound && this.gameObject.tag == "Helper")
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.right);

            if (transform.position.z <= _rightBound)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
