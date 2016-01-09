﻿using eUni.BusinessLogic.Providers.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUni.BusinessLogic.IProviders
{
    public interface IModuleProvider
    {
        void CreateModule(ModuleDTO dtoMod);
        ModuleDTO GetById(int modId);
        void UpdateModule(ModuleDTO mod);
    }
}