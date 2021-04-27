/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

namespace DillyzRolesAPI
{
    public class sussysave// : MonoBehaviour
    {
        private const string SAVE_SEPARATOR = "#Value#";

        public static void Save()
        {
            SaveObject saveObject = new SaveObject { big = true, chungus = 69 };
            //string json = JsonConvert.SerializeObject(saveObject, null);
            string[] stringmoment = new string[] { saveObject.big.ToString(), saveObject.chungus.ToString() };
            string json = string.Join(SAVE_SEPARATOR, stringmoment);
            Debug.Log(json);
            File.WriteAllText(getPath(),json);
        }

        public static void Load()
        {
            if (File.Exists(getPath()))
            {
                string saveString = File.ReadAllText(getPath());
                string[] contents = saveString.Split(new[] { SAVE_SEPARATOR }, StringSplitOptions.None);
                bool big = bool.Parse(contents[0]);
                int chungus = int.Parse(contents[1]);
                Reactor.Logger<RoleAPI>.Error("big = " + big.ToString() + " chungus = " + chungus.ToString());
            }
        }

        public class SaveObject
        {
            public bool big;
            public int chungus;
        }

        public static string getPath()
        {
            string pathFile = Environment.GetEnvironmentVariable("AmongUs") + @"\BepInEx\config\gg.reactor.dillyzroleapi.cfg";
            return pathFile;
        }


    }   //TESTING, PLEASE DO NOT USE AS REFERENCE.
}*/