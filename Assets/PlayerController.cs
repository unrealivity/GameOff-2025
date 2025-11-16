using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDistance;

    private InputAction _mousePositionInput;
    private Vector2 _mouseScreenPosition;
    private Vector2 _mouseWorldPosition;
    private Sprite _playerSprite;
    private Vector2 _currentPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mousePositionInput = InputSystem.actions.FindAction("MouseMovement");
        _playerSprite = GetComponent<Sprite>();
    }

    // Update is called once per frame
    void Update()
    {   
        _mouseScreenPosition = _mousePositionInput.ReadValue<Vector2>();
        // Convert to world space (force z = camera near clip, then flatten to 2D)
        _mouseWorldPosition = _camera.ScreenToWorldPoint(
            new Vector3(_mouseScreenPosition.x, _mouseScreenPosition.y, _camera.nearClipPlane)
        );

        _currentPosition = transform.position;

        transform.position = Vector2.MoveTowards(
            _currentPosition,
            _mouseWorldPosition,
            _maxDistance
        );

    }
}
