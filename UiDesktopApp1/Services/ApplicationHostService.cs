﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UiDesktopApp1.Views.Pages;
using UiDesktopApp1.Views.Windows;
using Wpf.Ui;

namespace UiDesktopApp1.Services
{
    /// <summary>
    /// Managed host of the application.
    /// </summary>
    public class ApplicationHostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        private INavigationWindow _navigationWindow;

        public ApplicationHostService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await HandleActivationAsync();
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Creates main window during activation.
        /// </summary>
        private async Task HandleActivationAsync()
        {
            var loginService = _serviceProvider.GetRequiredService<LoginWindow>();
            var loginResult = loginService.ShowDialog();
            
            if (loginResult == true && !Application.Current.Windows.OfType<MainWindow>().Any())
            {
                _navigationWindow = (
                    _serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow
                )!;

                Application.Current.MainWindow = _navigationWindow as Window;

                _navigationWindow!.ShowWindow();

                //if(_navigationWindow is MainWindow mw) mw.NavigateOnLoad(typeof(DashboardPage));

                _navigationWindow.Navigate(typeof(Views.Pages.DashboardPage));
            }

            await Task.CompletedTask;
        }
    }
}
