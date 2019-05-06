using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace TesseractOnWPF.Model
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public ViewModelBase() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(params string[] names)
        {
            var h = PropertyChanged;
            if (h == null) return;

            CheckPropertyName(names);

            foreach (var name in names)
            {
                h(this, new PropertyChangedEventArgs(name));
            }
        }

        private void CheckPropertyName(params string[] names)
        {
            var props = GetType().GetProperties();
            foreach (var name in names)
            {
                var prop = props.SingleOrDefault(p => p.Name == name);
                if (prop == null) throw new ArgumentException(name);
            }
        }

        protected void OnPropertyChanged<T>(params Expression<Func<T>>[] propertyExpression)
        {
            OnPropertyChanged(
                propertyExpression.Select(ex => ((MemberExpression)ex.Body).Member.Name).ToArray());
        }
    }
}