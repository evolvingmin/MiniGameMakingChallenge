﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChallengeKit.Pattern;

namespace ChallengeKit.GamePlay
{
    class DialogParser : IParser
    {
        DialogSystem dialogSystem;

        public Define.Result Init(SystemMono parentSystem)
        {
            dialogSystem = (DialogSystem)parentSystem;

            if (dialogSystem == null)
                return Define.Result.NOT_INITIALIZED;

            return Define.Result.OK;
        }

        public void ParseCommand(string Command, params object[] Objs)
        {
            if(Command == "StartDialogByScenario")
            {
                ScenarioNode startnode = (ScenarioNode)Objs[0];
                
                if(dialogSystem == null)
                {
                    UnityEngine.Debug.LogWarning("dialogSystem is null, Command is : " + Command);
                    return;
                }

                dialogSystem.ParseCSVData(startnode.ScriptRoot, startnode.DialogType, startnode.ScriptName);
                dialogSystem.StartDialog();
            }


        }
    }
}
