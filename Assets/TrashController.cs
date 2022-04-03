using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrashController : MonoBehaviour
{
    GameController _gameCon;
    [SerializeField] Transform _trashSpout = null;

    [Tooltip ("0: X, 1: Star, 2: Hex, 3: Circle, 4: Square, 5: Clover")]
    [SerializeField] GameObject[] _trashPrefabs = null;
    [SerializeField] TextMeshPro _scoreTMP;

    //settings
    [SerializeField] float _timeBetweenTrash = 0.2f;

    //state
    [SerializeField] float _timeForNextTrash;
    List<TrashHandler> _activeTrash = new List<TrashHandler>();
    Queue<TrashHandler> _pooledTrash = new Queue<TrashHandler>();
    int score = 0;

    private void Awake()
    {
        _gameCon = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        _timeForNextTrash = Time.time + _timeBetweenTrash;
    }

    public void Reset()
    {
        for (int i = _activeTrash.Count - 1; i >= 0; i--)
        {
            _activeTrash[i].Despawn();
        }
        
        score = 0;
        _scoreTMP.text = score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (_gameCon.IsInCoreGame && !_gameCon.IsPaused && Time.time >= _timeForNextTrash)
        {
            SpawnTrash();
            _timeForNextTrash = Time.time + _timeBetweenTrash;
        }

        //_timeBetweenTrash -= (Time.deltaTime * .1f);
    }

    private void SpawnTrash()
    {
        TrashHandler nth;
        if (_pooledTrash.Count > 0)
        {
            nth = _pooledTrash.Dequeue();
            nth.gameObject.SetActive(true);
            nth.transform.position = _trashSpout.position;
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, _trashPrefabs.Length);
            nth = Instantiate(_trashPrefabs[rand], _trashSpout.position, Quaternion.identity).GetComponent<TrashHandler>();
            nth.Setup(this);
        }
        int randColor = UnityEngine.Random.Range(0, 5);
        nth.SetTColor((TrashHandler.TColor)randColor);
        _activeTrash.Add(nth);
        score++;
        _scoreTMP.text = score.ToString();
    }

    public void ReturnTrash(TrashHandler completedTrash)
    {
        _pooledTrash.Enqueue(completedTrash);
        completedTrash.gameObject.SetActive(false);
        _activeTrash.Remove(completedTrash);
    }
}
