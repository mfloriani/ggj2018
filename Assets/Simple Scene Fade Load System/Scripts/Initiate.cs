using UnityEngine;

public class Initiate : MonoBehaviour {
    public static void Fade(string scene, Color col, float damp) {
        GameObject init = new GameObject();
        init.name = "Fader";
        init.AddComponent<Fader>();
        Fader scr = init.GetComponent<Fader>();
        scr.fadeDamp = damp;
        scr.fadeScene = scene;
        scr.fadeColor = col;
        scr.start = true;
    }
}
