using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class EffectFactory  {


    public static GameEffect CreateGameEffect(GameEffect.EffectType type, float delay = 3f) {
        GameEffect newEffect = new GameEffect(type, delay);
        
        switch (type) {
            case GameEffect.EffectType.KillPlayer:
                newEffect.onActivate = KillPlayer;
                break;
            case GameEffect.EffectType.VentAir:
                newEffect.onActivate = VentAir;
                break;
            case GameEffect.EffectType.HoldBreath:
                newEffect.onActivate = HoldBreath;
                break;

            case GameEffect.EffectType.Cry:
                newEffect.onActivate = Cry;
                break;

            case GameEffect.EffectType.Explode:
                newEffect.onActivate = Explode;
                break;

            case GameEffect.EffectType.Flail:
                newEffect.onActivate = Flail;
                break;
        }



        return newEffect;
    }


   public static void KillPlayer() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            return;

        GameObject loadedParticles = Resources.Load("Particles/Blood Poof") as GameObject;

        GameObject activeParticles = GameObject.Instantiate(loadedParticles, player.transform.position, Quaternion.identity) as GameObject;

        GameObject.Destroy(activeParticles, 3f);

        SoundManager.StopMusic();
        SoundManager.PlaySound("Splat");
        
        
        if(player != null) {
            GameObject.Destroy(player);


            SoundManager.soundManager.StartCoroutine(Gameover());

            //Debug.Log("Player Dead");
        }
            
    }

    public static void Cry() {
        SoundManager.PlaySound("Cry", 1.5f);
    }

    public static void Explode() {
        SoundManager.PlaySound("Splosion");

        O2Panel o2Panel = GameObject.Find("O2 Panel").GetComponent<O2Panel>();

        o2Panel.VentO2();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        player.GetComponent<SpaceManInfo>().flames.Play();


    }

    public static void Flail() {
        Rigidbody2D playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        playerBody.angularVelocity += 700f;
    }


    private static IEnumerator Gameover() {
        yield return new WaitForSeconds(2f);
        SoundManager.RestartMusic("Level");

        SceneManager.LoadScene("GameOver");
    }


    

    private static void VentAir()
    {
        Debug.Log("air vented");
  
        SoundManager.soundManager.StartCoroutine(VentAirOngoing());
    }

    private static IEnumerator VentAirOngoing()
    {
        float endTime;
        float duration = 2.5f;
        endTime = Time.time + duration;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D rb2d = player.GetComponent<Rigidbody2D>();
        Transform ventAirLoc = player.GetComponent<SpaceManInfo>().ventAirLoc;
        Vector2 ventAirPos = ventAirLoc.localPosition;
        ParticleSystem pS = ventAirLoc.GetComponent<ParticleSystem>();
       
        O2Panel o2Panel =  GameObject.Find("O2 Panel").GetComponent<O2Panel>();

        pS.Play();
        o2Panel.VentO2();


        while (Time.time < endTime)
        {
            if (player == null || ventAirPos == null)
                break;


            //rb2d.AddForceAtPosition(ventAirLoc.right * 5000 * Time.deltaTime, ventAirPos);
            ventAirPos = ventAirLoc.localPosition;
            
            rb2d.AddRelativeForce(new Vector2(800 * Time.deltaTime, 0));

            yield return null;
            
        }

        if(pS != null)
            pS.Stop();
    }

    public static void HoldBreath()
    {
        SoundManager.soundManager.StartCoroutine(HoldBreathOngoing());
    }

    public static IEnumerator HoldBreathOngoing()
    {
        O2Panel o2Panel = GameObject.Find("O2 Panel").GetComponent<O2Panel>();

        o2Panel.Change02Speed(20);

        SoundManager.PlaySound("Gasp");

        yield return new WaitForSeconds(1f);

        o2Panel.Change02Speed(-20.08f);

        yield return new WaitForSeconds(1f);

        o2Panel.Change02Speed(.08f);

        yield return null;
    }

}
