using UnityEngine;

namespace MiniGame1
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField]
		private float _speed = 1;
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
			Vector2 velocity = _rb2D.velocity;
			velocity.x = direction.x * _speed;
			_rb2D.velocity = velocity;
		}
	}
}