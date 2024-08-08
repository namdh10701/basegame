using _Game.Features.Gameplay;
using System.Collections.Generic;
using UnityEngine;

public class CarpetShieldManager : MonoBehaviour
{
    public Carpet carpet;
    public Dictionary<IBuffable, List<CarpetShield>> dic = new Dictionary<IBuffable, List<CarpetShield>>();
    public void Buff(IBuffable buffable, int[] effectId)
    {
        MonoBehaviour transform = buffable as MonoBehaviour;
        CarpetShield carpetShield = null;
        List<CarpetShield> shields = new List<CarpetShield>();
        foreach (int id in effectId)
        {
            switch (id)
            {
                case 1:
                    if (buffable is not Ship)
                    {
                        return;
                    }
                    carpetShield = transform.gameObject.AddComponent<TypeOneShield>();
                    carpetShield.Init(carpet);
                    break;
                case 2:
                    carpetShield = transform.gameObject.AddComponent<TypeTwoShield>();
                    carpetShield.Init(carpet);
                    break;
                case 3:
                    if (buffable is not Ship)
                    {
                        return;
                    }
                    carpetShield = transform.gameObject.AddComponent<TypeThreeShield>();
                    carpetShield.Init(carpet);

                    break;
                case 4:
                    if (buffable is not Ship)
                    {
                        return;
                    }
                    carpetShield = transform.gameObject.AddComponent<TypeFourShield>();
                    carpetShield.Init(carpet);
                    break;
            }
            shields.Add(carpetShield);
        }

        dic.Add(buffable, shields);
    }

    public void RemoveBuff(IBuffable buffable)
    {
        MonoBehaviour transform = buffable as MonoBehaviour;
        CarpetShield carpetShield = null;
        if (dic.ContainsKey(buffable))
        {
            foreach (CarpetShield shield in dic[buffable])
            {
                shield.RemoveBuff(buffable);
                Destroy(shield);
            }  
            dic.Remove(buffable);
        }
      
    }
}

