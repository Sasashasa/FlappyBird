using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public static Bird Instance { get; private set; }
    
    public event EventHandler<OnGameOverEventArgs> OnGameOver;
    public class OnGameOverEventArgs : EventArgs
    {
        public int TotalScore;
    }
    
    public event EventHandler<OnScoreChangedEventArgs> OnScoreChanged;
    public class OnScoreChangedEventArgs : EventArgs
    {
        public int Score;
    }

    public event EventHandler OnCoinCollected;
    
    [SerializeField] private float _movementSpeed = 0.5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _maxRotation = 90f;
    [SerializeField] private float _minRotation = 0f;
    [SerializeField] private float _upRotationSpeed = 400f;
    [SerializeField] private float _downRotationSpeed = 600f;
    [SerializeField] private float _rotationCooldown = 0.1f;

    [SerializeField] private RuntimeAnimatorController _yellowBirdAnimatorController;
    [SerializeField] private RuntimeAnimatorController _blueBirdAnimatorController;
    [SerializeField] private RuntimeAnimatorController _redBirdAnimatorController;
    [SerializeField] private Sprite _yellowBirdSprite;
    [SerializeField] private Sprite _blueBirdSprite;
    [SerializeField] private Sprite _redBirdSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _curAngle;
    private float _timer;
    private bool _isGameOver;
    private int _score;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;

        switch (PlayerData.BirdType)
        {
            case BirdType.YellowBird:
                _animator.runtimeAnimatorController = _yellowBirdAnimatorController;
                _spriteRenderer.sprite = _yellowBirdSprite;
                break;
            case BirdType.BlueBird:
                _animator.runtimeAnimatorController = _blueBirdAnimatorController;
                _spriteRenderer.sprite = _blueBirdSprite;
                break;
            case BirdType.RedBird:
                _animator.runtimeAnimatorController = _redBirdAnimatorController;
                _spriteRenderer.sprite = _redBirdSprite;
                break;
        }
    }
    
    private void Update()
    {
        Move();
        Rotate();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Coin>())
        {
            PlayerPrefs.SetInt(SaveDataType.Coins, PlayerPrefs.GetInt(SaveDataType.Coins) + 1);
            OnCoinCollected?.Invoke(this, EventArgs.Empty);
            Destroy(col.gameObject);
        }
        else if (col.gameObject.GetComponent<Pipe>()|| col.gameObject.GetComponent<CameraMovement>())
        {
            _isGameOver = true;
            _rigidbody.gameObject.GetComponent<Collider2D>().enabled = false;
            OnGameOver?.Invoke(this, new OnGameOverEventArgs{TotalScore = _score});
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Pass>())
            return;
        
        if (_isGameOver)
            return;
        
        IncreaseScore();
    }
    
    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        Jump();
    }

    private void IncreaseScore()
    {
        _score++;
        OnScoreChanged?.Invoke(this, new OnScoreChangedEventArgs{Score = _score});
    }

    private void Move()
    {
        if (_rigidbody.isKinematic)
            return;
        
        Vector3 position = transform.position;
        float speedMultiplier = PlayerData.Mode == Mode.Fast ? 2f : 1f;
        
        transform.position = new Vector3(position.x + speedMultiplier * _movementSpeed * Time.deltaTime, position.y, position.z);
    }

    private void Jump()
    {
        if (_isGameOver)
            return;
        
        _animator.enabled = true;
            
        if (_rigidbody.isKinematic)
        {
            _rigidbody.isKinematic = false;
        }
        
        _rigidbody.AddForce(Vector2.up * _jumpForce * -Mathf.Sign(Physics2D.gravity.y), ForceMode2D.Impulse);
    }
    
    private void Rotate()
    {
        float zRotationAngle = transform.rotation.eulerAngles.z;
        
        switch (_rigidbody.velocity.y)
        {
            case > 0:
                _timer = 0;
                zRotationAngle = transform.rotation.eulerAngles.z + _upRotationSpeed * Time.deltaTime;
                break;
            case < 0 when _timer > _rotationCooldown:
                zRotationAngle = transform.rotation.eulerAngles.z - _downRotationSpeed * Time.deltaTime;
                break;
            case < 0:
                _timer += Time.deltaTime;
                break;
        }

        zRotationAngle = Mathf.Clamp(zRotationAngle, _minRotation, _maxRotation);

        Vector3 rotationAngles = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotationAngles.x, rotationAngles.y, zRotationAngle);
    }
}