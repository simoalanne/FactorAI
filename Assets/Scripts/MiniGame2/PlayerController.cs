using UnityEngine;

namespace MiniGame2
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private float _speed = 2;
		private Rigidbody2D _rb2D;
		private InputReader _inputReader;
		private Vector2 _direction = Vector2.zero;

		private void Awake()
		{
			_rb2D = GetComponent<Rigidbody2D>();
			_inputReader = GetComponent<InputReader>();
        }

		private void Update()
		{
			_direction = _inputReader.Movement;
        }

		private void FixedUpdate()
		{
            Move(_direction);
        }

		private void Move(Vector2 direction)
		{
			_rb2D.velocity = new Vector2(direction.x * _speed, _rb2D.velocity.y);
		}
	}
}