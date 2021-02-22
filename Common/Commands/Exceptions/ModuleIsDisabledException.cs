using BonusBot.Common.Languages;
using System;

namespace BonusBot.Common.Commands.Exceptions
{
    public class ModuleIsDisabledException : Exception
    {
        public ModuleIsDisabledException() : base(Texts.ModuleIsDisabledError)
        { }
    }
}