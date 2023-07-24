using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SoundManagerInstance;

    [SerializeField] private float masterVolume, effectsVolume, musicVolume;
    [SerializeField] private AudioSource music, effects;
    [SerializeField] private AudioClip[] fireSounds, meleeSounds, impactSounds, musicTracks, injurySounds, deathSounds, miscSounds, explosionSounds;

    private bool engaged, musicFading, menuVolumeControlEffectPlaying;
    private float cooldownTimer, fadeTimer, tempMusicVolume, menuVolumeControlEffectCooldownTimer;
    private float maxCooldown = 180.0f;
    private float fadeRate = 0.00075f;
    private float fadeLength = 720.0f;
    private float menuVolumeControlEffectCooldown = 15.0f;
    private float damageCooldownTimerMax = 10.0f;
    private float damageCooldownTimer = 0.0f;
    private int trackTransitionIndex;

    private int bossDeathSoundCounter = 0;

    private int bossPhase;
    private bool alarm;

    private int defaultBGMIndex;

    public Scene prevScene;
    public Scene currScene;

    private bool laserSound;
    private bool chargeSound;

    void Awake()
    {
        if(SoundManagerInstance == null)
        {
            SoundManagerInstance = this;
            DontDestroyOnLoad(gameObject);
            AudioListener.volume = masterVolume;
            music.volume = musicVolume;
            effects.volume = effectsVolume;

            cooldownTimer = maxCooldown;
            musicFading = false;
            bossPhase = 0;
            alarm = false;
            laserSound = false;

            CheckScene(true);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckScene(bool updateTrack)
    {
        prevScene = currScene;
        Scene currentScene = SceneManager.GetActiveScene();
        currScene = currentScene;

        switch (currentScene.name)
        {
            case "openreal":
                defaultBGMIndex = 6;
                break;

            case "endreal":
                defaultBGMIndex = 24;
                break;

            case "Town":
                defaultBGMIndex = 0;
                break;
            case "Forest 1":
                defaultBGMIndex = 18;
                break;
            case "Town-Template":
                defaultBGMIndex = 8;
                break;
            case "Level 1":
                defaultBGMIndex = 8;
                break;
            case "Level 2":
                defaultBGMIndex = 10;
                break;
            case "Level 2 and a half":
                if (!alarm) defaultBGMIndex = 8;
                else defaultBGMIndex = 22;
                break;
            case "Level 3":
                defaultBGMIndex = 12;
                break;
            case "BossRoom":
                if (bossPhase == 0) defaultBGMIndex = 20;
                else defaultBGMIndex = 16;
                bossDeathSoundCounter = 0;
                break;
            case "Forest 2":
                defaultBGMIndex = 16;
                break;
            case "tavern":
            case "tavernnormal":
            case "tavernnormal2":
                defaultBGMIndex = 2;
                break;
            case "StartScreen":
                defaultBGMIndex = 6;
                break;
            
        }

        if (updateTrack) { ChangeMusicTrack(defaultBGMIndex); music.volume = musicVolume; }
    }

    void FixedUpdate()
    {
        if (engaged)
        {
            //Debug.Log(cooldownTimer);
            cooldownTimer -= 1.0f;
            if(cooldownTimer <= 0.0f)
            {
                engaged = false;
                //FadeMusicTrack(0); 
                CheckScene(false);
                FadeMusicTrack(defaultBGMIndex);
            }
        }
        if (damageCooldownTimer >= 0)
        {
            damageCooldownTimer -= 1.0f; 
        }

        if (musicFading)
        {
            if (engaged)
            {
                musicFading = false;
                musicVolume = tempMusicVolume;
                music.volume = musicVolume;
            }

            music.volume -= fadeRate;
            fadeTimer -= 1.0f;
            //if(fadeTimer <= 0 || music.volume <= 0)
            if(music.volume <= 0)
            {
                musicFading = false;
                ChangeMusicTrack(trackTransitionIndex);
                musicVolume = tempMusicVolume;
                music.volume = musicVolume;
            }
        }
        if (menuVolumeControlEffectPlaying)
        {
            menuVolumeControlEffectCooldownTimer -= 1.0f;
            if(menuVolumeControlEffectCooldownTimer <= 0)
            {
                menuVolumeControlEffectPlaying = false;
            }
        }
    }

    public void BossPhase(int i)
    {
        if(bossPhase == 0 && i != 0)
        {
            ChangeMusicTrack(16);
        }

        bossPhase = i;
    }

    public void SoundAlarm()
    {
        if (music.volume > 0)
        {
            musicVolume += 0.15f;
            music.volume += 0.15f;
        }
        alarm = true;
        ChangeMusicTrack(22);
        
    }

    public void LaserLevelFireSound()
    {
        if (laserSound == true) return;
        else
        {
            laserSound = true;
            PlayFireSound(13);
            Invoke("LaserLevelFireSound2", 2f);
        }
    }

    public void LaserLevelChargeSound()
    {
        if (chargeSound == true) return;
        else
        {
            chargeSound = true;
            PlayFireSound(14);
            Invoke("LaserLevelChargeReset", 2f);
        }
    }

    public void LaserLevelFireSound2()
    {
        PlayFireSound(13);
        laserSound = false;
    }

    public void LaserLevelChargeReset()
    {
        chargeSound = false;
    }

    public Scene getCurrentScene()
    {
        return currScene;
    }
    public Scene getPreviousScene()
    {
        return prevScene;
    }

    public void FadeMusicTrack(int i)
    {
        tempMusicVolume = musicVolume;
        musicFading = true;
        trackTransitionIndex = i;
        fadeTimer = fadeLength;
    }

    public void ResetSettings()
    {
        musicFading = false;
        engaged = false;
    }

    public void Engage()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Level 3" || currentScene.name == "BossRoom" || currentScene.name == "Forest 2" || (currentScene.name == "Level 2 and a half" && alarm == true)) return;
        if (!engaged)
        {
            engaged = true;
            if(!musicFading) ChangeMusicTrack(4);
        }
        cooldownTimer = maxCooldown;
    }

    public void ChangeMusicTrack(int i)
    {
        if (i >= musicTracks.Length || i+1 >= musicTracks.Length) return;
        music.Stop();
        music.clip = musicTracks[i];
        if(musicTracks[i+1])
        {
            music.PlayOneShot(musicTracks[i + 1]);
        }
        music.PlayScheduled(AudioSettings.dspTime + musicTracks[i + 1].length);
    }
    
    public void PlayDashSound(AudioClip clip)
    {
        effects.PlayOneShot(clip);
    }

    public void PlayFireSound(int i)
    {
        if (i >= fireSounds.Length) return;
        effects.PlayOneShot(fireSounds[i]);
    }

    public void PlayMeleeSound(int i)
    {
        if (i >= meleeSounds.Length) return;
        effects.PlayOneShot(meleeSounds[i]);
    }

    public void PlayImpactSound(int i, bool cooldown = true)
    {
        if (damageCooldownTimer <= 0 || cooldown == false)
        {
            if (i >= impactSounds.Length) return;
            effects.PlayOneShot(impactSounds[i]);
            damageCooldownTimer = damageCooldownTimerMax;
        }
    }

    public void PlayInjurySound(int i)
    {
        if(damageCooldownTimer <= 0)
        {
            if (i >= injurySounds.Length) return;
            effects.PlayOneShot(injurySounds[i]);
            damageCooldownTimer = damageCooldownTimerMax;
        }
    }

    public void PlayDeathSound(int i)
    {
        if (i >= deathSounds.Length) return;

        if (i == 8 && bossDeathSoundCounter > 5) return;
        else if (i == 8) bossDeathSoundCounter++;

        effects.PlayOneShot(deathSounds[i]);
    }

    public void PlayMiscSound(int i)
    {
        if (i >= miscSounds.Length) return;
        effects.PlayOneShot(miscSounds[i]);
    }

    public void PlayExplosionSound(int i)
    {
        if (i >= explosionSounds.Length) return;
        effects.PlayOneShot(explosionSounds[i]);
    }

    public void ChangeVolume(int type, float value)
    {
        switch (type)
        {
            case 1:
                masterVolume = value;
                AudioListener.volume = masterVolume;
                break;
            case 2:
                effectsVolume = value;
                effects.volume = effectsVolume;
                if (!menuVolumeControlEffectPlaying)
                {
                    PlayFireSound(0);
                    menuVolumeControlEffectPlaying = true;
                    menuVolumeControlEffectCooldownTimer = menuVolumeControlEffectCooldown;
                }
                break;
            case 3:
                musicVolume = value;
                music.volume = musicVolume;
                break;
        }
    }
}
