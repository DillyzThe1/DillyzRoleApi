using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Reactor;
using Reactor.Extensions;

namespace DillyzRolesAPI.Options
{
    [RegisterInIl2Cpp]
    public class CustomNumberOption : MonoBehaviour
    {
        public StringNames numOptionTitle { get; private set; }
        public int optTitleInt { get; private set; }
        public string hostOptionsName { get; set; } = "int opt";
        public float defValue { get; set; } = 1f;
        public float minValue { get; set; } = 0f;
        public float maxValue { get; set; } = 2f;
        public float incrementValue { get; set; } = 1f;
        public float value { get; set; }
        public char suffix { get; set; } = ' ';

        public void Awake()
        {
            this.numOptionTitle = (StringNames)CustomOptions.stringId;
            this.optTitleInt = CustomOptions.stringId;
            CustomOptions.stringId += 1;
            value = defValue;
            CustomOptions.numOpts.Add(this);
        }
    }
    [RegisterInIl2Cpp]
    public class CustomBoolOption : MonoBehaviour
    {
        public StringNames boolOptionTitle { get; private set; }
        public int optTitleInt { get; private set; }
        public string hostOptionsName { get; set; } = "bool opt";
        public bool defValue { get; set; } = false;
        public bool value { get; set; }

        public void Awake()
        {
            this.boolOptionTitle = (StringNames)CustomOptions.stringId;
            this.optTitleInt = CustomOptions.stringId;
            CustomOptions.stringId += 1;
            value = defValue;
            CustomOptions.boolOpts.Add(this);
        }
    }
}
