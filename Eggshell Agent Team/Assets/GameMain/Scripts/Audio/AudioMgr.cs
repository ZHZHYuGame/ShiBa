using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMgr : MonoBehaviour
{
    // 单例实例
    public static AudioMgr Instance { get; private set; }

    [Header("音频设置")]
    [SerializeField, Tooltip("最大同时播放音效数")]
    private int maxSimultaneousSFX = 10;

    [SerializeField, Tooltip("默认音效空间混合（0=2D, 1=3D）")]
    private float defaultSpatialBlend = 0f;

    [Header("音频混合器")]
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private AudioMixerGroup musicMixerGroup;

    [SerializeField]
    private AudioMixerGroup sfxMixerGroup;

    // 对象池
    private Queue<AudioSource> audioSourcePool = new Queue<AudioSource>();
    private List<AudioSource> activeSources = new List<AudioSource>();

    // 当前背景音乐
    private AudioSource currentMusicSource;

    private void Awake()
    {
        // 单例初始化
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
       // DontDestroyOnLoad(gameObject);

        InitializePool();
    }

    // 初始化音频源池
    private void InitializePool()
    {
        for (int i = 0; i < maxSimultaneousSFX; i++)
        {
            CreateNewAudioSource();
        }
    }

    // 创建新的AudioSource
    private AudioSource CreateNewAudioSource()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.spatialBlend = defaultSpatialBlend;
        source.playOnAwake = false;
        audioSourcePool.Enqueue(source);
        return source;
    }

    // 播放音效
    public void PlaySFX(AudioClip clip, float volume = 1f, bool is3D = false)
    {
        if (clip == null) return;

       
        AudioSource source = GetAvailableAudioSource();
        if (source == null) return;
        
        ConfigureAudioSource(source, clip, volume, is3D ? 1f : defaultSpatialBlend);
        source.outputAudioMixerGroup = sfxMixerGroup;
        source.Play();
        StartCoroutine(ReturnToPoolWhenFinished(source));
    }

    // 播放3D音效
    public void Play3DSFX(AudioClip clip, Vector3 position, float minDistance = 1f,
                         float maxDistance = 10f, float volume = 1f)
    {
        AudioSource source = GetAvailableAudioSource();
        if (source == null) return;

        source.transform.position = position;
        source.minDistance = minDistance;
        source.maxDistance = maxDistance;
        ConfigureAudioSource(source, clip, volume, 1f);
        source.outputAudioMixerGroup = sfxMixerGroup;
        source.Play();
        StartCoroutine(ReturnToPoolWhenFinished(source));
    }

    // 播放背景音乐
    public void PlayMusic(AudioClip clip, float volume = 1f, bool loop = true)
    {
        if (currentMusicSource != null && currentMusicSource.isPlaying)
        {
            currentMusicSource.Stop();
        }

        if (currentMusicSource == null)
        {            
            currentMusicSource = gameObject.AddComponent<AudioSource>();
        }

        ConfigureAudioSource(currentMusicSource, clip, volume, 0f);
        currentMusicSource.outputAudioMixerGroup = musicMixerGroup;
        currentMusicSource.loop = loop;
        currentMusicSource.Play();
    }

    // 获取可用音频源
    private AudioSource GetAvailableAudioSource()
    {
        // 从池中查找可用源
        while (audioSourcePool.Count > 0)
        {
            AudioSource source = audioSourcePool.Dequeue();
            if (!source.isPlaying)
            {
                activeSources.Add(source);
                return source;
            }
            activeSources.Add(source);
        }

        // 池已满时创建新源（动态扩容）
        if (activeSources.Count < maxSimultaneousSFX * 2)
        {
            AudioSource newSource = CreateNewAudioSource();
            activeSources.Add(newSource);
            return newSource;
        }

        return null;
    }

    // 配置音频源参数
    private void ConfigureAudioSource(AudioSource source, AudioClip clip,
                                     float volume, float spatialBlend)
    {
        
        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = spatialBlend;
    }

    // 播放完成后回收到池
    private System.Collections.IEnumerator ReturnToPoolWhenFinished(AudioSource source)
    {
        yield return new WaitWhile(() => source.isPlaying);

        if (activeSources.Contains(source))
        {
            activeSources.Remove(source);
            audioSourcePool.Enqueue(source);
        }
    }



    // 暂停所有音效
    public void PauseAllSFX()
    {
        foreach (var source in activeSources)
        {
            if (source.isPlaying) source.Pause();
        }
    }

    // 恢复所有音效
    public void ResumeAllSFX()
    {
        foreach (var source in activeSources)
        {
            if (!source.isPlaying) source.UnPause();
        }
    }

    // 设置全局音量
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", LinearToDecibel(volume));
    }

    // 线性音量转分贝
    private float LinearToDecibel(float linear)
    {
        return linear <= 0 ? -80f : Mathf.Log10(linear) * 20f;
    }

    // 清理资源
    public void Cleanup()
    {
        foreach (var source in activeSources)
        {
            if (source != null) source.Stop();
        }
        activeSources.Clear();
        audioSourcePool.Clear();
    }

    /// <summary>
    /// 简单的音频增益
    /// </summary>
    /// <param name="data">音频样本数据，即多个声道的样本交替存储</param>
    /// <param name="channels">音频的声道数</param>
    /// <param name="gain">增益倍数</param>

    //public void OnAudioFilterRead(float[] data ,int channels,float gain)
    //{
    //    for(int i=0;i<data.Length;i++)
    //    {
    //        data[i]*=gain;//对每个音频样本应用增益
    //    }
    //}
}
