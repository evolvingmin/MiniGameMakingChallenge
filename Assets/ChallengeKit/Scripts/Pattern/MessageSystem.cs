using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChallengeKit.Pattern
{
    public class MessageSystem : Singleton<MessageSystem>
    {
        List<SystemMono> systemObjects;

        public void Listen(SystemMono systemObject)
        {
            if(systemObjects == null)
            {
                systemObjects = new List<SystemMono>();
            }

            if (systemObjects.Contains(systemObject) == true)
                return;

            systemObjects.Add(systemObject);
        }

        public void BroadcastSystems(SystemMono sender, string command, params object[] objs)
        {
            foreach (var system in systemObjects)
            {
                if (sender == system)
                    continue;

                system.ProcReceive(command, objs);
            }
            
        }
    }

    public class SystemMono : MonoBehaviour, IMessenger
    {
        private IParser parser;

        private void Awake()
        {
            MessageSystem.Instance.Listen(this);
        }

        public virtual Define.Result Init(IParser parser)
        {
            this.parser = parser;

            if (parser == null)
                return Define.Result.SYSTEM_PARSER_NULL;

            return parser.Init(this);
        }

        public void ProcSend(string Command, params object[] Objs)
        {
            MessageSystem.Instance.BroadcastSystems(this, Command, Objs);
        }

        public void ProcReceive(string Command, params object[] Objs)
        {
            if (parser == null)
                return;

            parser.ParseCommand(Command, Objs);
        }
    }

    public interface IParser
    {
        Define.Result Init(SystemMono parentSystem);

        void ParseCommand(string Command, params object[] Objs);
    }

}
