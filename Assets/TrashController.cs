using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    GameController _gameCon;
    [SerializeField] Transform _trashSpout = null;

    [Tooltip ("0: X, 1: Star, 2: Hex, 3: Circle, 4: Square, 5: Clover")]
    [SerializeField] GameObject[] _trashPrefabs = null;

    //settings
    float _timeBetweenTrash = 3f;

    //state
    [SerializeField] float _timeForNextTrash;
    List<GameObject> _allTrash = new List<GameObject>();

    private void Awake()
    {
        _gameCon = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        _timeForNextTrash = Time.time + _timeBetweenTrash;
    }
    // Update is called once per frame
    void Update()
    {
        if (_gameCon.IsInCoreGame && !_gameCon.IsPaused && Time.time >= _timeForNextTrash)
        {
            SpawnTrash();
            _timeForNextTrash = Time.time + _timeBetweenTrash;
        }
    }

    private void SpawnTrash()
    {
        int randShape = UnityEngine.Random.Range(0, _trashPrefabs.Length);
        GameObject newTrash = Instantiate(_trashPrefabs[randShape], _trashSpout.position, Quaternion.identity);
        TrashHandler th = newTrash.GetComponent<TrashHandler>();
        int randColor = UnityEngine.Random.Range(0, 5);
        th.SetTColor((TrashHandler.TColor)randColor);
    }
}
