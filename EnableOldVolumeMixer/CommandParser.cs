﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnableOldVolumeMixer
{
    class CommandParser
    { 
        private RegistryEditor regEditor;

        public CommandParser(RegistryEditor regEditor)
        {
            this.regEditor = regEditor;
        }


        public void TakeCommands(string command)
        {
            bool keyExists = regEditor.KeyExists();
            switch (command)
            {  
                case "1":
                    TryEnable();
                    break;
                case "2":
                    TryDisable();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            } 
        }

        private void TryEnable()
        {
            if (regEditor.KeyExists())
            {
                Console.WriteLine("Old mixer already enabled");
            }
            else
            {
                DWord dWord = new DWord
                {
                    Name = Constants.MixerDwordName,
                    Value = Constants.MixerDwordValue,
                };

                regEditor.CreateKey();
                regEditor.CreateDWordValue(dWord);
                Console.WriteLine("Mixer successfully changed.");
            }
        }

        private void TryDisable()
        {
            if (!regEditor.KeyExists())
            {
                Console.WriteLine("Old mixer already disabled");
            }
            else
            {
                regEditor.DeleteKey();
                Console.WriteLine("Old mixer successfully disabled");
            }
            
        }
    }

}
