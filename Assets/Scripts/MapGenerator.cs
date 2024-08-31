using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _gatePrefab;
    [SerializeField] private float _yGateOffset = 0.3f;
    [SerializeField] private float _xStartGatePosition = 1f;
    [SerializeField] private float _xDistBetweenGates = 0.6f;

    private int _spawnedGatesCount;

    private void Start()
    {
        Bird.Instance.OnScoreChanged += Bird_OnScoreChanged;

        for (int i = 0; i < 5; i++)
        {
            SpawnGate();
        }
    }
    
    private void SpawnGate()
    {
        float yPosition = Random.Range(-_yGateOffset, _yGateOffset);
        float xPosition = _xStartGatePosition + _xDistBetweenGates * _spawnedGatesCount;
        Vector3 spawnPosition = new Vector3(xPosition, yPosition, _gatePrefab.transform.position.z);
        
        Instantiate(_gatePrefab, spawnPosition, Quaternion.identity);
        
        _spawnedGatesCount++;
    }
    
    private void Bird_OnScoreChanged(object sender, Bird.OnScoreChangedEventArgs e)
    {
        SpawnGate();
    }
}