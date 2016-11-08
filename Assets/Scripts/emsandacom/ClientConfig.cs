using UnityEngine;
using System.Collections;
//using Broadcast;
//using Utilities;
//using WebSocketSharp;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
//using AppUsMobile.Modules.Sound;

namespace AppConfig
{
    public class ClientConfig
    {

        public class UserInfo
        {
            public enum LoginType
            {
                TRYAL = -1,
                GAME,
                FACEBOOK
            }

            private static string uname = "", disp_name = "";
            private static string session = "";
            private static string password = "";
            private static string avatar_url = "";
            private static string markid = "";
            private static long cash_gold = 0, cash_silver = 0;
            private static int tutorial_finished = 0;
            private static int level = 0;
            private static string level_name = "";
            private static string facebook_id = "";
            private static int use_facebook_avatar = 0;
            private static int is_first_time_login_facebook = 0;

            private static LoginType login_type = LoginType.TRYAL;

            public static void InitUserInfo()
            {
                //init user info
                UserInfo.uname = PlayerPrefs.GetString("uname", "");
                UserInfo.disp_name = PlayerPrefs.GetString("disp_name", "");
                UserInfo.session = PlayerPrefs.GetString("session", "");
                UserInfo.password = PlayerPrefs.GetString("password", "");
                UserInfo.avatar_url = PlayerPrefs.GetString("avatar_url", "");
                UserInfo.markid = PlayerPrefs.GetString("markid", "");
                UserInfo.cash_gold = (long)PlayerPrefs.GetFloat("cash_gold", 0);
                UserInfo.cash_silver = (long)PlayerPrefs.GetFloat("cash_silver", 0);
                UserInfo.tutorial_finished = PlayerPrefs.GetInt("tutorial_finished", 0);
                UserInfo.login_type = (LoginType)PlayerPrefs.GetInt("login_type", (int)LoginType.TRYAL);
                UserInfo.level = PlayerPrefs.GetInt("level", 0);
                UserInfo.level_name = PlayerPrefs.GetString("level_name", "");
                UserInfo.facebook_id = PlayerPrefs.GetString("facebook_id", "");
                UserInfo.use_facebook_avatar = PlayerPrefs.GetInt("use_facebook_avatar", 0);
                UserInfo.is_first_time_login_facebook = PlayerPrefs.GetInt("is_first_time_login_facebook", 0);
            }

            #region USER INFO
            public static string UNAME
            {
                get
                {
                    return uname;
                }
                set
                {
                    uname = value;
                    PlayerPrefs.SetString("uname", uname);
                    PlayerPrefs.Save();
                }
            }

            public static string DISPLAY_NAME
            {
                get
                {
                    return (disp_name == null || disp_name.Length == 0) ? uname : disp_name;
                }
                set
                {
                    disp_name = value;
                    PlayerPrefs.SetString("disp_name", disp_name);
                    PlayerPrefs.Save();
                }
            }

            public static string SESSION
            {
                get
                {
                    return session;
                }
                set
                {
                    session = value;
                    PlayerPrefs.SetString("session", session);
                    PlayerPrefs.Save();
                }
            }

            public static string PASSWORD
            {
                get
                {
                    return password;
                }
                set
                {
                    password = value;
                    PlayerPrefs.SetString("password", password);
                    PlayerPrefs.Save();
                }
            }

            public static LoginType LOGIN_TYPE
            {
                get
                {
                    return login_type;
                }

                set
                {
                    login_type = value;
                    PlayerPrefs.SetInt("login_type", (int)login_type);
                    PlayerPrefs.Save();
                }
            }

            public static string MARK_ID
            {
                get
                {
                    return markid;
                }
                private set
                {
                    markid = value;
                    PlayerPrefs.SetString("markid", markid);
                    PlayerPrefs.Save();
                }
            }

            public static long CASH_GOLD
            {
                get
                {
                    return cash_gold;
                }
                set
                {
                    cash_gold = value;
                    PlayerPrefs.SetFloat("cash_gold", cash_gold);
                    PlayerPrefs.Save();
                }
            }

            public static long CASH_SILVER
            {
                get
                {
                    return cash_silver;
                }
                set
                {
                    cash_silver = value;
                    PlayerPrefs.SetFloat("cash_silver", cash_silver);
                    PlayerPrefs.Save();
                }
            }

            public static bool TUTORIAL_FINISHED
            {
                get { return tutorial_finished == 0 ? false : true; }
                set
                {
                    tutorial_finished = value == true ? 1 : 0;
                    PlayerPrefs.SetInt("tutorial_finished", tutorial_finished);
                    PlayerPrefs.Save();
                }
            }

            public static int LEVEL
            {
                get
                {
                    return level;
                }
                set
                {
                    level = value;
                    PlayerPrefs.SetInt("level", level);
                    PlayerPrefs.Save();
                }
            }

            public static string LEVEL_NAME
            {
                get
                {
                    return level_name;
                }
                set
                {
                    level_name = value;
                    PlayerPrefs.SetString("level_name", level_name);
                    PlayerPrefs.Save();
                }
            }

            public static string FACEBOOK_ID
            {
                get
                {
                    return facebook_id;
                }
                set
                {
                    facebook_id = value;
                    PlayerPrefs.SetString("facebook_id", facebook_id);
                    PlayerPrefs.Save();
                }
            }

            public static int USE_FACEBOOK_AVATAR
            {
                get
                {
                    return use_facebook_avatar;
                }
                set
                {
                    use_facebook_avatar = value;
                    PlayerPrefs.SetInt("use_facebook_avatar", use_facebook_avatar);
                    PlayerPrefs.Save();
                }
            }

            public static int IS_FIRST_TIME_LOGIN_FACEBOOK
            {
                get
                {
                    return is_first_time_login_facebook;
                }
                set
                {
                    is_first_time_login_facebook = value;
                    PlayerPrefs.SetInt("is_first_time_login_facebook", is_first_time_login_facebook);
                    PlayerPrefs.Save();
                }
            }

            /// <summary>
            /// Chay trong mainthread vi co thao tac luu vao cache (PlayerPrefs) (@see MonoBihaviourHelper.ExecuteIEnumerator)
            /// </summary>
            /// <param name="uname"></param>
            /// <param name="password"></param>
            /// <param name="session"></param>
            /// <param name="cash"></param>
            public static void SetUserInfo(string uname, string disp_name, string password, string session, string markid, LoginType type, long cash_gold, long cash_silver, int level, string level_name)
            {
                UNAME = uname;
                DISPLAY_NAME = disp_name;
                PASSWORD = password;
                SESSION = session;
                MARK_ID = markid;
                CASH_GOLD = cash_gold;
                CASH_SILVER = cash_silver;
                LEVEL = level;
                LEVEL_NAME = level_name;

                LOGIN_TYPE = type;

                //CacheUserInfo(uname, disp_name, password, session, markid, type, cash_gold, cash_silver);
            }

            private static void CacheUserInfo(string uname, string disp_name, string password, string session, string markid, LoginType type, long cash_gold, long cash_silver, int level, string level_name)
            {
                PlayerPrefs.SetString("uname", uname);
                PlayerPrefs.SetString("disp_name", disp_name);
                PlayerPrefs.SetString("session", session);
                PlayerPrefs.SetString("password", password);
                PlayerPrefs.SetString("markid", markid);
                PlayerPrefs.SetFloat("cash_gold", cash_gold);
                PlayerPrefs.SetFloat("cash_silver", cash_silver);
                PlayerPrefs.SetInt("login_type", (int)type);
                PlayerPrefs.SetInt("level", level);
                PlayerPrefs.SetString("level_name", level_name);

                PlayerPrefs.Save();
            }

            public static void ClearUserInfo()
            {
                //UNAME = ""; ko clear username, su dung de autologin hoac dien vao phan goi y login
                DISPLAY_NAME = "";
                PASSWORD = "";
                SESSION = "";
                MARK_ID = "";
                CASH_GOLD = 0;
                CASH_SILVER = 0;
                LEVEL = 0;
                LEVEL_NAME = "";

            }
            #endregion
        }
        public class SoftWare
        {
            private const string KEY_PREF_DBTOR = "cnf_dbtor_pref";
            private const string KEY_PREF_UTM_MEDIUM = "cnf_utm_medium_pref";

            /// <summary>
            /// software
            /// </summary>
            private static string version = "1.0.1";
            private static string channel = "p70";
            private static string utm_medium = "";

            public static void InitSoftWare()
            {
                //init software info
                SoftWare.VERSION = "1.0.1";
                SoftWare.CHANNEL = PlayerPrefs.GetString(KEY_PREF_DBTOR, "p70");
                SoftWare.UTM_MEDIUM = PlayerPrefs.GetString(KEY_PREF_UTM_MEDIUM, "");
            }

            #region SOFTWARE
            public static string VERSION
            {
                get
                {
                    return version;
                }
                private set
                {
                    version = value;
                }
            }

            public static string CHANNEL
            {
                get
                {
                    return channel;
                }
                set
                {
                    channel = value;
                    PlayerPrefs.SetString(KEY_PREF_DBTOR, channel);
                    PlayerPrefs.Save();
                }
            }

            public static string UTM_MEDIUM
            {
                get
                {
                    return utm_medium;
                }
                set
                {
                    utm_medium = value;
                    PlayerPrefs.SetString(KEY_PREF_UTM_MEDIUM, utm_medium);
                    PlayerPrefs.Save();
                }
            }
            #endregion

        }

        public class HardWare
        {
            /// <summary>
            /// hardware
            /// </summary>
            private static string imeii = "";
            private static string imei = "";
            private static string cellid = "";
            private static string mnc = "";
            private static string mcc = "";
            private static string lac = "";
            private static string platform = "";
            private static string device = "";
            private static string macaddress = "";

            public static void InitHardWare()
            {
                //init hardware info
                //TelephonyManager tm = (TelephonyManager)getSystemService(Context.TELEPHONY_SERVICE);
#if UNITY_ANDROID && !UNITY_EDITOR
                //AndroidJavaObject TM = new AndroidJavaObject("android.telephony.TelephonyManager");
                //Debug.LogError("AndroidJavaObject " + );
                //IMEII = TM.Call<string>("getDeviceId");

                //AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                //AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
                //AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
                //AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
                //IMEII = secure.CallStatic<string>("getString", contentResolver, "android_id");

                //TelephonyManager mngr = (TelephonyManager)getSystemService(Context.TELEPHONY_SERVICE); 
                //mngr.getDeviceId();
#endif

                //HardWare.IMEI = SystemInfo.deviceUniqueIdentifier;

                HardWare.CELLID = "000000";
                HardWare.MNC = "04";
                HardWare.MCC = "452";
                HardWare.LAC = "0";
                HardWare.PLATFORM = Application.platform.ToString();
                HardWare.DEVICE = SystemInfo.deviceName;
            }

#region HARDWARE

            public static string IMEII
            {
                get
                {
                    return imeii;
                }
                private set
                {
                    imeii = value != null ? value : "";
                }
            }


            public static string IMEI
            {
                get
                {
                    return imei;
                }
                set
                {
                    imei = value != null ? value : SystemInfo.deviceUniqueIdentifier;
                }
            }

            public static string CELLID
            {
                get
                {
                    return cellid;
                }
                set
                {
                    cellid = value != null ? value : "";
                }
            }

            public static string MNC
            {
                get
                {
                    return mnc;
                }
                set
                {
                    mnc = value != null ? value : "";
                }
            }

            public static string MCC
            {
                get
                {
                    return mcc;
                }
                set
                {
                    mcc = value != null ? value : "";
                }
            }

            public static string LAC
            {
                get
                {
                    return lac;
                }
                set
                {
                    lac = value != null ? value : "";
                }
            }

            public static string PLATFORM
            {
                get
                {
                    return platform;
                }
                private set
                {
                    platform = value != null ? value : "";
                    if (platform.ToLower().Contains("iphone"))
                        platform = "iphone";
                    if (platform.ToLower().Contains("android"))
                        platform = "android";
                }
            }

            public static string DEVICE
            {
                get
                {
                    return device;
                }
                private set
                {
                    device = value != null ? value : "";
                }
            }

            public static string MACADDRESS
            {
                get
                {
                    return macaddress;
                }
                set
                {
                    macaddress = value != null ? value : "";
                }
            }
#endregion
        }
        
        public class Language
        {
            //http://stackoverflow.com/questions/3191664/list-of-all-locales-and-their-short-codes
            public static readonly string EN = "en";
            public static readonly string VN = "vn";
            public static readonly string CN = "cn";
            public static readonly string TH = "th";
            public static readonly string ID = "id";
            public static readonly string TW = "tw";

            private static string lang = "vn";

            private static JObject N;
            private static bool isInited = false;

            public static void InitLanguage()
            {
                if (!isInited)
                {
                    //Debug.LogError("LangHelper: Init");
                    TextAsset all_lang_as_json_string = Resources.Load("res_langs") as TextAsset;
                    //Debug.Log("LANG" + all_lang_as_json_string.text);
                    N = JObject.Parse(all_lang_as_json_string.text);

                    //khoi tao lang, lay tu cache, neu cache chua co thi gia tri khoi tao la VI
                    //Fix VI nen comment doan code nay
                    //								string tempLang = PlayerPrefs.GetString ("lang", "");
                    //								if (string.IsNullOrEmpty (tempLang)) {
                    //										if (Application.systemLanguage == SystemLanguage.Vietnamese)
                    //												LANG = VI;
                    //										else if (Application.systemLanguage == SystemLanguage.Chinese)
                    //												LANG = CN;
                    //										else
                    //												LANG = EN;
                    //								} else 
                    //										LANG = tempLang;

                    //				LANG = VN;
                    LANG = !string.IsNullOrEmpty(PlayerPrefs.GetString("lang_device")) ? PlayerPrefs.GetString("lang_device") : VN;
                    isInited = true;
                }
            }

            public static string LANG
            {
                get
                {
                    return lang;
                }

                set
                {
                    lang = value;
                    //luu vao cache
                    //Fix Vi nen ko luu cache
                    //				PlayerPrefs.SetString ("lang", lang);
                    //				PlayerPrefs.Save ();
                    //Broadcast
                    int msg = 1;//0 = Open Loading Popup, 1 = Change Language in LangLocalize, 2 = Close Popup, 3 = Change Fail
                    //BroadcastReceiver.Instance.BroadcastMessage(MessageCode.APP, MessageType.LangChanged, msg);
                }
            }

            public static string GetText(string key)
            {
                string value = N[lang][key].ToString();
                if (string.IsNullOrEmpty(value))
                    return key + "-" + lang;
                else
                    return value;
            }

            public static string GetText(string lang, string key)
            {
                return N[lang][key].ToString();
            }
        }

#region Sound
        public class StringValueAttribute : Attribute
        {
            public string Value { get; set; }
        }
        public class Sound
        {
            public enum SoundId
            {
                [StringValue(Value = "Sounds/Background_Music")]
                Background_Music,
                [StringValue(Value = "Sounds/Button_Click")]
                Button_Click,

                //Emotion Chat
                [StringValue(Value = "Sounds/Emoticon/1181315")]
                Emotion_1,
                [StringValue(Value = "Sounds/Emoticon/1181321")]
                Emotion_2,
                [StringValue(Value = "Sounds/Emoticon/1181322")]
                Emotion_3,
                [StringValue(Value = "Sounds/Emoticon/1181329")]
                Emotion_4,
                [StringValue(Value = "Sounds/Emoticon/1181330")]
                Emotion_5,
                [StringValue(Value = "Sounds/Emoticon/1181332")]
                Emotion_6,
                [StringValue(Value = "Sounds/Emoticon/1181336")]
                Emotion_7,
                [StringValue(Value = "Sounds/Emoticon/1181338")]
                Emotion_8,
                [StringValue(Value = "Sounds/Emoticon/1182338")]
                Emotion_9,
                [StringValue(Value = "Sounds/Emoticon/1181356")]
                Emotion_10,
                [StringValue(Value = "Sounds/Emoticon/1181357")]
                Emotion_11,
                [StringValue(Value = "Sounds/Emoticon/1181362")]
                Emotion_12,
                [StringValue(Value = "Sounds/Emoticon/1181363")]
                Emotion_13,
                [StringValue(Value = "Sounds/Emoticon/1181369")]
                Emotion_14,
                [StringValue(Value = "Sounds/Emoticon/1181370")]
                Emotion_15,
                [StringValue(Value = "Sounds/Emoticon/1181376")]
                Emotion_16,
                [StringValue(Value = "Sounds/Emoticon/1181382")]
                Emotion_17,
                [StringValue(Value = "Sounds/Emoticon/1181384")]
                Emotion_18,

                // SendGift
                [StringValue(Value = "Sounds/SendGift/ani_Bomb")]
                Bomb,
                [StringValue(Value = "Sounds/SendGift/ani_Egg")]
                Egg,
                [StringValue(Value = "Sounds/SendGift/ani_Flower")]
                Flower,
                [StringValue(Value = "Sounds/SendGift/ani_PourWater")]
                PourWater,
                [StringValue(Value = "Sounds/SendGift/ani_BrickThrow")]
                Brick,
                [StringValue(Value = "Sounds/SendGift/ani_Like")]
                Like,
                [StringValue(Value = "Sounds/SendGift/ani_Ice_cream")]
                Cream,

                //Ingame
                [StringValue(Value = "Sounds/InGame/Countdown_Tiktok")]
                Countdown_Tiktok,
                [StringValue(Value = "Sounds/InGame/Eatting_Piece")]
                Eatting_Piece,
                [StringValue(Value = "Sounds/InGame/King_Being_Attacked")]
                King_Being_Attacked,
                [StringValue(Value = "Sounds/InGame/Level_Up")]
                Level_Up,
                [StringValue(Value = "Sounds/InGame/Moving_Piece")]
                Moving_Piece,
                [StringValue(Value = "Sounds/InGame/Player_Get_In_Table")]
                Player_Get_In_Table,
                [StringValue(Value = "Sounds/InGame/Ready")]
                Ready,
                [StringValue(Value = "Sounds/InGame/Win_Game")]
                Win_Game,


            }

            //static AudioSource[] Sounds = new AudioSource[100];
            //static List<AudioSource> Sounds = new List<AudioSource>(100);
            static Dictionary<SoundId, AudioSource> Sounds = new Dictionary<SoundId, AudioSource>();
            static SoundId[] AutoPlaySounds = new SoundId[] { };
            static SoundId[] AutoPlayLoopSounds = new SoundId[] { SoundId.Background_Music };

            /// <summary>
            /// co danh dau am thanh duoc mo hay ko, 0: disable, 1: enable
            /// </summary>
            private static int enable = 1;

            private static int enable_bgsound = 1;

            public static bool is_init_done = false;


            //static SoundAssetBundleHelper soundHelper = new SoundAssetBundleHelper();

            //internal static void InitSound(SoundAssetBundleHelper SoundHelper)
            //{
            //    Debug.Log("Init Sound");
            //    soundHelper = SoundHelper;
            //    //Debug.LogError("InitSound  length= " + soundHelper.MapSound.Count);
            //    enable = PlayerPrefs.GetInt("sound_enable", 1);
            //    enable_bgsound = PlayerPrefs.GetInt("bgsound_enable", 1);

            //    //gameObject.AddComponent<AudioListener>(); Tam thoi comment 25/8/2016 by ronaldo

            //    //load file trong thu muc resource

            //    Dictionary<string, SoundId> Maps = new Dictionary<string, SoundId>();
            //    foreach (var field in typeof(SoundId).GetFields())
            //    {
            //        //Debug.LogError("------------------------------ InitSOund------------------------");
            //        var attribute = (StringValueAttribute[])field.GetCustomAttributes(typeof(StringValueAttribute), false);

            //        if (attribute.Length == 0) continue;
            //        Maps[attribute[0].Value] = (SoundId)field.GetValue(null);
            //        //Debug.LogError("------------------------------ InitSOund------------------------" + Maps[attribute[0].Value]);
            //    }


//#if UNITY_EDITOR
//                string myPath = "Assets/Resources/Sounds";
//                //                GetSoundFilesFromDirectory(gameObject, Maps, myPath);
//                LoadAllSoundFromConfig(soundHelper.gameObject, Maps);
//#else
//				LoadAllSoundFromConfig(soundHelper.gameObject, Maps);
//#endif

//                is_init_done = true;
            }

        //private static void GetSoundFilesFromDirectory(GameObject gameObject, Dictionary<string, SoundId> Maps, string dirpath)
        //{
        //    DirectoryInfo dir = new DirectoryInfo(dirpath);
        //    FileInfo[] info = dir.GetFiles("*.*");
        //    DirectoryInfo[] dinfo = dir.GetDirectories();
        //    foreach (FileInfo f in info)
        //    {
        //        if (f.Extension == ".ogg" || f.Extension == ".mp3" || f.Extension == ".wav")
        //        {
        //            string tempName = (dirpath + "/" + f.Name).Replace("Assets/Resources/", "");
        //            string path = tempName.Substring(0, tempName.IndexOf('.'));
        //            if (!Maps.ContainsKey(path))
        //            {
        //                LogMng.LogError("ClientConfig-Sound", "Config has not key: " + path + ", " + Maps.ContainsKey(path));
        //                continue;
        //            }
        //            //Logger.Log("ClientConfig-Sound", "key: " + path + ", " + Maps.ContainsKey(path));
        //            //                        LoadSound(gameObject, Maps[path], path);
        //            Debug.LogError("Try To Get Sound");
        //            LoadSoundFromBundle(gameObject, Maps[path], path);
        //        }
        //        else if (f.Extension != ".meta")
        //        {
        //            LogMng.LogError("ClientConfig-Sound", "Unknown Extension: " + (dirpath + "/" + f.Name));
        //        }
        //    }

        //    foreach (DirectoryInfo d in dinfo)
        //    {

        //        string tempName = d.Name;
        //        GetSoundFilesFromDirectory(gameObject, Maps, dirpath + "/" + tempName);
        //    }
        //}

        //private static void LoadAllSoundFromConfig(GameObject gameObject, Dictionary<string, SoundId> Maps)
        //{
        //    //Debug.LogError("Maps.Keys.Count= " + Maps.Keys.Count);
        //    //Debug.LogError("LoadAllSoundFromConfig soundHelper.MapSound length= " + soundHelper.MapSound.Count);
        //    Dictionary<string, SoundId>.KeyCollection.Enumerator enu = Maps.Keys.GetEnumerator();
        //    while (enu.MoveNext())
        //    {
        //        string path = enu.Current;
        //        //                    LoadSound(gameObject, Maps[path], path);
        //        //Debug.LogError("path: " + path);
        //        LoadSoundFromBundle(gameObject, Maps[path], path);
        //    }
        //    PlayLoopSound(SoundId.Background_Music);
        //}


        //private static void LoadSound(GameObject gameObject, SoundId id, string path)
        //{
        //    //Sounds[(int)id] = gameObject.AddComponent<AudioSource>();
        //    //Sounds[(int)id].clip = Resources.Load(path, typeof(AudioClip)) as AudioClip;

        //    AudioSource asrc = gameObject.AddComponent<AudioSource>();
        //    asrc.clip = Resources.Load(path, typeof(AudioClip)) as AudioClip;

        //    Sounds[id] = asrc;
        //}

        //private static void LoadSoundFromBundle(GameObject gameObject, SoundId id, string path)
        //{
        //    //Debug.LogError("LoadSoundFromBundle id= " + id.ToString());
        //    //Sounds[(int)id] = gameObject.AddComponent<AudioSource>();
        //    //Sounds[(int)id].clip = Resources.Load(path, typeof(AudioClip)) as AudioClip;

        //    //				asrc.clip = GlobalVariable.AssetBundleSound.clips[GlobalVariable.AssetBundleSound.ids.IndexOf(id)];


        //    if (soundHelper != null)
        //    {
        //        if (soundHelper.MapSound.ContainsKey(id))
        //        {
        //            AudioSource asrc = gameObject.AddComponent<AudioSource>();
        //            asrc.clip = soundHelper.MapSound[id];
        //            Sounds[id] = asrc;
        //            //Debug.LogError("BUNDLE : " + asrc.clip.name);
        //        }
        //        else
        //        {
        //            Debug.LogError("NOT BUNDLE : " + id);
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("SoundAssetBundleHelper.Instance nulllll");
        //    }



        //}

        //    public static bool ENABLE
        //    {
        //        get
        //        {
        //            return enable == 1;
        //        }
        //        set
        //        {
        //            int temp = value ? 1 : 0;
        //            //if (temp == enable)
        //            //    return;

        //            enable = temp;
        //            //if (!value)
        //            //    StopSound ();
        //            PlayerPrefs.SetInt("sound_enable", enable);
        //            PlayerPrefs.Save();
        //        }
        //    }

        //    public static bool ENABLE_BGSOUND
        //    {
        //        get
        //        {
        //            return enable_bgsound == 1;
        //        }
        //        set
        //        {
        //            int temp = value ? 1 : 0;
        //            //if (temp == enable_bgsound)
        //            //    return;

        //            enable_bgsound = temp;
        //            // enable_bgsound = 0; // temp lock
        //            if (!value)
        //                StopBackgroundSound();
        //            else
        //            {
        //                for (int i = 0; i < AutoPlayLoopSounds.Length; i++)
        //                {
        //                    continue;
        //                    PlayLoopSound(AutoPlayLoopSounds[i]);
        //                }
        //            }
        //            PlayerPrefs.SetInt("bgsound_enable", enable_bgsound);
        //            PlayerPrefs.Save();
        //        }
        //    }

        //    public static void ChangeBackgroundSound(SoundId bgsoundid)
        //    {
        //        return;
        //        StopBackgroundSound();
        //        AutoPlayLoopSounds[0] = bgsoundid;
        //        if (ENABLE_BGSOUND)
        //            PlayLoopSound(AutoPlayLoopSounds[0]);
        //    }

        //    /// <summary>
        //    /// play 1 sound trong khoang thoi gian duration, goi ngoai mainthread
        //    /// </summary>
        //    /// <param name="soundId">tham so sound co the sua tuy theo api</param>
        //    /// <param name="duration">tham so thoi gian co the sua tuy vao api</param>
        //    public static void PlaySound(SoundId soundId)
        //    {

        //        //LogMng.Log("ClientConfig-Sound", "PlaySound " + soundId + ", " + ENABLE);
        //        try
        //        {
        //            if (ENABLE && soundId != null && Sounds[soundId] != null)
        //            {
        //                //play sound
        //                Sounds[soundId].loop = false;
        //                Sounds[soundId].Play();
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            LogMng.Log("ClientConfig-Sound", "PlaySound error: " + exception.Message);
        //        }
        //    }

        //    public static void PlayLoopSound(SoundId soundId)
        //    {
        //        try
        //        {
        //            //LogMng.Log("ClientConfig-Sound", "PlayLoopSound " + soundId + ", " + ENABLE_BGSOUND);
        //            if (ENABLE_BGSOUND && soundId != null && Sounds[soundId] != null)
        //            {
        //                //play sound

        //                Sounds[soundId].loop = true;
        //                Sounds[soundId].Play();
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            LogMng.Log("ClientConfig-Sound", "PlayLoopSound error: " + exception.Message);

        //        }
        //    }

        //    /// <summary>
        //    /// sStop all sound
        //    /// </summary>
        //    public static void StopSound()
        //    {
        //        Dictionary<SoundId, AudioSource>.Enumerator elements = Sounds.GetEnumerator();
        //        while (elements.MoveNext())
        //            if (elements.Current.Value != null)
        //                elements.Current.Value.Stop();
        //        //for (int i = 0; i < Sounds.Length; i++)
        //        //    if(Sounds[i] != null)
        //        //        Sounds [i].Stop ();
        //    }

        //    public static void StopBackgroundSound()
        //    {
        //        try
        //        {
        //            for (int i = 0; i < AutoPlayLoopSounds.Length; i++)
        //            {
        //                Sounds[AutoPlayLoopSounds[i]].Stop();
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            LogMng.Log("ClientConfig-Sound", "StopBackgroundSound error: " + exception.Message);

        //        }
        //    }


        //    public static void PauseSound(SoundId soundId)
        //    {
        //        try
        //        {
        //            if (soundId != null && Sounds[soundId] != null)
        //            {
        //                Sounds[soundId].Pause();
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            LogMng.Log("ClientConfig-Sound", "PauseSound error: " + exception.Message);

        //        }
        //    }
        //}

#endregion

        public static string GetDeviceAccount()
        {
#if UNITY_ANDROID
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                // NGUIDebug.Log("GetDeviceAccount: class = " + activityClass);
                AndroidJavaObject Activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
                //NGUIDebug.Log("GetDeviceAccount: activity = " + Activity);
                using (AndroidJavaClass androidAction = new AndroidJavaClass("com.library.ActivityAction"))
                {
                    // NGUIDebug.Log("GetDeviceAccount: action = " + androidAction);
                    return androidAction.CallStatic<string>("GetDeviceAccount", Activity);
                }
            }
#elif UNITY_IPHONE
			//return CallPlugin.getUUID();
			return SystemInfo.deviceUniqueIdentifier;
#elif UNITY_WEBGL
			return SystemInfo.deviceUniqueIdentifier;
#endif
        }
        /// <summary>
        /// quit app
        /// </summary>
        public static void QuitApplication(bool forceQuit)
        {
#if UNITY_ANDROID
            if (forceQuit)
            {
                Application.Quit();
                return;
            }

            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject Activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
                using (AndroidJavaClass activityAction = new AndroidJavaClass("com.library.ActivityAction"))
                {
                    activityAction.CallStatic("QuitApplication", Activity);
                }
            }
#else
			Application.Quit ();
#endif
        }

        public static void InitClient()
        {
            UserInfo.InitUserInfo();
            SoftWare.InitSoftWare();
            HardWare.InitHardWare();
            Language.InitLanguage();
        }
    }
}