using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _gatePrefab;
    [SerializeField] private float _yGateOffset = 0.3f;
    [SerializeField] private float _xStartGatePosition = 1f;
    [SerializeField] private float _xDistBetweenGates = 0.6f;

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private float _yCoinOffset = 0.45f;

    private int _spawnedGatesCount;
    private int _spawnedCoinsCount;

    private void Start()
    {
        Bird.Instance.OnScoreChanged += Bird_OnScoreChanged;

        for (int i = 0; i < 5; i++)
        {
            SpawnGate();
            SpawnCoin();
        }
    }
    
    private void SpawnGate()
    {
        float distMultiplier = PlayerData.Mode == Mode.Fast ? 2f : 1f;
        float yPosition = Random.Range(-_yGateOffset, _yGateOffset);
        float xPosition = _xStartGatePosition + distMultiplier * _xDistBetweenGates * _spawnedGatesCount;
        Vector3 spawnPosition = new Vector3(xPosition, yPosition, _gatePrefab.transform.position.z);
        
        Instantiate(_gatePrefab, spawnPosition, Quaternion.identity);
        
        _spawnedGatesCount++;
    }

    private void SpawnCoin()
    {
        float distMultiplier = PlayerData.Mode == Mode.Fast ? 2f : 1f;
        float yPosition = Random.Range(-_yCoinOffset, _yCoinOffset);
        float xPosition = _xStartGatePosition + distMultiplier * _xDistBetweenGates * _spawnedCoinsCount + distMultiplier * _xDistBetweenGates / 2;
        Vector3 spawnPosition = new Vector3(xPosition, yPosition, _coinPrefab.transform.position.z);
        
        Instantiate(_coinPrefab, spawnPosition, Quaternion.identity);

        _spawnedCoinsCount++;
    }
    
    private void Bird_OnScoreChanged(object sender, Bird.OnScoreChangedEventArgs e)
    {
        SpawnGate();
        SpawnCoin();
    }
}