﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Zodiacon.WPF;

namespace SrvDrv.ViewModels {
    class ServiceViewModel : BindableBase {
        public ServiceController Service { get; }

        [Import]
        IMessageBoxService MessageBoxService;

        [Import]
        string AppName;

        public ServiceViewModel(ServiceController service) {
            Service = service;
        }

        ICommand _startCommand;
        public ICommand StartCommand => _startCommand ?? (_startCommand = new DelegateCommand(() => StartService(true), () => Service.Status == ServiceControllerStatus.Stopped));

        private void StartService(bool start) {
            try {
                Service.Start();
            }
            catch(Exception ex) {
                MessageBoxService.ShowMessage($"Error: {ex.Message}", AppName);
            }
        }
    }
}