using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MeshRenderer))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private Material _alarmSignaling; 
    [SerializeField] private Material _notAlarmSignaling; 
    
    private AudioSource _alarmSource;
    private MeshRenderer _renderer;
    private bool _isThiefInsideHouse;

    private void Awake()
    {
        _alarmSource = GetComponent<AudioSource>();
        _alarmSource.Stop();
        _alarmSource.playOnAwake = false;
        _alarmSource.loop = true;

        _renderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _))
        {
            _alarmSource.Play();
            _isThiefInsideHouse = true;
            StartCoroutine(SinusoidVolumeChange());
            _renderer.material = _alarmSignaling;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _))
        {
            _alarmSource.Stop();
            _isThiefInsideHouse = false;
            _renderer.material = _notAlarmSignaling;
        }
    }

    private IEnumerator SinusoidVolumeChange()
    {
        var pastTime = 0f;
        while (_isThiefInsideHouse)
        {
            _alarmSource.volume = Mathf.Sin(pastTime);
            pastTime += Time.deltaTime;
            yield return null;
        }
    }
}