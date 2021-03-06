﻿using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Threading;
using Drawer3D.Model.Exceptions;

namespace Drawer3D.ViewWpf
{
    /// <summary>
    ///     Логика взаимодействия с приложением
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        ///     Обработчик необработанных исключений
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Событие</param>
        private void Application_DispatcherUnhandledException(object sender
            , DispatcherUnhandledExceptionEventArgs e)
        {
            string errorInfo = null;

            switch (e.Exception.InnerException)
            {
                case FigureException figureException:
                    errorInfo = figureException.FigureError.Message;
                    e.Handled = true;
                    break;

                case COMException _:
                    errorInfo = "Проект, программа были закрыты. Создайте новый проект.";
                    e.Handled = true;
                    break;

                case ExternalException exception:
                    errorInfo = "В ОС не найдена программа SOLIDWORKS, " +
                                $"которая использует API {exception.Message}.";
                    e.Handled = true;
                    break;
            }

            if (errorInfo != null)
            {
                MessageBox.Show(errorInfo
                    , string.Empty
                    , MessageBoxButton.OK
                    , MessageBoxImage.Exclamation);
            }
        }
    }
}