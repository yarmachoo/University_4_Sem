using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MvvmTest;
public class MVVMTestViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    int _counter;
    public int Counter
    {
        get => _counter;
        set
        {
            if(_counter == value) return;
            _counter = value;
            OnPropertyChanged();
        }
    }
    private void OnPropertyChanged([CallerMemberName] string property="")
    {
        if (!String.IsNullOrEmpty(property))
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
