using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(player != null) {
            GameObject.Destroy(player);

            //Debug.Log("Player Dead");
        }
            
    }



}
