using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RubikCasual.Waifu
{
    [Serializable]
    public class InfoWaifuAssets
    {
        public List<InfoWaifuAsset> lsInfoWaifuAssets = new List<InfoWaifuAsset>();
    }

    [Serializable]
    public class InfoWaifuAsset
    {
        public float Index, AttackDistance, HP, DmgPhysic, DmgMagic, Def, MagicDef, CritRate, CritDmgPhysic, CritDmgMagic;
    }
}
