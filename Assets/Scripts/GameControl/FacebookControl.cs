using UnityEngine;

public class FacebookControl : MonoBehaviour {
    /* #region Initialize the SDK
     void Awake() {
         if (!FB.IsInitialized) {
             // Initialize the Facebook SDK
             FB.Init(InitCallback, OnHideUnity);
         } else {
             // Already initialized, signal an app activation App Event
             FB.ActivateApp();
         }
     }

     private void InitCallback() {
         if (FB.IsInitialized) {
             // Signal an app activation App Event
             FB.ActivateApp();
             // Continue with Facebook SDK
         } else {
             Debug.Log("Failed to Initialize the Facebook SDK");
         }
     }

     private void OnHideUnity(bool isGameShown) {
         if (!isGameShown) {
             // Pause the game - we will need to hide
             Time.timeScale = 0;
         } else {
             // Resume the game - we're getting focus again
             Time.timeScale = 1;
         }
     }
     #endregion
     #region Facebook Login
     List<string> perms = new List<string>() { "public_profile", "email", "user_friends" };
     public void login() {
         FB.LogInWithReadPermissions(perms, AuthCallback);
     }

     private void AuthCallback(ILoginResult result) {
         if (FB.IsLoggedIn) {
             //// AccessToken class will have session details
             var aToken = AccessToken.CurrentAccessToken;
             //// Print current access token's User ID
             //Debug.Log(aToken.UserId);
             //// Print current access token's granted permissions
             //foreach (string perm in aToken.Permissions) {
             //    Debug.Log(perm);
             //}
             GameControl.instance.login.login(1, "sgc", "sgc", GameControl.IMEI, "", 1, "", aToken.TokenString, "");
         } else {
             GameControl.instance.panelMessageSytem.onShow("Đăng nhập facebook thất bại!");
         }
     }
     #endregion
     #region Invite Friend
     public void invite() {
         //FB.AppRequest(message: "Kiếm xiền dễ dàng với Rgame Online!!!", title: "Chơi Rgame nhận quà khủng!!!");
     }
     #endregion*/

    void SendAccetoken(string acces) {
        Debug.Log("ACCCCCC " + acces);
        //sua
        //LobbyViewScript.instance.login.sendloginFB(acces);
    }

    //void SendIP(string ip) {
    //    Debug.Log("IP: " + ip);
    //    GameControl.IMEI = ip;
    //}

    void SendClose() {
        NetworkUtil.GI().close();
    }
}
