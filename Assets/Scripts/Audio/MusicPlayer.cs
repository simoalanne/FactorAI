using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        public static MusicPlayer Instance { get; private set; }
        [SerializeField] private List<AudioClip> _musicClips;
        [SerializeField] private bool _shuffleMusic = true;
        [SerializeField] private float _volumeFromZeroToOne = 0.1f;
        private AudioSource _audioSource;
        private int _currentClipIndex = 0;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = _volumeFromZeroToOne;
            _audioSource.loop = false;

            if (PlayerPrefs.GetInt("MusicEnabled", 1) == 0 || PlayerPrefs.GetInt("AudioEnabled", 1) == 0)
            {
                _audioSource.mute = true;
            }

            if (_shuffleMusic)
            {
                ShuffleList();
            }
            _audioSource.clip = _musicClips[_currentClipIndex];
            _audioSource.Play();
            StartCoroutine(CheckIfClipHasEnded());
        }

        private void ShuffleList() // Fisher-Yates shuffle algorithm :D
        {
            if (_musicClips.Count == 0)
            {
                Debug.LogError("No music clips to choose from");
                return;
            }

            for (int i = 0; i < _musicClips.Count; i++)
            {
                int randomIndex = Random.Range(i, _musicClips.Count);
                (_musicClips[i], _musicClips[randomIndex]) = (_musicClips[randomIndex], _musicClips[i]);
            }
        }

        private IEnumerator CheckIfClipHasEnded()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);

                if (!_audioSource.isPlaying)
                {
                    _currentClipIndex = (_currentClipIndex + 1) % _musicClips.Count; // Loop back to the beginning of the list when the last clip has ended
                    _audioSource.clip = _musicClips[_currentClipIndex];
                    _audioSource.Play();
                }
            }
        }

        public void ToggleMusic()
        {
            if (PlayerPrefs.GetInt("MusicEnabled", 1) == 0 || PlayerPrefs.GetInt("AudioEnabled", 1) == 0)
            {
                _audioSource.mute = true;
            }
            else
            {
                _audioSource.mute = false;
            }
        }

        public void PauseMusic()
        {
            _audioSource.Pause();
        }

        public void UnpauseMusic()
        {
            _audioSource.UnPause();
        }
    }
}
