using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class EffectFactory  {


    public static GameEffect CreateGameEffect(GameEffect.EffectType type) {
        GameEffect newEffect = new GameEffect(type);

        switch (type) {
            case GameEffect.EffectType.KillPlayer:
                newEffect.onActivate = KillPlayer;
                break;
        }



        return newEffect;
    }


    private static void KillPlayer() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //Debug.Log("Killing Player");

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

}
