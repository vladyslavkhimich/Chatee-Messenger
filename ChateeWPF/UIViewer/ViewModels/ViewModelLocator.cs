﻿using ChateeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeWPF
{
    public class ViewModelLocator
    {
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();
        public static ApplicationViewModel ApplicationViewModel => IoCContainer.Get<ApplicationViewModel>();
        public static SettingsViewModel SettingsViewModel => IoCContainer.Get<SettingsViewModel>();
    }
}
