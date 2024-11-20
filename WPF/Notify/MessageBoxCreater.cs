using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF.Notify
{
    public static class MessageBoxCreater
    {
        public static MessageBoxResult ShowError(string message)
        {
            return MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.Yes);
        }
    }
}
