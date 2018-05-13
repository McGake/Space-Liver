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
        }



        return newEffect;
    }


   public static void KillPlayer() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        

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
            //rb2d.AddForceAtPosition(ventAirLoc.right * 5000 * Time.deltaTime, ventAirPos);
            ventAirPos = ventAirLoc.localPosition;
            
            rb2d.AddRelativeForce(new Vector2(800 * Time.deltaTime, 0));

            yield return null;
            
        }
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

        yield return new WaitForSeconds(1f);

        o2Panel.Change02Speed(-20.08f);

        yield return new WaitForSeconds(1f);

        o2Panel.Change02Speed(.08f);

        yield return null;
    }

}
