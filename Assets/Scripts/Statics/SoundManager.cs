using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    AudioSource audioSource;
    [SerializeField]
   AudioClip[] list_audios;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.volume = 1;
        //list_audios = Resources.LoadAll<AudioClip>("sounds") as AudioClip[];
    }

    AudioClip getSoundByName(string name) {
        for (int i = 0; i < list_audios.Length; i++) {
            if (name.Equals(list_audios[i].name)) {
                return list_audios[i];
            }
        }
        return null;
    }

    void PlaySound(string soundName, bool isLoop) {
        if (BaseInfo.gI().isSound) {
            pauseSound();
            //audioSource.PlayOneShot(Resources.Load("sounds/" + soundName) as AudioClip);
            audioSource.PlayOneShot(getSoundByName(soundName));
            audioSource.loop = isLoop;
        }
    }

    public void pauseSound() {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    public void PlayVibrate() {
        if (BaseInfo.gI().isVibrate)
            Handheld.Vibrate();
    }

    public void startAlertAudio()
    {
        PlaySound("alert", false);
    }

    public void startchiabaiAudio() {
        PlaySound("danhbai", false);
    }

    public void startCountDownAudio() {
        PlaySound("countdown", true);
    }

    public void startLostAudio() {
        PlaySound("bet", false);
    }

    public void startMessageAudio() {
        PlaySound("message", false);
    }

    public void startTineCountAudio() {
        PlaySound("timecount", false);
    }

    public void startToAudio() {
        PlaySound("to", false);
    }

    public void startWinAudio() {
        PlaySound("nhat", false);
    }

    public void startClickButtonAudio() {
        PlaySound("add", false);
    }

    public void startChiaBaiLienTucAudio()
    {
        PlaySound("chiabai_lien_tuc", true);
    }

    public void stopChiaBaiLienTucAudio()
    {
    }

    public void startAnbaiAudio()
    {
        PlaySound("anbai", false);
    }

    public void startBiAnbaiAudio()
    {
        PlaySound("bianbai", false);
    }


    public void startAnbairacAudio() {
        PlaySound("anbairac", false);
    }

    public void startMomAudio() {
        PlaySound("mom", false);
    }

    public void startBaAudio() {
        PlaySound("ba", false);
    }

    public void startNhatAudio() {
        PlaySound("nhat", false);
    }

    public void startGuibaiAudio() {
        PlaySound("guibai", false);
    }

    public void startHaphomAudio() {
        PlaySound("haphom", false);
    }

    public void startVaobanAudio() {
        PlaySound("knock", false);
    }

    public void startBinhlungAudio() {
        PlaySound("binhlung", false);
    }

    public void startFinishAudio() {
        PlaySound("finished", false);
    }

    public void startSobaiAudio() {
        PlaySound("sobai", false);
    }

    public void startMaubinhAudio() {
        PlaySound("maubinh", false);
    }

    public void start_HAINE() {
        PlaySound("haine", false);
    }

    public void start_MAYHABUOI() {
        PlaySound("mayhabuoi", false);
    }

    public void start_ThuaDiCung() {
        PlaySound("thuadicung", false);
    }

    public void start_DODI() {
        PlaySound("dodi", false);
    }

    public void start_ChetNE() {
        PlaySound("chetmayne", true);
    }

    public void start_random() {
        int ran = Random.Range(1, 4);
        switch (ran) {
            case 1:
                start_DODI();
                break;
            case 2:
                start_MAYHABUOI();
                break;
            case 3:
                start_ThuaDiCung();
                break;
            case 4:
                start_ChetNE();
                break;

        }
    }

    public void startUAudio() {
        PlaySound("u", false);
    }

    ///Xoc dia
    //public void startVaobanAudio () {
    //    PlaySoundXocdia ("knock", false);
    //}

    public void startXocdiaAudio() {
        PlaySound("xoc_dia", false);
    }

    public void MoneyAudio() {
        PlaySound("xeng_money", false);
    }

    //public void startWinAudioXocdia() {
    //    PlaySoundXocdia("nhat", false);
    //}

    //public void startLoseAudioXocdia() {
    //    PlaySoundXocdia("bet", false);
    //}

    public void clickBtnAudio() {
        PlaySound("xeng_click", false);
    }
    ///Xocdia

    public void start_xeng_lose() {
        PlaySound("xeng_lose", false);
    }

    public void start_xeng_shot() {
        PlaySound("xeng_shot", false);
    }
    public void start_xeng_spin() {
        PlaySound("xeng_spin", false);
    }
    public void start_xeng_win() {
        PlaySound("xeng_win", false);
    }


    public void start_taixiu_win() {
        PlaySound("win_taisiu", false);
    }
}
